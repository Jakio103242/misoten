using UnityEngine;
using Game.Story;

namespace Game.Intaract
{
    public class Investigatable : Intaractable
    {
        [SerializeField] private StoryInvestigationEvent storyIncident;
        public override StoryIncident StoryIncident() => storyIncident;
    }
}