using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;

    FloatingJoystick joystick;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    void Update()
    {
        //�������� ��� �����
        Vector3 movementVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        //��������� ������� � ��������
        if(movementVector.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(movementVector);
            characterController.Move(movementVector * speed * Time.deltaTime);
        }
        
    }
}
