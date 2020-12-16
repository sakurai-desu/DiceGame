using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour
{
    [SerializeField]
    string g_select_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("B")) {
            OcClick();
        }
    }
    public void OcClick() {
        SceneManager.LoadScene(g_select_name);
    }
}
