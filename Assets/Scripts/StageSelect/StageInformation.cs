using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInformation : MonoBehaviour
{
    public string[] g_tutorial_name;

    public int g_tutorial_num;

    public string g_playStageName = "";
    /// <summary>
    /// 現在どのステージにいるのかを取得
    /// </summary>
    private int g_arrayPointerNum = 0;
    /// <summary>
    /// 横列の数(現在は４)
    /// </summary>
    private const int g_sideNum = 4;
    Folder_Script g_folder;
    /// <summary>
    /// 評価表示のスクリプト
    /// </summary>
    private StaerScript g_staerScript = null;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        g_folder = GetComponent<Folder_Script>();
    }

    void Update()
    {
        //ステージを選択するシーンの時
        if (SceneManager.GetActiveScene().name == "SelectScene") {
            g_staerScript = GameObject.Find("SterParentObject").GetComponent<StaerScript>();
            g_staerScript.SetStaer(g_arrayPointerNum);
        }
    }
    /// <summary>
    /// チュートリアルに飛ぶようにする
    /// </summary>
    /// <param name="tutorial">最初のチュートリアルの名前</param>
    public void Change_Tutorial(string tutorial) {
        g_playStageName = tutorial;
    }
    /// <summary>
    /// 選択したステージに飛ぶようにする
    /// </summary>
    /// <param name="name">選択したステージの名前</param>
    public void Change_StageName(string name) {
        g_playStageName = name;
    }
    /// <summary>
    /// 選択状態のステージがステージ何なのかを設定させる
    /// </summary>
    /// <param name="stagenum">選択しているポインターの状態</param>
    public void Change_StageNum(int stageVerNum, int stageSideNum) {
        //選択したステージが一段目の時
        if (stageVerNum > 0) {
                stageSideNum = (stageVerNum*g_sideNum)+stageSideNum+1;
        } else {
          stageSideNum++;
        }
          g_arrayPointerNum = stageSideNum;
        Debug.Log(g_arrayPointerNum+"ステージ目");
    }
    /// <summary>
    /// 何個目のステージが選択されたのかを表示する
    /// </summary>
    /// <returns>ステージのナンバー</returns>
    public int Get_StageNum() {
        return g_arrayPointerNum;
    }
}
