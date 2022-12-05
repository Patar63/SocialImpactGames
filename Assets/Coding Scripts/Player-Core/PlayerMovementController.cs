using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{


    PlayerInputAction inputActions;
    Vector2 moveVec;

    [SerializeField] private float runSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;

    private Rigidbody2D _rigidbody;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Transform botLeft;
    [SerializeField] private Transform botRight;
    [SerializeField] private float distanceToGround;
    private float checkFreeTimer = 0.5f;

    private Animator _animator;

    private bool isWalking = false;
    [SerializeField] private int jumpingTime = 0;
    [SerializeField] private int attackTime = 0;
    private float doubleAttackTimer = 1.0f;


    private void Awake()
    {
        inputActions = PlayerInputController.instance.inputAction;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        inputActions.Player.Move.performed += context => Move(context.ReadValue<Vector2>());
        inputActions.Player.Move.canceled += context => Move(Vector2.zero);

        inputActions.Player.Jump.performed += context => Jump();

        inputActions.Player.Attack.performed += context => Attack();
    }

    private void GroundCheck()
    {
        bool leftGrounded = Physics2D.Raycast(botLeft.position, -Vector2.up, distanceToGround);
        bool rightGrounded = Physics2D.Raycast(botRight.position, -Vector2.up, distanceToGround);

        isGrounded = (leftGrounded && rightGrounded);

        if(jumpingTime != 0)
        {
            if(checkFreeTimer > 0)
            {
                checkFreeTimer -= Time.deltaTime;
            }
            else
            {
                if(isGrounded)
                {
                    checkFreeTimer = 0.5f;
                    jumpingTime = 0;
                    _animator.SetInteger("JumpingTime", jumpingTime);
                }
            }
        }
        
    }

    private void Move(Vector2 moveVecIn)
    {
        moveVec = moveVecIn;

        isWalking = moveVec.x != 0;
        _animator.SetBool("isRunning", isWalking);
        if (moveVec.x > 0)
        {
            this.transform.localScale = new Vector2(Math.Abs(this.transform.localScale.x) * 1.0f, this.transform.localScale.y);
        }
        if (moveVec.x < 0)
        {
            this.transform.localScale = new Vector2(Math.Abs(this.transform.localScale.x) * -1.0f, this.transform.localScale.y);
        }

        
    }
    private void Jump()
    {
        if(jumpingTime == 0)
        {
            if(isGrounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
                isGrounded = false;
                jumpingTime = 1;
                _animator.SetInteger("JumpingTime", jumpingTime);
            }
        }
        else if(jumpingTime == 1)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            isGrounded = false;
            jumpingTime = 2;
            _animator.SetInteger("JumpingTime", jumpingTime);
        }
    }

    private void Attack()
    {
        doubleAttackTimer = 1.0f;
        if (attackTime == 0)
        {
            if (isGrounded)
            {
                attackTime = 1;
                _animator.SetInteger("AttackTime", attackTime);
            }
        }
        else if (attackTime == 1)
        {
            attackTime = 2;
            _animator.SetInteger("AttackTime", attackTime);
        }
        else if (attackTime == 2)
        {
            attackTime = 0;
            _animator.SetInteger("AttackTime", attackTime);
        }
    }

    private void AttackCheck()
    {
        if(attackTime != 0)
        {
            if(doubleAttackTimer > 0)
            {
                doubleAttackTimer -= Time.deltaTime;
            }
            else
            {
                attackTime = 0;
                _animator.SetInteger("AttackTime", attackTime);
                doubleAttackTimer = 1.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        
        
        transform.Translate(Vector2.right * moveVec.x * Time.deltaTime * runSpeed, Space.Self);

        AttackCheck();
        GroundCheck();
    }
}
