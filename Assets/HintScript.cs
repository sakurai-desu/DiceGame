using UnityEngine;
using UnityEngine.SceneManagement;

public class HintScript : MonoBehaviour
{
    /// <summary>
    /// ヒントを表示するかしないかを管理するフラグ
    /// </summary>
    private bool g_hintFlag = true;

    void Update()
    {
        //ステージセレクト画面で指定されたボタンが押されたときにフラグを切り替える
        if (SceneManager.GetActiveScene().name == "SelectScene") 
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Y"))
            {
                if (g_hintFlag == false) {
                    g_hintFlag = true;
                } else if (g_hintFlag) {
                    g_hintFlag = false;
                }
            } 
        }
      
    }

    /// <summary>
    /// ヒント機能のフラグを返す
    /// </summary>
    /// <returns>サイコロに色を付けるかどうかのフラグ</returns>
    public bool GetHint() {
        return g_hintFlag;
    }
}
