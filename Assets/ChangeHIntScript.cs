using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHIntScript : MonoBehaviour
{
    /// <summary>
    /// ヒント機能のオンオフを判断するためのクラス
    /// </summary>
    private HintScript g_hintScript = null;
    /// <summary>
    /// ヒント機能がついているかどうかを判断するための画像
    /// </summary>
    private Image g_hintImage = null;

    private void Start() {
        g_hintScript = GameObject.Find("Stageinformation").GetComponent<HintScript>();
        g_hintImage = GetComponent<Image>();
    }

    void Update()
    {
        if (g_hintScript.GetHint()) {
            g_hintImage.color = new Color32(255, 255, 0, 255);
        }
        else if (g_hintScript.GetHint()==false) {
            g_hintImage.color = new Color32(60, 60, 0, 255);
        }
    }
}
