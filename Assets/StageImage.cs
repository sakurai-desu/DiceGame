using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StageImage : MonoBehaviour
{
    Image g_image;

    ImageArray g_image_Array;

    ButtonSelect g_button_select_Script;
    // Start is called before the first frame update
    void Start()
    {
        g_image = GetComponent<Image>();
        g_image_Array = GetComponent<ImageArray>();
        g_button_select_Script = GameObject.Find("Stageparent").GetComponent<ButtonSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.streamingAssetsPath + "/" + g_image_Array.g_stage_string[g_button_select_Script.g_var_pointer,g_button_select_Script.g_side_pointer]);
        datastr = reader.ReadToEnd();
        reader.Close();
    }
}
