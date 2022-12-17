using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEditor;

namespace Game.Story
{
    [CreateAssetMenu(fileName = "StoryEvent", menuName = "GameSO/StoryEvent")]
    public class StoryEvent : ScriptableObject
    {
        [SerializeField] List<StoryEvent> parentEvents;
        public List<StoryEvent> ParentEvents => parentEvents;
        [SerializeField] List<StoryEvent> childEvents;
        [SerializeField] List<StoryIncident> incidents;
        [SerializeField] private ReactiveProperty<bool> completed;
        public bool Completed
        {
            get => completed.Value;
            set => completed.Value = value;
        }
        public IObservable<bool> OnCompleted => completed;
        [SerializeField] float ratio;
        public float Ratio => ratio;
        [SerializeField] ReactiveProperty<bool> active;
        public bool Active
        {
            get => active.Value;
            set => active.Value = value;
        }
        private int curProgressCount;

        void OnEnable()
        {
            curProgressCount = 0;
            active.Where(value => value).Subscribe(_ => OnActive());
            foreach(var incident in incidents)
            {
                incident.OnCompleted.Where(value => value).Subscribe(_ => OnCompletedIncident(incident));
                
                if(Active) incident.Active = true;
                else incident.Active = false;

                if(!incident.Completed) break;
                curProgressCount += 1;
            }
#if UNITY_EDITOR
            if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == true)
            {
                UnityEditor.EditorApplication.playModeStateChanged += state => 
                {
                    if(state == PlayModeStateChange.EnteredEditMode)
                    {
                        Resources.UnloadAsset(this);
                    }
                };
            }
#endif
        }

        void OnCompletedIncident(StoryIncident storyIncident)
        {
            curProgressCount += 1;
            if(curProgressCount >= incidents.Count)
            {
                completed.Value = true;
                Active = false;
                foreach(var childEvent in childEvents)
                {
                    childEvent.Active = true;
                }
            }
        }

        void OnActive()
        {
            foreach(var incident in incidents)
            {
                incident.Active = true;
            }
        }
    }
}
