using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageInformation : MonoBehaviour
{
    public string[] g_tutorial_name;

    public int g_tutorial_num;

    public string g_playStageName = "";

    Folder_Script g_folder;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        g_folder = GetComponent<Folder_Script>();
        g_tutorial_name = new string[g_folder.Filenum("T*.json")];
        for (int i = 0; i > g_folder.Filenum("T*.json"); i++) {
            g_tutorial_name[i] = "TStage00"+i+".json";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            Debug.Log(g_tutorial_name[1]);
        }
    }
    public void Change_Tutorial(string tutorial) {
        g_playStageName = tutorial;
    }
    public void Change_StageName(string name) {
        g_playStageName = name;
    }
}
