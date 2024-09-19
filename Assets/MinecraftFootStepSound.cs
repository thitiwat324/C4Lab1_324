using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftFootStepSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource minecraftFootStepSound;
    Vector3 inputVec;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = -(Input.GetAxisRaw("Vertical"));
        if (x != 0 || z != 0)//ตรวจสอบการกดลูกศรมีการกดหรือไม่
        {
            minecraftFootStepSound.enabled = true;
        }
        else
        {
            minecraftFootStepSound.enabled = false;
        }
    }
}
