using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance;

    public PlayerInputAction inputAction;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        inputAction = new PlayerInputAction();

    }
}
