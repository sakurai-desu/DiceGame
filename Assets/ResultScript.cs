using UnityEngine;

public class ResultScript : MonoBehaviour {
    /// <summary>
    /// 残りの手数に応じた評価
    /// </summary>
    private int g_troubleNum = 0;
    /// <summary>
    /// 残りの手数の数値
    /// </summary>
    private int g_troubleRemainingNum = 0;
    /// <summary>
    /// ステージ数を保持しているフォルダ
    /// </summary>
    private JsonArray g_jsonArrayScript = null;

    private void Awake() {
        g_jsonArrayScript = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
    }
    /// <summary>
    /// 残りの手数に応じて評価を決定するメソッド
    /// </summary>
    /// <param name="trouble">残りの手かず</param>
    /// <param name="max_eva">最大評価の手数</param>
    /// <param name="mid_eva">真ん中の評価の手数</param>
    /// <param name="row_eva">一番低い評価の手数</param>
    public void Trouble_Eva(int trouble,float max_eva,float mid_eva,float row_eva) {
        g_troubleRemainingNum = trouble;
        if (trouble >= max_eva) {
            Debug.Log("評価"+3);
            g_troubleNum = 3;
        } else if (trouble > mid_eva) {
            Debug.Log("評価" + 2);
            g_troubleNum = 2;
        } else{
            Debug.Log("評価" + 1);
            g_troubleNum = 1;
        }
    }

    /// <summary>
    /// 評価を取得させる
    /// </summary>
    public int Trouble() {
        return g_troubleNum;
    }

    /// <summary>
    /// 残りの手数を取得させる
    /// </summary>
    /// <returns></returns>
    public int GetRemaining() {
        return g_troubleRemainingNum;
    }
}
