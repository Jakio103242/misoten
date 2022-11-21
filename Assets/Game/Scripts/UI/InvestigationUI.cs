using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem;
using UniRx;
using TMPro;
public class InvestigationUI : MonoBehaviour
{
    [SerializeField]
    [Header("調査UIを表示する")]
    BoolReactiveProperty InvestigationDisplay = new BoolReactiveProperty(false);

    [SerializeField]
    private TextMeshProUGUI NameText;

    [SerializeField]
    private TextMeshProUGUI InvestigationText;

    TextMeshProUGUI nametext;
    TextMeshProUGUI investigationText;

    private bool calledOnce;

    private void Start()
    {
        nametext = NameText.GetComponent<TextMeshProUGUI>();
        investigationText = InvestigationText.GetComponent<TextMeshProUGUI>();

        //UI非表示
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        calledOnce = false;
    }

    void Update()
    {
        ////デバッグ用
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    SetBoolInvestigationDisplay(true);
        //}


        //InvestigationDisplayの値がtrueのときのみ処理を行う
        if (InvestigationDisplay.Value == true && calledOnce == false)
        {
            ShowInvestigationUI(this.GetCancellationTokenOnDestroy()).Forget();
        }

    }

    //表示フラグを変更する
    public void SetBoolInvestigationDisplay(bool setbool)
    {
        InvestigationDisplay.Value = setbool;
    }

    //調査UIを表示する
    private async UniTask ShowInvestigationUI(CancellationToken token)
    {
        //UI表示
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //範囲外に出たら消す（今はスペースキーで代用）
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: token);

        //UI非表示
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        InvestigationDisplay.Value = false;
        calledOnce = false;

    }
}
