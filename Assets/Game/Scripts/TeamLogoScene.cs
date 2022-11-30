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

            //フェードアウト終了判定
            if (alpha >= 255.0f)
            {
                isFadeIn = false;
                alpha = 255.0f;
                Debug.Log("aa");
            }

            //フェード用Imageの色・透明度設定
            fadeImage.color = new Color(255.0f, 255.0f, 255.0f, alpha);
        }
        else
        {
            //1秒待つ
            yield return new WaitForSeconds(1);

            //FadeManager.FadeOut("TitleScene", 0.5f);
        }
    }
}
