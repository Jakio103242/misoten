using Game.Story;
using Game.Data;

namespace Game.Intaract
{
    public class Investigatable : Intaractable
    {
        public InvestigationData GetInvestigationData() => ((StoryInvestigationEvent)storyIncident).InvestigationData;
    }
}