using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem;
public class InvestigationUI : MonoBehaviour
{

    private void Start()
    {
        ShowInvestigationUI(this.GetCancellationTokenOnDestroy()).Forget();
    }

    void Update()
    {
        
    }

    private async UniTask ShowInvestigationUI(CancellationToken token)
    {
        this.gameObject.SetActive(false);

        //調査の範囲に入るまで待機
        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: token);

        //以下UI表示
        this.gameObject.SetActive(true);

        //範囲外に出たら消す
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: token);

        this.gameObject.SetActive(false);
    }
}
