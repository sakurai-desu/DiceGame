using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageArray : MonoBehaviour
{
    //ステージの画像
    Image g_stage_image;

    //ステージ名を入れていく配列
    public string[] g_stage_string;

    //ストリーミングアセットから読み出すスクリプト
    Folder_Script g_streaming_folder;
    // Start is called before the first frame update
    void Start()
    {
        g_stage_image = GetComponent<Image>();
        g_streaming_folder = GameObject.Find("Stageinformation").GetComponent<Folder_Script>();
        g_stage_string = new string[g_streaming_folder.Filenum("*.jpg")];
        g_streaming_folder.Filename("*.jpg");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
