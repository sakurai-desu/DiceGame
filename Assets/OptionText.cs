using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SelectScene(string scenemane) {
        SceneManager.LoadScene(scenemane);
    }
  
}
