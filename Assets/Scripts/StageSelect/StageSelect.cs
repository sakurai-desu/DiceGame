using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    StageInformation g_informationScript;

    public string g_stageName = "";
    // Start is called before the first frame update
    void Start()
    {
        g_informationScript = GameObject.Find("StageInformation").GetComponent<StageInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainScene() {
        g_informationScript.g_playStageName = g_stageName;
    }
}
