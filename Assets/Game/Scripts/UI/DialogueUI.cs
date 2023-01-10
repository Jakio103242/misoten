using UnityEngine;
using Game.Data;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;

namespace Game.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Image dialogueImage;
        [SerializeField] private Image edaImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI dialogueText;

        [SerializeField] private BoolReactiveProperty display = new BoolReactiveProperty(false);

        [Header("Text Animation"), SerializeField] private float duration;
        [SerializeField] private float characterInterval;
        [SerializeField] private float quateInterval;
        private float alpha;
        private bool inAnimation;
        private DialogueData dialogueData;
        private int visibleCharacters;
        private int curDialogueIndex;

        private Sequence sequence;

        private void Start()
        {
            //test
            display.Where(value => value).Subscribe(_ => ActiveAnimation()).AddTo(this);
            display.Where(value => !value).Subscribe(_ => InactiveAnimation()).AddTo(this);

            inAnimation =false;

            sequence = DOTween.Sequence().SetLink(this.gameObject);
            sequence.Append(DOTween.To(() => alpha, x => alpha = x, 1.0f, duration));
            
           //UIオブジェクト非表示
            visibleCharacters = 0;
            curDialogueIndex = 0;
            alpha = 0.0f;
            dialogueImage.color = new Color(dialogueImage.color.r, dialogueImage.color.g, dialogueImage.color.b, alpha);
            edaImage.color = new Color(edaImage.color.r, edaImage.color.g, edaImage.color.b, alpha);
            nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, alpha);
            dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, alpha);
        }

        private void Update()
        {
            UpdateColor();
            UpdateTextVisibleCharacters();
        }

        public void SetBoolSerifDisplay(bool setbool)
        {
            display.Value = setbool;
        }

        public void OnDisplay(DialogueData dialogueData)
        {
            this.dialogueData = dialogueData;

            display.Value = true;
        }
        public void NonDisplay()
        {
            display.Value = false;
        }

        private void UpdateColor()
        {
            dialogueImage.color = new Color(dialogueImage.color.r, dialogueImage.color.g, dialogueImage.color.b, alpha);
            edaImage.color = new Color(edaImage.color.r, edaImage.color.g, edaImage.color.b, alpha);
            nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, alpha);
            dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, alpha);
        }

        private void UpdateTextVisibleCharacters()
        {
            dialogueText.maxVisibleCharacters = visibleCharacters;
        }

        private void ActiveAnimation()
        {
            inAnimation = true;
            visibleCharacters = 0;
            for(int index = 0; index < dialogueData.Dialogue.Count; index++)
            {
                sequence.AppendCallback(() => SetQuate())
                        .Append(DOTween.To(() => visibleCharacters, x => visibleCharacters = x, dialogueData.Dialogue[curDialogueIndex].quote.Length, characterInterval).SetEase(Ease.Linear))
                        .AppendInterval(quateInterval);
            }

            sequence.Play().OnComplete(() => inAnimation = false);
        }

        private void SetQuate()
        {
            visibleCharacters = 0;
            nameText.text = dialogueData.Dialogue[curDialogueIndex].name;
            dialogueText.text = dialogueData.Dialogue[curDialogueIndex].quote;
            curDialogueIndex++;
        }

        private void InactiveAnimation()
        {
            inAnimation = true;
            DOTween.To(() => alpha, x => alpha = x, 0.0f, duration).OnComplete(() => inAnimation = false).Play();
        }
    }
}
