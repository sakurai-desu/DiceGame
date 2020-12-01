using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInformation : MonoBehaviour
{
    public string g_playStageName = "";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_StageName(string name) {
        g_playStageName = name;
    }
}
