using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactorMovement : MonoBehaviour
{
    Animator animator;//ให้แสดงท่าทางเดินหรือหยุดนิ่ง
    CharacterController characterController;//ควบคุมตัวละครให้ทำการเคลื่อนที่
    public float speed = 6.0f;//ความเร็วในการเดิน
    public float rotationSpeed = 25f;//ความเร็วในการหมุนตัว
    // Start is called before the first frame update
    public float gravity = 20f;
    Vector3 inputVec;
    Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        float x = -(Input.GetAxisRaw("Vertical"));
        inputVec = new Vector3(x, 0, z);

        animator.SetFloat("InputX",z);
        animator.SetFloat("InputZ",-(x));

        if (x != 0 || z != 0)//ตรวจสอบการกดลูกศรมีการกดหรือไม่
        {
            animator.SetBool("IsRunning", true);//ให้ทำการเคลื่อนไหว
            animator.SetBool("IsMoving", true);//ให้ทำการเคลื่อนไหว
        }
        else
        {
            animator.SetBool("IsRunning", false);//กลับไปท่าพัก
            animator.SetBool("IsMoving", false);//กลับไปท่าพัก
        }
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));//กำหนดแกน x หรือแกน z และความเร็ว
            moveDirection *= speed;
            Debug.Log("Grounded: " + moveDirection);
        }else{
            // moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));//กำหนดแกน x หรือแกน z และความเร็ว
            // moveDirection *= speed;
            Debug.Log("!Grounded: " + moveDirection);
        }
        characterController.Move(moveDirection * Time.deltaTime);//ทำการเคลื่อนไหว
        UpdateMovement();
    }

        void UpdateMovement()
    {
        Vector3 motion = inputVec;
        motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? .7f : 1;
        RotateTowardMovementDirection();
        GetCameraRelativeMovement();
    }

    void RotateTowardMovementDirection()
    {
        if (inputVec != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }

    void GetCameraRelativeMovement()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        targetDirection = h * right + v * forward;
    }
}