using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorealAnim : MonoBehaviour
{
    Animator g_tuto_anim;
    // Start is called before the first frame update
    void Start()
    {
        g_tuto_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            g_tuto_anim.SetBool("EndFlag", true);
        }
    }
}
