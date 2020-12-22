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

    StageInformation g_info_Script;
    void Start()
    {
        g_info_Script = GetComponent<StageInformation>();

    }
    /// <summary>
    /// ファイル数を数える
    /// </summary>
    public int Filenum(string folderName) {
        g_pathnum = 0;
        g_json_array = GetComponent<JsonArray>();
        //jpgを探す場合
        if (folderName == "T*.json") {
            //指定した名前のフォルダ取得
            g_dir = new DirectoryInfo(Application.streamingAssetsPath+ "/Tutorial/");
        }
        //jsonを探す場合
        else if (folderName == "*.json") {
            //指定した名前のフォルダ取得
            g_dir = new DirectoryInfo(Application.streamingAssetsPath);
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
    int g_array_pointer;
    /// <summary>
    /// 指定した形式のファイルを配列に入れる
    /// </summary>
    public void Filename(string filename) {
        g_stagearray_pointer = 0;
        g_array_pointer = 0;
            //指定した名前のファイルの名前表示
            foreach (FileInfo f in g_info) {
                g_json_array.g_json_stage[g_stagearray_pointer] = f.Name;
                g_stagearray_pointer++;
            }
    }
}
