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

        //UIオブジェクト非表示
        SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        quotetext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    }

    private void Update()
    {
        ////デバッグ用
        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    SetBoolSerifDisplay(true);
        //}

        //Displayの値がtrueのときのみ処理を行う
        if (Display.Value == true && calledOnce == false)
        {
            OnDisplay(Dialogue, this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    //表示フラグを変更する
    public void SetBoolSerifDisplay(bool setbool)
    {
        Display.Value = setbool;
    }

    //セリフを表示する
    public async UniTask OnDisplay(DialogueData data, CancellationToken token)
    {
        calledOnce = true;

        //UIオブジェクト表示
        SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        quotetext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //セリフ表示
        for (int i = 0; i < data.Dialogue.Count; i++)
        {
            nametext.text = data.Dialogue[i].name;
            quotetext.text = data.Dialogue[i].quote;

            //左クリックで次へ（仮）
            await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: token);

            //セリフ終了
            if (i == data.Dialogue.Count-1)
            {
                //UIオブジェクト非表示
                SerifImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                quotetext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                Display.Value = false;
                calledOnce = false;
            }
        }

    }
}
