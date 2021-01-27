using UnityEngine;
using UnityEngine.UI;

public class ResultObjCreate : MonoBehaviour
{
    /// <summary>
    /// 評価を表す際に使用するオブジェクト
    /// </summary>
    public GameObject[] g_troubleObjct = null;
    /// <summary>
    /// 評価をあらわすオブジェクトの色を変えるか目に使用
    /// </summary>
    public Sprite[] g_troubleImage = null;
    /// <summary>
    /// リザルトの数値を持っているスクリプト
    /// </summary>
    private ResultScript g_resultScript = null;

    private void Start()
    {
        g_resultScript = GameObject.Find("Stageinformation").GetComponent<ResultScript>();
        GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>().GameStart_Fade_In();
        CreateObj();
    }

    /// <summary>
    /// 評価を表すオブジェクトを生成する
    /// </summary>
    private void CreateObj()
    {
        //評価に応じた回数行う
        for (int i = 0; i < g_resultScript.Trouble(); i++) {
                    g_troubleObjct[i].GetComponent<Image>().sprite = g_troubleImage[g_resultScript.Trouble()-1];
        }
        //評価に使用しないものの色を変更する
        for (int i = g_resultScript.Trouble(); i < g_troubleObjct.Length; i++) {
                    g_troubleObjct[i].GetComponent<Image>().color = new Color32(48,48,48,255);
        }
    }
}
