using UnityEngine;
using UniRx;
using System;
using Game.Story;

namespace Game.Intaract
{
    public abstract class Intaractable : MonoBehaviour
    {
        public Renderer Renderer;
        [SerializeField] protected ReactiveProperty<bool> hintActive;
        public bool HintActive
        {
            get => hintActive.Value;
            set => hintActive.Value = value;
        }
        public IObservable<bool> OnHintActive => hintActive;
        [SerializeField] protected ReactiveProperty<bool> completed;
        public bool Completed
        {
            get => completed.Value;
            set => completed.Value = value;
        }

        [SerializeField] protected StoryIncident storyIncident;
        public StoryIncident StoryIncident => storyIncident;

        void Start()
        {
            hintActive.Value = false;
            completed.Value = false;
            completed.Where(value => value).Subscribe(_ => storyIncident.Completed = true);
        }
    }
}