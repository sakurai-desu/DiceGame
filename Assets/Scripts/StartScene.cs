using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    private void Start() {
        SceneManager.LoadScene("TitleScene");
    }
}
