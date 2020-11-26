using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stageparameter : MonoBehaviour
{
    StageInformation g_informationScript;
    GetStagename g_getstage_name;
    public string g_stageName = "";

    // Start is called before the first frame update
    void Start()
    {
        g_getstage_name = GameObject.FindGameObjectWithTag("Stage").GetComponent<GetStagename>();
        g_informationScript = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainScene() {
        g_stageName = g_getstage_name.g_get_stagename;
        g_informationScript.g_playStageName = g_stageName;
    }
}
