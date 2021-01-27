using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_UI : MonoBehaviour {
    private Fade_In_Out g_fade_Script;
    [SerializeField]
    private GameObject[] g_gameover_UI;
    /// <summary>
    /// 選択中のポインタ
    /// </summary>
    private int g_select_pointer = 0;
    [SerializeField]
    private Color g_default_color;
    [SerializeField]
    private Color g_select_color;

    private bool g_horizontal_flag;

    void Start() {
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
        g_gameover_UI[g_select_pointer].GetComponent<Text>().color = g_select_color;
    }

    void Update() {
        if (Input.GetAxisRaw("Vertical") > 0.5 && g_horizontal_flag == false) {
            //選択中だったテキストの色をデフォルトに戻す
            DontSelect();
            //ポインターを戻す
            g_select_pointer--;
            if (g_select_pointer < 0) {
                g_select_pointer = g_gameover_UI.Length - 1;
            }
            //次に選択するテキストの色を変更する
            ChangeSelect();
            //連続で押せないようにする
            g_horizontal_flag = true;
        }
        if (Input.GetAxisRaw("Vertical") < -0.5 && g_horizontal_flag == false) {
            //選択中だったテキストの色をデフォルトに戻す
            DontSelect();
            //ポインターを進める
            g_select_pointer++;
            if (g_select_pointer > g_gameover_UI.Length - 1) {
                g_select_pointer = 0;
            }
            //次に選択するテキストの色を変更する
            ChangeSelect();
            //連続で押せないようにする
            g_horizontal_flag = true;
        }
        if (Input.GetAxisRaw("Vertical") < 0.5 && Input.GetAxisRaw("Vertical") > -0.5) {
            g_horizontal_flag = false;
        }

        if (Input.GetButtonDown("A")) {
            g_fade_Script.Start_Fade_Out(Select());
        }
    }

    private IEnumerator Select() {
        switch (g_select_pointer) {
            case 0:
                SceneManager.LoadScene("MainScene");
                break;
            case 1:
                SceneManager.LoadScene("SelectScene");
                break;
        }
        yield break;
    }

    /// <summary>
    /// 選択中のテキストの色を変更する
    /// </summary>
    private void ChangeSelect() {
        g_gameover_UI[g_select_pointer].GetComponent<Text>().color = g_select_color;
    }
    /// <summary>
    /// 選択中ではなくなったテキストの色を変更する
    /// </summary>
    private void DontSelect() {
        g_gameover_UI[g_select_pointer].GetComponent<Text>().color = g_default_color;
    }
}
