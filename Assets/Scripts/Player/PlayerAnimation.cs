using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    FloatingJoystick joystick;

    int isRun = Animator.StringToHash("IsRun");
    int speed = Animator.StringToHash("Speed");

    void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    void Update()
    {
        Vector2 inputVector = joystick.Direction;
        animator.SetBool(isRun, inputVector.magnitude > 0.1);
        animator.SetFloat(speed, inputVector.magnitude);
    }
}
