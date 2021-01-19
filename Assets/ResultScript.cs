using UnityEngine;

public class ResultScript : MonoBehaviour {
    /// <summary>
    /// 残りの手数
    /// </summary>
    private int g_troubleNum = 0;
    /// <summary>
    /// 残りの手数に応じて評価を決定するメソッド
    /// </summary>
    /// <param name="trouble">残りの手かず</param>
    /// <param name="max_eva">最大評価の手数</param>
    /// <param name="mid_eva">真ん中の評価の手数</param>
    /// <param name="row_eva">一番低い評価の手数</param>
    public void Trouble_Eva(int trouble,float max_eva,float mid_eva,float row_eva) {
        if (trouble > max_eva) {
            Debug.Log("評価"+3);
            g_troubleNum = 3;
        } else if (max_eva>trouble&&trouble > mid_eva) {
            Debug.Log("評価" + 2);
            g_troubleNum = 2;
        } else if (mid_eva>trouble) {
            Debug.Log("評価" + 1);
            g_troubleNum = 1;
        }
    }

    /// <summary>
    /// 手数を取得させる
    /// </summary>
    public int Trouble() {
        return g_troubleNum;
    }
}
