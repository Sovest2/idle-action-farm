using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    FloatingJoystick joystick;

    int isRun = Animator.StringToHash("IsRun");
    int speed = Animator.StringToHash("Speed");
    int cut = Animator.StringToHash("Cut");

    [SerializeField] GameObject scythe;

    void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
        CutButton.CutPressed += Cut;
    }

    private void OnDestroy()
    {
        CutButton.CutPressed -= Cut;
    }

    void Update()
    {
        Vector2 inputVector = joystick.Direction;
        animator.SetBool(isRun, inputVector.magnitude > 0.1);
        animator.SetFloat(speed, inputVector.magnitude);
    }

    void Cut() 
    {
        animator.SetTrigger(cut);
    }

}
