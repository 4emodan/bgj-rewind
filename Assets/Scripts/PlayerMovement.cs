using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    public UnityEvent OnMoveEvent;
    public UnityEvent OnStopEvent;

    float horizontalMove = 0f;
    bool jump = false;
    private bool isMoving = false;


    private void Start()
    {
        if (OnMoveEvent == null) {
            OnMoveEvent = new UnityEvent();
        }
        if (OnStopEvent == null) {
            OnStopEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Math.Abs(horizontalMove) > 0.1f)
        {
            if (!isMoving) {
                OnMoveEvent.Invoke();
                isMoving = true;
            }
        }
        else if (isMoving)
        {
            OnStopEvent.Invoke();
            isMoving = false;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
