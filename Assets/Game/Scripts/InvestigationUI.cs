using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class InvestigationUI : MonoBehaviour
{
    [SerializeField][Header("�I�u�W�F�N�g��")]
    private string[] msgObjectName;

    [SerializeField][Header("����")]
    private string[] msgInvestigation;

    [SerializeField][Header("Canvas")]
    private GameObject obj;

    [SerializeField][Header("�I�u�W�F�N�g��:�e�L�X�g")]
    private Text txtObjectName;

    [SerializeField][Header("����:�e�L�X�g")]
    private Text txtInvestigation;

    GameObject objCanvas = null;

    private async void Start()
    {
        await ShowInvestigationUI(this.GetCancellationTokenOnDestroy());

        objCanvas = obj;
        objCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    private async UniTask ShowInvestigationUI(CancellationToken token)
    {
        //�����͈̔͂ɓ���܂őҋ@
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: token);

        //�ȉ�UI�\��
        objCanvas.SetActive(true);

        for (int i = msgObjectName.GetLowerBound(0); i <= msgObjectName.GetUpperBound(0); i++)
        {
            txtObjectName.text = msgObjectName[i];
            txtInvestigation.text = msgInvestigation[i];

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken: token);
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }

        objCanvas.SetActive(false);
    }
}
