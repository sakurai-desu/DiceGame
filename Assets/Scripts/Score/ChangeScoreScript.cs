using UnityEngine;

public class ChangeScoreScript : MonoBehaviour
{
    /// <summary>
    /// 評価と手数が入っているスクリプト
    /// </summary>
    private ResultScript g_resultScript = null;
    /// <summary>
    /// .jsonフォルダを数えるスクリプト
    /// </summary>
    private JsonArray g_jsonArrayScript=null;
    /// <summary>
    /// スコアを入れるメソッドが入ってるスクリプト
    /// </summary>
    private ScoreJsonScript g_scoreJsonScript = null;
    /// <summary>
    /// ステージの情報が格納されているスクリプト
    /// </summary>
    private const string g_stageInfoName = "Stageinformation";
    void Start()
    {
        g_resultScript = GameObject.Find(g_stageInfoName).GetComponent<ResultScript>();
        g_jsonArrayScript = GameObject.Find(g_stageInfoName).GetComponent<JsonArray>();
        g_scoreJsonScript = GameObject.Find("ScoreInformation").GetComponent<ScoreJsonScript>();
        //jsonに数値を入れる
            g_scoreJsonScript.ChangeInfo(0, g_resultScript.Trouble(), g_resultScript.GetRemaining());
    }
}
