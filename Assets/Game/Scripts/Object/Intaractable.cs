using UnityEngine;
using UniRx;
using System;
using Game.Story;

namespace Game.Intaract
{
    public abstract class Intaractable : MonoBehaviour
    {
        public Renderer Renderer;
        public bool HintActive;
        [SerializeField] private ReactiveProperty<bool> completed;
        public bool Completed
        {
            get => completed.Value;
            set => completed.Value = value;
        }

        public abstract StoryIncident StoryIncident();

        void Start()
        {
            HintActive = false;
            completed.Value = false;
            completed.Where(value => value).Subscribe(_ => StoryIncident().Completed = true);
        }
    }
}