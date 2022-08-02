using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] LayerMask groundMask;

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

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2f, groundMask))
        {
            transform.position = hit.point + Vector3.up * characterController.bounds.extents.y;
        }
    }
}
