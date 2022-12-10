using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Dialoue;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem;
using UniRx;
using TMPro;

public class SerifDisplay : MonoBehaviour
{
    public DialogueData Dialogue;
    public Image SerifImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI QuoteText;


    [SerializeField]
    BoolReactiveProperty Display = new BoolReactiveProperty(false);

    TextMeshProUGUI nametext;
    TextMeshProUGUI quotetext;

    private bool calledOnce;

    private void Start()
    {
        nametext = NameText.GetComponent<TextMeshProUGUI>();
        quotetext = QuoteText.GetComponent<TextMeshProUGUI>();
        calledOnce = false;

        //UI�I�u�W�F�N�g��\��
        SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        quotetext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    }

    private void Update()
    {
        ////�f�o�b�O�p
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    SetBoolSerifDisplay(true);
        //}

        //Display�̒l��true�̂Ƃ��̂ݏ������s��
        if (Display.Value == true && calledOnce == false)
        {
            OnDisplay(Dialogue, this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    //�\���t���O��ύX����
    public void SetBoolSerifDisplay(bool setbool)
    {
        Display.Value = setbool;
    }

    //�Z���t��\������
    public async UniTask OnDisplay(DialogueData data, CancellationToken token)
    {
        calledOnce = true;

        //UI�I�u�W�F�N�g�\��
        SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        quotetext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //�Z���t�\��
        for (int i = 0; i < data.Dialogue.Count; i++)
        {
            nametext.text = data.Dialogue[i].name;
            quotetext.text = data.Dialogue[i].quote;

            //���N���b�N�Ŏ��ցi���j
            await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: token);

            //�Z���t�I��
            if (i == data.Dialogue.Count-1)
            {
                //UI�I�u�W�F�N�g��\��
                SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                quotetext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                Display.Value = false;
                calledOnce = false;
            }
        }

    }
}
