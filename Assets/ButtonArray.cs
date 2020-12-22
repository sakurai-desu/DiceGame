using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonArray : MonoBehaviour
{
    //入力を検知するキーの名前
    private const string g_axisName = "Vertical";

    //ステージを移動したりするやつを入れておく
    [SerializeField]
    GameObject[] g_button_obj_array;

    //検索用ポインター
    [SerializeField]
    int g_button_pointer;

    StageInformation g_information_script;

    SelectScript g_select;

    //スティックが倒されたときに一回だけ処理を行うためのフラグ
    bool g_array_move_flag;
    // Start is called before the first frame update
    void Start()
    {
        g_information_script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Back")||Input.GetKeyDown(KeyCode.Escape)) {
            Exit();
        }
        //スティックを上に倒したとき
        if (Input.GetAxisRaw(g_axisName) >0.5&&g_array_move_flag==false) {
            g_array_move_flag = true;
            g_button_obj_array[g_button_pointer].GetComponent<SelectScript>().DontSelect();
            //上のボタンを選択する
            if (g_button_pointer != 0) {
                g_button_pointer--;
                g_button_obj_array[g_button_pointer].GetComponent<SelectScript>().SelectCoror();
            }
            //上のボタンが一番上だった時に一番下のボタンを選択する
            else  {
                g_button_pointer = g_button_obj_array.Length-1;
            }
        }
        //スティックを下に倒したとき
        if (Input.GetAxisRaw(g_axisName) <-0.5&&g_array_move_flag==false) {
            g_array_move_flag = true;

            g_button_obj_array[g_button_pointer].GetComponent<SelectScript>().DontSelect();
            //下のボタンを選択する
            if (g_button_pointer != g_button_obj_array.Length-1) {
                g_button_pointer++;

                g_button_obj_array[g_button_pointer].GetComponent<SelectScript>().SelectCoror();
            }
            //下のボタンが一番上だった時に一番下のボタンを選択する
            else {
                g_button_pointer = 0;
            }
        }
        //スティックがニュートラルの時
        if (-0.5<Input.GetAxisRaw(g_axisName) && Input.GetAxisRaw(g_axisName) < 0.5) {
            g_array_move_flag = false;
        }
        //選択されている配列内に入っているオブジェクトを取得してその中のスクリプト内のメソッドを呼び出す
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A")) {
            //チュートリアル以外のボタンを押したとき
            if (g_button_pointer != 1) {
                ClickMesod();
            }//チュートリアル以外のボタンを押したとき
           else if (g_button_pointer == 1) {
                g_information_script.g_playStageName = "/Tutorial/TStage000.json";
                g_information_script.g_tutorial_num = 0;
                ClickMesod();
            }
   
        }
    }
    void ClickMesod() {
         g_button_obj_array[g_button_pointer].GetComponent<SelectScript>().OcClick();
    }

    private void Exit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif 
    }
}
