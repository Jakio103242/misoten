using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Verum
{
    public class Story : ScriptableObject
    {
        public struct Event
        {
            public string id;
            public bool flag;
        }

        public List<Event> ProgressFlag;
        public float ProgressRatio => FProgressRatio();

        private float FProgressRatio()
        {
            int count = 0;
            foreach(var gameEvent in ProgressFlag)
            {
                if(gameEvent.flag == true) count++;
            }
            return (float)count / ProgressFlag.Count;
        }
    }
}

