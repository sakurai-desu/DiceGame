using UnityEngine;

public class ResultObjCreate : MonoBehaviour
{
    /// <summary>
    /// 評価を表す際に使用するオブジェクト
    /// </summary>
    public GameObject g_troubleObjct = null;
    /// <summary>
    /// リザルトの数値を持っているスクリプト
    /// </summary>
    private ResultScript g_resultScript = null;
    /// <summary>
    /// 元のポジション（X）
    /// </summary>
    public float g_originPosX = 0f;
    private const float g_posY = 5f;
    private void Start()
    {
        g_resultScript = GameObject.Find("Stageinformation").GetComponent<ResultScript>();
        CreateObj();
    }

    /// <summary>
    /// 評価を表すオブジェクトを生成する
    /// </summary>
    private void CreateObj()
    {
        for (int i = 0; i < g_resultScript.Trouble(); i++) {
            GameObject trouble = Instantiate(g_troubleObjct);
            trouble.transform.position = new Vector3(g_originPosX + (i * 10), g_posY, 0f);
            //親を決める
            trouble.transform.parent = gameObject.transform;
        }
    }
}
