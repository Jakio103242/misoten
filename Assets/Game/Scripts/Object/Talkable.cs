using UnityEngine;
using Game.Story;
using Game.Data;

namespace Game.Intaract
{
    public sealed class Talkable : Intaractable
    {
        public DialogueData GetDialogueData() => ((StoryTalkEvent)storyIncident).DialogueData;
    }
}