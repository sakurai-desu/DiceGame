using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optionselect : MonoBehaviour
{
    [SerializeField]
    GameObject[] g_option;

    [SerializeField]
    int g_option_pointer;

    bool g_horizontal_flag;

    [SerializeField]
    string g_scene_name;

    //スタートボタンが押されたかどうかを判断するスクリプト
    PushStartScri g_pushStart_Script;

    void Start()
    {
        g_option[0].GetComponent<Image>().color = Color.red;

        g_pushStart_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();
    }

    void Update() {
        if (g_pushStart_Script.g_start_flag) {
            if (Input.GetAxisRaw("Vertical") > 0.5 && g_horizontal_flag == false) {
                ChangeSelect();
                if (g_option_pointer > 0) {
                    g_option_pointer--;
                } else if (g_option_pointer == 0) {
                    g_option_pointer = g_option.Length - 1;
                }
                DontSelect();
                g_horizontal_flag = true;
            }
            if (Input.GetAxisRaw("Vertical") < -0.5 && g_horizontal_flag == false) {
                ChangeSelect();
                if (g_option_pointer < g_option.Length - 1) {
                    g_option_pointer++;
                } else if (g_option_pointer == g_option.Length - 1) {
                    g_option_pointer = 0;
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

                        break;
                    case 1:
                        g_option[g_option_pointer].GetComponent<OptionText>().SelectScene(g_scene_name);
                        break;
                    case 2:
                        g_option[g_option_pointer].GetComponent<OptionText>().SelectScene("SelectScene");
                        break;
                    case 3:

                        break;
                }
            }
        }
    }
    void ChangeSelect() {
        g_option[g_option_pointer].GetComponent<Image>().color = Color.white;
    }
    void DontSelect() {
        g_option[g_option_pointer].GetComponent<Image>().color = Color.red;
    }
}
