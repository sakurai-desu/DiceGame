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
    void Start()
    {
        //指定した名前のフォルダ取得
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);

        //指定したフォルダの中のフォルダーをすべて取得
        g_dirarray = dir.GetDirectories("*");
        //フォルダの名前を表示
        foreach (DirectoryInfo d in g_dirarray)
        {
            //Debug.Log(d.Name);
        }

        //指定したフォルダの名前指定
        g_info = dir.GetFiles("*.json");
        //指定した名前のファイルの名前表示
        foreach (FileInfo f in g_info)
        {
            g_pathnum++;
            Debug.Log(g_pathnum);
            Debug.Log(f.Name);
        }
    }
    
}
