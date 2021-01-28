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
    /// 何番目のステージなのかを取得できるスクリプト
    /// </summary>
    private StageInformation g_stageInformationScript = null;
    /// <summary>
    /// ステージの情報が格納されているスクリプト
    /// </summary>
    private const string g_stageInfoName = "Stageinformation";
    void Start()
    {
        #region スクリプトの取得
        g_resultScript = GameObject.Find(g_stageInfoName).GetComponent<ResultScript>();
        g_jsonArrayScript = GameObject.Find(g_stageInfoName).GetComponent<JsonArray>();
        g_stageInformationScript = GameObject.Find(g_stageInfoName).GetComponent<StageInformation>();
        g_scoreJsonScript = GameObject.Find("ScoreInformation").GetComponent<ScoreJsonScript>();
        #endregion
        if (g_scoreJsonScript.g_stageScore.g_stageInfo[g_stageInformationScript.Get_StageNum()].g_evaluation < g_resultScript.Trouble()) {
        //jsonに数値を入れる
        g_scoreJsonScript.ChangeInfo(g_stageInformationScript.Get_StageNum(), g_resultScript.Trouble(), g_resultScript.GetRemaining());
        }
    }
}
