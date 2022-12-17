using System;
using UnityEngine;
using UnityEditor;
using UniRx;

namespace Game.Story
{
    public abstract class StoryIncident : ScriptableObject
    {
        [SerializeField] protected StoryEvent storyEvent;
        [SerializeField] protected float ratio;
        public float Ratio => ratio;
        public bool Active;
        [SerializeField] private ReactiveProperty<bool> completed;
        public bool Completed
        {
            get => completed.Value;
            set => completed.Value = value;
        }
        public IObservable<bool> OnCompleted => completed;

        void OnEnable()
        {
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
    }
}