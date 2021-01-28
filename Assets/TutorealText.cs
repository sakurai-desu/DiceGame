using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorealText : MonoBehaviour
{
    //自信のテキストを取得
    TextMeshProUGUI g_tuto_text;

    //テキストを入れる配列
    [SerializeField]
    private string[] g_text_array ={"移動してーなー","転がしてーなー","何かしたいなー","頑張りたいなー" };

    StageInformation g_info_script;
    // Start is called before the first frame update
    void Start()
    {
        g_tuto_text=GetComponent<TextMeshProUGUI>();
        g_info_script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g_info_script.g_tutorial_num<=3) {

            g_tuto_text.text = g_text_array[g_info_script.g_tutorial_num];
        }
    }
}
