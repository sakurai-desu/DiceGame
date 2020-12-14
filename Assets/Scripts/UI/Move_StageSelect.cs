using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_StageSelect : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetButtonDown("Back")) {
        //    StageSelect();
        //}
    }

    public void StageSelect() {
        SceneManager.LoadScene("SelectScene");
    }
}
