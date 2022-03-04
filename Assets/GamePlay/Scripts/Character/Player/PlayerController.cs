using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField] private FloatingJoystick _Joystick;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(AnimState.Attack);
    }

    // Update is called once per frame
    void Update()
    {
        if (_Joystick.Horizontal != 0 || _Joystick.Vertical != 0) //Nếu vị trí joystick được di chuyển thì Move Player
        {
            CharactorAnim(AnimState.Run);
            move();
        }
        else
        {
            CharactorAnim(AnimState.Idle);
        }
    }

    public override void move()
    {
        Vector3 temp = transform.position;
        temp.x -= _Joystick.Vertical * Time.deltaTime * _character.moveSpeed;
        temp.z += _Joystick.Horizontal * Time.deltaTime * _character.moveSpeed;
        Vector3 moveDirection = new Vector3(temp.x-transform.position.x, 0, temp.z - transform.position.z);
        moveDirection.Normalize();
        Quaternion toRotate = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 720* Time.deltaTime);
        transform.position = temp;
    }
}
