using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Clear : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void This_Stage_Clear() {
        Move_StageSelect();
    }

    private void Move_StageSelect() {
        SceneManager.LoadScene("SelectScene");
    }
}
