using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetStagename : MonoBehaviour
{
    private StageInformation g_info_Script;
    public string g_get_stagename = "";
    // Start is called before the first frame update
    void Start()
    {
        g_info_Script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick() {
        g_info_Script.Change_StageName(g_get_stagename);
        //this.gameObject.transform.root.GetComponent<Stageparameter>().MainScene();
        SceneManager.LoadScene("MainScene");
    }
}
