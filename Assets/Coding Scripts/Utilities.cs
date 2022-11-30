using UnityEngine;

public static class Tags 
{
    //for potential sprites
    public static string player = "Player";
    public static string enemy = "Enemy";

    //other potentially needed sprites
    public static string world = "World";
    public static string bullet = "Bullet";
}

//handles potential animation states 
public static class AnimationStates
{
    //sprites
    public static int xmovement = Animator.StringToHash("XMovement");
    public static int direction = Animator.StringToHash("Direction");
    public static int onground = Animator.StringToHash("OnGround");
    public static int alive = Animator.StringToHash("Alive");
}

//enums
//sprites
public enum SpriteType { Grounded, Flying };
public enum AttackType { None, Constant, Target };
public enum MoveType { None, Normal, Circle, Jump };

//Delegates if enemies are needed
/*
 public delegate void AttackPattern(EnemyController encon);
 public delegate void MovePattern(EnemyController encon);
 */