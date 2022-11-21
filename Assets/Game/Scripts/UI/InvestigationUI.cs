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
    [Header("����UI��\������")]
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

        //UI��\��
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        calledOnce = false;
    }

    void Update()
    {
        ////�f�o�b�O�p
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    SetBoolInvestigationDisplay(true);
        //}


        //InvestigationDisplay�̒l��true�̂Ƃ��̂ݏ������s��
        if (InvestigationDisplay.Value == true && calledOnce == false)
        {
            ShowInvestigationUI(this.GetCancellationTokenOnDestroy()).Forget();
        }

    }

    //�\���t���O��ύX����
    public void SetBoolInvestigationDisplay(bool setbool)
    {
        InvestigationDisplay.Value = setbool;
    }

    //����UI��\������
    private async UniTask ShowInvestigationUI(CancellationToken token)
    {
        //UI�\��
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //�͈͊O�ɏo��������i���̓X�y�[�X�L�[�ő�p�j
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: token);

        //UI��\��
        nametext.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        investigationText.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        InvestigationDisplay.Value = false;
        calledOnce = false;

    }
}
