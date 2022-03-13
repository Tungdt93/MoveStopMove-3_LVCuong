using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Character, IInitializeVariables
{
    public static PlayerController instance;
    [SerializeField] private FloatingJoystick _Joystick;
    [SerializeField] private GameObject _Cycle;

    [SerializeField] private Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public override void move()
    {
        if (GameManager.Instance.gameState==GameManager.GameState.gameStarted)
        {
            if (_Joystick.Horizontal != 0 || _Joystick.Vertical != 0)       //Nếu vị trí joystick được di chuyển thì Move Player
            {
                OnRun();
                Vector3 temp = transform.position;
                temp.x -= _Joystick.Vertical * Time.deltaTime * _character.moveSpeed;
                temp.z += _Joystick.Horizontal * Time.deltaTime * _character.moveSpeed;
                Vector3 moveDirection = new Vector3(temp.x - transform.position.x, 0, temp.z - transform.position.z);
                moveDirection.Normalize();
                Quaternion toRotate = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, 720 * Time.deltaTime);
                transform.position = temp;
                enableToAttackFlag = true;      //Bật cờ Attack để khi Character dừng lại thì sẽ tấn công nếu có Enemy ở trong bán kính tấn công.

            }
            else if (Vector3.Distance(transform.position, enemy.transform.position) < AttackRange)
            {
                attack();
            }
            else OnIdle();
                
        }else if (GameManager.Instance.gameState == GameManager.GameState.gameOver)
        {
            OnIdle();
        }
    }

    public override void attack()
    {
        if (enableToAttackFlag && _Joystick.Horizontal == 0 && _Joystick.Vertical == 0) //Điều kiện để Attack là có Enemy trong tầm ngắm, cờ enableToAttackFlag =true và đang vừa di chuyển xong dừng lại
        {
            transform.LookAt(enemy.position);
            enableToAttackFlag = false;
            OnAttack();
            StartCoroutine(TurntoIdle());
        }
    }

    void changeAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        _Cycle.transform.localScale = new Vector3(AttackRange, 1f, AttackRange);
    }
    
    IEnumerator TurntoIdle()
    {
        yield return new WaitForSeconds(0.5f);
        OnIdle();
    }

    void IInitializeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitializeVariables()
    {
        IInitializeSingleton();
        changeAttackRange(8f);
        weaponListCreate(); //Khởi tạo danh sách vũ khí
    }
}
