using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEditor;

namespace Game.Story
{
    [CreateAssetMenu(fileName = "StoryEventManager", menuName = "GameSO/StoryEventManager")]
    public class StoryEventManager : ScriptableObject
    {
        [SerializeField] List<StoryEvent> storyEvents;
        [SerializeField] StoryEvent rootStoryEvent;
        [SerializeField] bool completed;
        [SerializeField] float progressRatio;
        public float ProgressRatio => progressRatio;

        void OnEnable()
        {
            foreach(var storyEvent in storyEvents)
            {
                Debug.Log(storyEvent);
                storyEvent.OnCompleted.Where(value => value).Subscribe(_ => OnCompletedStoryEvent(storyEvent));
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

        void OnDisable()
        {

        }

        public void SetRootEvent()
        {
            for(int index = 0; index < storyEvents.Count; index++)
            {
                if(storyEvents[index].ParentEvents.Count == 0)
                {
                    rootStoryEvent = storyEvents[index];
                    storyEvents[index].Active = true;
                }
                else 
                {
                    storyEvents[index].Active = false;
                }
            }
        }

        void OnCompletedStoryEvent(StoryEvent storyEvent)
        {
            progressRatio += storyEvent.Ratio;
            if(progressRatio >= 100)
            {
                completed = true;
            }
        }
    }
}

