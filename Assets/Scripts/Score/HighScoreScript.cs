using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    /// <summary>
    /// リザルトに必要な数値が入っているスクリプト
    /// </summary>
    private ResultScript g_resultScript=null;
    /// <summary>
    /// ハイスコアを表示させるためのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject g_highScoreText = null;
    /// <summary>
    /// スコアを入れるメソッドが入ってるスクリプト
    /// </summary>
    private ScoreJsonScript g_scoreJsonScript = null;
    /// <summary>
    /// 何番目のステージなのかを取得できるスクリプト
    /// </summary>
    private StageInformation g_stageInformationScript = null;

    private void Start() {
        g_resultScript = GameObject.Find("Stageinformation").GetComponent<ResultScript>();
        g_highScoreText = GameObject.Find("high_move_para");
        g_scoreJsonScript = GameObject.Find("ScoreInformation").GetComponent<ScoreJsonScript>();
        g_stageInformationScript = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
        //前の残りて数が現在の残りて数よりも少なかった場合
        if (g_scoreJsonScript.g_stageScore.g_stageInfo[g_stageInformationScript.Get_StageNum()].g_trouble < g_resultScript.GetRemaining()) {
        //ハイスコアを変更させる
        g_highScoreText.GetComponent<TextMeshProUGUI>().text = g_resultScript.GetRemaining().ToString();
        }
    }
}
