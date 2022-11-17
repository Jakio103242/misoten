using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Dialoue;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem;
using UniRx;

public class SerifDisplay : MonoBehaviour
{
    public DialogueData Dialogue;
    public Text NameText;
    public Text QuoteText;

    [SerializeField]
    [Header("セリフを表示する")]
    BoolReactiveProperty Display = new BoolReactiveProperty(false);

    Text nametext;
    Text quotetext;

    private bool calledOnce;

    private void Start()
    {
        nametext = NameText.GetComponent<Text>();
        quotetext = QuoteText.GetComponent<Text>();
        calledOnce = false;
    }

    private void Update()
    {
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
                nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                quotetext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

                Display.Value = false;
                calledOnce = false;
            }
        }

    }
}
