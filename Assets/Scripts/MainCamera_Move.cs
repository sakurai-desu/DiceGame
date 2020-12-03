using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject[] g_camera_Array;
    [SerializeField]
    private int g_camera_pointer;

    private GameObject g_this_Obj;

    void Start()
    {
        g_this_Obj = this.gameObject;
        g_camera_pointer = 0;
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Change_Right_Camra_Pos();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Change_Left_Camra_Pos();
        }
    }

    private void Change_Left_Camra_Pos() {
        g_camera_pointer++;
        if (g_camera_pointer>=g_camera_Array.Length) {
            g_camera_pointer = 0;
        }
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    private void Change_Right_Camra_Pos() {
        g_camera_pointer--;
        if (g_camera_pointer < 0) {
            g_camera_pointer = g_camera_Array.Length-1;
        }
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    private void Local_PosAndRotation_Reset() {
        g_this_Obj.transform.localPosition= new Vector3(0, 0, 0);
        g_this_Obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
