using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScript : MonoBehaviour
{
    TroubleScr g_trouble_Script;

    int g_trouble_num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 残りの手数に応じて評価を決定するメソッド
    /// </summary>
    /// <param name="trouble">残りの手かず</param>
    /// <param name="max_eva">最大評価の手数</param>
    /// <param name="mid_eva">真ん中の評価の手数</param>
    /// <param name="row_eva">一番低い評価の手数</param>
    public void Trouble_Eva(int trouble,float max_eva,float mid_eva,float row_eva) {
        if (trouble > row_eva) {
            Debug.Log("評価"+1);
        } else if (row_eva>trouble&&trouble > mid_eva) {
            Debug.Log("評価" + 2);
        } else if (mid_eva>trouble&&trouble > max_eva) {
            Debug.Log("評価" + 3);
        }

    }
}
