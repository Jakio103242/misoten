using UnityEngine;
using Game.Story;

namespace Game.Intaract
{
    public class Talkable : Intaractable
    {
        public StoryTalkEvent storyIncident;
        public override StoryIncident StoryIncident() => storyIncident;
    }
}