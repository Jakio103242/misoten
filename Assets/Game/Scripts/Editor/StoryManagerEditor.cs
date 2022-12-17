using UnityEngine;
using UnityEditor;

namespace Game.Story
{
    [CustomEditor(typeof(StoryEventManager))]
    public class StoryManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            StoryEventManager storyEventManager = target as StoryEventManager;
            if (GUILayout.Button("SetRootEvent"))
            {
                storyEventManager.SetRootEvent();
            }
        }
    }
}