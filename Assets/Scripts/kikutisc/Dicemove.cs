using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dicemove : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Move_dice_plus_v() {
        if (Input.GetKey(KeyCode.W)) {
            Debug.Log("dawdddddddddddd");
        }
    }
    public void Move_dice_minus_v() {
        if (Input.GetKey(KeyCode.S)) {
        }
    }
    public void Move_dice_plus_s() {
        if (Input.GetKey(KeyCode.D)) {
        }
    }
    public void Move_dice_minus_s() {
        if (Input.GetKey(KeyCode.A)) {
        }
    }
}
