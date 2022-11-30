using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Social Impact/Sprite Stats")]

public class SpriteStats : ScriptableObject
{
    [Header("General")]
    public SpriteType _spriteType;
    public float _maxHP;

    [Header("Attack")]
    public AttackType _defaultsword;
    public float _timeBetweenSwings;
    public Vector2 _hitboxOffset;

    [Header("Movement")]
    public MoveType _defaultMovement;
    public float _speed;
    public float _jumpIntensity;
    public LayerMask _groundCheck;
    public float _rayXOffset = 0.1f;
    public float _rayYOffset = 0f;
}
