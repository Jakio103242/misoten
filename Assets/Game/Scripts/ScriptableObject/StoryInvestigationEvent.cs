using UnityEngine;
using Game.Data;

namespace Game.Story
{
    [CreateAssetMenu(fileName = "SE-IE", menuName = "GameSO/StoryInvestigationEvent")]
    public class StoryInvestigationEvent : StoryIncident
    {
        public DialogueData DialogueData;
    }
}