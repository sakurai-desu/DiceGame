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
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Change_Tutorial(string tutorial) {
        g_playStageName = tutorial;
    }
    public void Change_StageName(string name) {
        g_playStageName = name;
    }
}
