using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionselect : MonoBehaviour {
    [SerializeField]
    GameObject[] g_option;

    [SerializeField]
    int g_option_pointer;

    //オプションパネル本体の角度の数値
    [SerializeField]
    float g_rotateController;

    //0 = 上移動,1 = 停止,2 = 下移動のカーソルフラグ
    [SerializeField]
    int g_rotate_flag;

    bool g_horizontal_flag;

    [SerializeField]
    string g_scene_name;

    //スタートボタンが押されたかどうかを判断するスクリプト
    PushStartScri g_pushStart_Script;

    //画像データ
    [SerializeField]
    Sprite[] g_unenableImage;
    [SerializeField]
    Sprite[] g_enableImage;

    void Start() {
        //画像コンポーネントを格納
        g_option[0].GetComponent<Image>().sprite = g_enableImage[0];

        g_pushStart_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();


    }

    void Update() {
        if (g_pushStart_Script.g_start_flag) {
            if (Input.GetAxisRaw("Vertical") > 0.5 && g_horizontal_flag == false) {
                ChangeSelect();
                if (g_option_pointer > 0) {
                    g_option_pointer--;
                    g_rotate_flag = 0;
                } else if (g_option_pointer == 0) {
                    g_option_pointer = g_option.Length - 1;
                    g_rotate_flag = 0;
                }
                DontSelect();
                g_horizontal_flag = true;
            }
            if (Input.GetAxisRaw("Vertical") < -0.5 && g_horizontal_flag == false) {
                ChangeSelect();
                if (g_option_pointer < g_option.Length - 1) {
                    g_option_pointer++;
                    g_rotate_flag = 2;
                } else if (g_option_pointer == g_option.Length - 1) {
                    g_option_pointer = 0;
                    g_rotate_flag = 2;
                }
                DontSelect();
                g_horizontal_flag = true;
            }
            if (Input.GetAxisRaw("Vertical") < 0.5 && Input.GetAxisRaw("Vertical") > -0.5) {
                g_horizontal_flag = false;
            }
            if (Input.GetButtonDown("A")) {
                switch (g_option_pointer) {
                    case 0:
                        g_option[g_option_pointer].GetComponent<OptionText>().SelectScene(g_scene_name);
                        break;
                    case 1:
                        g_option[g_option_pointer].GetComponent<OptionText>().SelectScene("SelectScene");
                        break;
                    case 2:
                        break;
                    case 3:

                        break;
                }
            }
        } 
        
        //オプション選択時に角度を変え、カーソルの位置を合わせる処理
        if (g_rotateController <= -45 * g_option_pointer || g_rotateController >= -45 * g_option_pointer) {
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, -45 * g_option_pointer);
            g_rotate_flag = 1;

            //if (g_option_pointer == 0) {
            //    g_rotateController = 0;
            //}
        }

        if (g_rotate_flag == 2) {
            g_rotateController -= 540 * Time.deltaTime;
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, g_rotateController);
        }
        else if (g_rotate_flag == 0) {
            g_rotateController += 540 * Time.deltaTime;
            this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, g_rotateController);
        }
    }
    void ChangeSelect() {
            g_option[g_option_pointer].GetComponent<Image>().sprite = g_unenableImage[g_option_pointer];
        }
        void DontSelect() {
            g_option[g_option_pointer].GetComponent<Image>().sprite = g_enableImage[g_option_pointer];
        }
    }
