using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    void Start()
    {


    }
    /// <summary>
    /// ファイル数を数える
    /// </summary>
    public int Filenum() {
        g_json_array = GetComponent<JsonArray>();
        //指定した名前のフォルダ取得
       g_dir = new DirectoryInfo(Application.streamingAssetsPath);

        //指定したフォルダの中のフォルダーをすべて取得
        g_dirarray = g_dir.GetDirectories("*");
        //フォルダの名前を表示
        foreach (DirectoryInfo d in g_dirarray)
        {
            //Debug.Log(d.Name);
        }
        //指定したフォルダの名前指定
        g_info = g_dir.GetFiles("*.json");
        //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info)
        {
            g_pathnum++;
            Debug.Log(g_pathnum);
            Debug.Log(f.Name);
        }
        return g_pathnum;
    }
    /// <summary>
    /// 指定した形式のファイルを配列に入れる
    /// </summary>
    public void Filename() {

        //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info) {
            g_json_array.g_json_stage[g_stagearray_pointer] = f.Name;
             g_stagearray_pointer++;
        }
    }
    
}
