using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Get_Stage() {
        this.gameObject.transform.root.GetComponent<Stageparameter>().MainScene();
    }
}
