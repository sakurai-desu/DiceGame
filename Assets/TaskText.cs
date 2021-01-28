using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskText : MonoBehaviour
{
    TextMeshProUGUI g_task;

    [SerializeField]
    string[] g_task_array=default;

    StageInformation g_stage_info;
    // Start is called before the first frame update
    void Start()
    {
        g_task = GetComponent<TextMeshProUGUI>();
        g_stage_info = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (g_stage_info.g_tutorial_num <= 3) {
        g_task.text = g_task_array[g_stage_info.g_tutorial_num];
        }
        
    }
}
