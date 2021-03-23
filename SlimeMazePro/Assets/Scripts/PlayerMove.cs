using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    public CharacterController characterController;
    public float speed = 5.0f;
    public Animator anim;

    void Update()
    {
        Move();
        Rotate();
    }

    public void Move()
    {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        anim.SetBool("isWalking", vMove != 0 || hMove != 0);

        Vector3 move = transform.forward * vMove + transform.right * hMove;
        characterController.Move(speed * Time.deltaTime * move);
    }

    public void Rotate()
    {
        float hRotate = Input.GetAxis("Mouse X");
        float vRotate = Input.GetAxis("Mouse Y");

        transform.Rotate(0, hRotate * mouseSensitivity, 0);
        cameraHolder.Rotate(-vRotate * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
}
