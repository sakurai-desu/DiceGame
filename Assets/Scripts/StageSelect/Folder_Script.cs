using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Folder_Script : MonoBehaviour
{

    [SerializeField]
    public DirectoryInfo[] g_dirarray;
    [SerializeField]
    public FileInfo[] g_info;

    [SerializeField]
    int g_pathnum;

    DirectoryInfo g_dir;

    JsonArray g_json_array;

    private int g_stagearray_pointer;

    ImageArray g_image;
    void Start()
    {


    }
    /// <summary>
    /// ファイル数を数える
    /// </summary>
    public int Filenum(string folderName) {
        g_pathnum = 0;
        g_json_array = GetComponent<JsonArray>();
        if (SceneManager.GetActiveScene().name == "SelectScene") {
            g_image = GameObject.Find("StageImage").GetComponent<ImageArray>();
        }
        //jsonを探す場合
        if (folderName == "*.json") {
            //指定した名前のフォルダ取得
            g_dir = new DirectoryInfo(Application.streamingAssetsPath);
        }
        //jpgを探す場合
        else if (folderName == "*.jpg") {
            //指定した名前のフォルダ取得
            g_dir = new DirectoryInfo(Application.streamingAssetsPath+"/Image/");
        }
        //指定したフォルダの中のフォルダーをすべて取得
        g_dirarray = g_dir.GetDirectories("*");
        //フォルダの名前を表示
        foreach (DirectoryInfo d in g_dirarray)
        {
            //Debug.Log(d.Name);
        }
        //指定したフォルダの名前指定
        g_info = g_dir.GetFiles(folderName);
        //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info)
        {
            g_pathnum++;
            //Debug.Log(g_pathnum);
            //Debug.Log(f.Name);
        }
        return g_pathnum;
    }
    /// <summary>
    /// 指定した形式のファイルを配列に入れる
    /// </summary>
    public void Filename(string filename) {
        g_stagearray_pointer = 0;
        //json形式を検索しようとしていた場合
        if (filename == "*.json") {
         //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info) {
            g_json_array.g_json_stage[g_stagearray_pointer] = f.Name;
             g_stagearray_pointer++;
         }
        }else　if (filename == "*.jpg") {
         //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info) {
                g_image.g_stage_string[g_stagearray_pointer] = f.Name;
             g_stagearray_pointer++;
         }
        }
      
    }
    
}
