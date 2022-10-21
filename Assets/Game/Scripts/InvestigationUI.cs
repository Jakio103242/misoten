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

        //�����͈̔͂ɓ���܂őҋ@
        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: token);

        //�ȉ�UI�\��
        this.gameObject.SetActive(true);

        //�͈͊O�ɏo�������
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: token);

        this.gameObject.SetActive(false);
    }
}
