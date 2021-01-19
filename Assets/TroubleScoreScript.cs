using UnityEngine;
using TMPro;

public class TroubleScoreScript : MonoBehaviour
{
    /// <summary>
    /// 手数が入ったスクリプト
    /// </summary>
    private ResultScript g_troubleScript = null;
    /// <summary>
    /// 手数を表示するためのテキスト
    /// </summary>
    private TextMeshProUGUI g_troubleText = null;

    private void Start() {
        g_troubleScript = GameObject.Find("Stageinformation").GetComponent<ResultScript>();
        g_troubleText=this.GetComponent<TextMeshProUGUI>();
        g_troubleText.text = g_troubleScript.GetRemaining().ToString();
    }
}
