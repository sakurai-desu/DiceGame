using UnityEngine;

public class FPSScript : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
