using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public Transform respawnPt;

    private float speedMultiplier = 1f;
    private Animator anim;
    private CharacterController controller;
    private Vector3 lastPosition;
    private Vector3 playerVelocity;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        gameObject.transform.position = respawnPt.position;
    }

    void Update()
    {
        //Movement
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed * speedMultiplier);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += -9.81f * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Speed is the distance between my current position - last position over time
        float speed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;

        anim.SetFloat("Speed", speed);
        lastPosition = this.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("You died");
            anim.SetTrigger("Dead");
            gameObject.transform.position = respawnPt.position;
            GetComponent<CharacterController>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (GetComponent<CharacterController>().enabled == false)
        {
            GetComponent<CharacterController>().enabled = true;
        }
    }
}
