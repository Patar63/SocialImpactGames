using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    [Header("Statistics")]
    public SpriteStats _stats;

    [Header("States")]
    public float _currentSpeed;
    public float _currentHP;
    public AttackType _currentAttackType;
    public MoveType _currentMoveType;
    public bool onGround;
    public bool isAttacking;
    public bool alive;

    [Header("Debugging")]
    public float _attackTimer;
    public Vector2 _movement;
    protected Rigidbody2D _rigidbody;
    public Vector2 _spriteSize;
    public float direct;
    protected Animator animator;

    public virtual void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        //gets component from child of game object
        animator = GetComponentInChildren<Animator>();

        alive = true;
        direct = 1f;

        //animates the sprite
        HandleAnimations();
        //sets base state of any sprite
        _spriteSize = animator.transform.localScale;
        _currentSpeed = _stats._speed;
        _currentHP = _stats._maxHP;
    }

    public virtual void Tick()
    {
        HandleAnimations();

        if (!alive)
        {
            return; //exit code if the sprite has died
        }

        //deals with any possible player interactions
        HandleMoving();
        HandleLanding();
        HandleAttacking();
    }

    protected virtual void HandleMoving()
    {
        //allow sprite to rotate correctly
        if (_movement.x > 0f)
        {
            direct = 1f;
        }
        else if (_movement.x < 0f)
        {
            direct = -1f;
        }

        //move the sprite
        transform.Translate(Vector2.right * _movement.x * _currentSpeed * Time.deltaTime);
        //jumping
        if (_movement.y > 0f && onGround)
        {
            _rigidbody.AddForce(Vector3.up * _stats._jumpIntensity);
        }
    }

    protected virtual void HandleLanding()
    {
        Vector3 leftPosition = (Vector3.down * (_stats._rayYOffset * _spriteSize.y)) + (Vector3.left * (_stats._rayXOffset * _spriteSize.x));

        //Casts a vector direction away from a point to see what it collides with
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + leftPosition, Vector3.down, 1f, _stats._groundCheck);

        //find the right side of the Sprite using offsets
        Vector3 rightPosition = (Vector3.down * (_stats._rayYOffset * _spriteSize.y)) + (Vector3.right * (_stats._rayXOffset * _spriteSize.x));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + rightPosition, Vector3.down, 1f, _stats._groundCheck);

        Debug.DrawRay(transform.position + leftPosition, Vector3.down, Color.green);
        Debug.DrawRay(transform.position + rightPosition, Vector3.down, Color.green);

        //checks if player is grounded
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    protected virtual void HandleAnimations()
    {
        animator.SetFloat(AnimationStates.xmovement, Mathf.Abs(_movement.x));
        animator.SetFloat(AnimationStates.direction, direct);
        animator.SetBool(AnimationStates.onground, onGround);
        animator.SetBool(AnimationStates.alive, alive);
        animator.SetBool(AnimationStates.attacking, isAttacking);
    }

    protected virtual void HandleAttacking()
    {
        //don't attack without input from player
        if (!isAttacking)
        {
            return;
        }

        //increment attack timer for the three hit
        _attackTimer += Time.deltaTime;

        //only allow certain follow ups if _attackTimer is within the time limit of _timeBetweenSwings
        /* ADD IF NEEDED
         * if (_attackTimer >= _stats._timeBetweenSwings)
        {
        }*/

    }

    public virtual void TakeDamage(float hitDamage)
    {
        if (!alive)
        {
            return;
        }
        //make sure there's no negative HP
        _currentHP = Mathf.Clamp(_currentHP - hitDamage, 0f, _stats._maxHP);

        //call kill code for a sprite
        if (_currentHP == 0)
        {
            HandleDeath();
        }
    }

    protected virtual void HandleDeath()
    {
        alive = false;
        //will add more later if death is needed
    }
}
