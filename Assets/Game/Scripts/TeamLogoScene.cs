using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamLogoScene : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    private bool isFadeIn = true;
    private float alpha = 0.0f;

    void Start()
    {
        isFadeIn = true;
        StartCoroutine("MovetoTitleScene");
    }

    void Update()
    {
    }

    IEnumerator MovetoTitleScene()
    {
        if (isFadeIn == true)
        {
            alpha += Time.deltaTime / 5.0f;

            //�t�F�[�h�A�E�g�I������
            if (alpha >= 255.0f)
            {
                isFadeIn = false;
                alpha = 255.0f;
                Debug.Log("aa");
            }

            //�t�F�[�h�pImage�̐F�E�����x�ݒ�
            fadeImage.color = new Color(255.0f, 255.0f, 255.0f, alpha);
        }
        else
        {
            //1�b�҂�
            yield return new WaitForSeconds(1);

            //FadeManager.FadeOut("TitleScene", 0.5f);
        }
    }
}
