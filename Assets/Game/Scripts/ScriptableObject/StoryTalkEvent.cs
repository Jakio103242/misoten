using UnityEngine;
using Game.Data;

namespace Game.Story
{
    [CreateAssetMenu(fileName = "SE-TE", menuName = "GameSO/StoryTalkEvent")]
    public class StoryTalkEvent : StoryIncident
    {
        public DialogueData DialogueData;
    }
}