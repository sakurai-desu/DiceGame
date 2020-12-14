using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageReset : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetButtonDown("Start")) {
        //    Resetstage();
        //}
        
    }
    public void Resetstage() {
        SceneManager.LoadScene("MainScene");
    }
}
