using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.InputSystem;


public class InvestigationUI : MonoBehaviour
{
    [SerializeField]
    [Header("�\������Ă��邩")]
    public bool dislay;

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

        //�����͈̔͂ɓ���܂őҋ@�i���͍��N���b�N�ő�p�j
        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: token);

        //�ȉ�UI�\��
        this.gameObject.SetActive(true);

        //�͈͊O�ɏo��������i���̓X�y�[�X�L�[�ő�p�j
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: token);

        this.gameObject.SetActive(false);
    }
}
