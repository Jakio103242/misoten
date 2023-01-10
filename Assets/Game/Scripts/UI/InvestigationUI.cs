using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using TMPro;
using DG.Tweening;
using Game.Data;

namespace Game.UI
{
    public class InvestigationUI : MonoBehaviour
    {
        [SerializeField]
        private BoolReactiveProperty display = new BoolReactiveProperty(false);
        //public bool Display => display.Value;
        //public IObservable<bool> OnInvestigationDisplay => display;

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI investigationText;

        //for text animation
        [Header("Text Animation"), SerializeField] private float duration;
        private float alpha;

        private bool inAnimation;

        private void Start()
        {
            alpha = 0.0f;
            //UI Color Settings
            nameText.color = new Color(investigationText.color.r, investigationText.color.g, investigationText.color.b, alpha);
            investigationText.color = new Color(investigationText.color.r, investigationText.color.g, investigationText.color.b, alpha);

            display.Value = false;
            inAnimation = false;

            display.Where(value => value).Subscribe(_ => ActiveAnimation()).AddTo(this);
            display.Where(value => !value).Subscribe(_ => InactiveAnimation()).AddTo(this);
        }

        void Update()
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            nameText.color = new Color(investigationText.color.r, investigationText.color.g, investigationText.color.b, alpha);
            investigationText.color = new Color(investigationText.color.r, investigationText.color.g, investigationText.color.b, alpha);
        }

        public void OnDisplay(InvestigationData investigationData)
        {
            display.Value = true;
            nameText.text = investigationData.Info.name;
            investigationText.text = investigationData.Info.explanation;
        }
        public void NonDisplay()
        {
            display.Value = false;
        }

        private void ActiveAnimation()
        {
            inAnimation = true;
            DOTween.To(() => alpha, x => alpha = x, 1.0f, duration).OnComplete(() => inAnimation = false).Play();
        }

        private void InactiveAnimation()
        {
            inAnimation = true;
            DOTween.To(() => alpha, x => alpha = x, 0.0f, duration).OnComplete(() => inAnimation = false).Play();
        }
    }
}
