using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Character, IInitializeVariables, IHit
{
    public static PlayerController instance;
    [SerializeField] private FloatingJoystick _Joystick;
    [SerializeField] private GameObject _Cycle;
    [SerializeField] private GameObject Reticle;
    [SerializeField] private Material[] CupMaterial;
    private Vector3 positionToAttack;
    public int PlayerLevel;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
        GameManager.Instance.CharacterList.Add(this.gameObject);    //Thêm player vào trong list Character để quản lý
    }

    // Update is called once per frame
    void Update()
    {
        ShowReticle();
        move();
        ObstacleFading();
        _Cycle.transform.position = transform.position;
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
            else if (Vector3.Distance(transform.position, positionToAttack) < AttackRange&& positionToAttack != Vector3.zero)
            {
                attack();
            }
            else OnIdle();
        }else if (GameManager.Instance.gameState == GameManager.GameState.gameWin)
        {
            OnWin();
        }
    }

    public override void attack()
    {
        if (enableToAttackFlag)
        {
            transform.LookAt(positionToAttack);
            enableToAttackFlag = false;
            OnAttack();
            attackScript.GetComponent<Attack>().SetID(gameObject.GetInstanceID(), opponentID);
            StartCoroutine(TurntoIdle());
        }
    }
    IEnumerator TurntoIdle()
    {
        yield return new WaitForSeconds(0.5f);
        if(GameManager.Instance.gameState == GameManager.GameState.gameStarted) OnIdle();
    }

    void changeAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        _Cycle.transform.localScale = new Vector3(AttackRange, 1f, AttackRange);
    }

    #region Singleton
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
    #endregion

    public void InitializeVariables()
    {
        AttackRange = 5f;
        AttackSpeed = 10;
        weaponListCreate();//Khởi tạo danh sách vũ khí
        CreateListOfWeaponMaterial();
        weaponSwitching(weaponType.uzi, new weaponMaterialsType[] {weaponMaterialsType.Uzi_2});
        AddWeaponPower();
        ChangeClothes(clothesType.Thor);
        IInitializeSingleton();
        changeAttackRange(AttackRange);             
        IsDeath = false;
        PlayerLevel = 0;
    }

    #region Reticle
    void ShowReticle() //Hiện mục tiêu của Player
    {
        positionToAttack = FindNearistEnemy(AttackRange);
        if (positionToAttack != Vector3.zero)
        {
            Reticle.transform.position = new Vector3(positionToAttack.x, 0.1f, positionToAttack.z);
            Reticle.SetActive(true);
        }
        else
        {
            Reticle.SetActive(false);
        }
    }
    #endregion  


    public void OnHit()
    {
        OnDeath();
        Reticle.SetActive(false);
        IsDeath = true;
        GameManager.Instance.KilledAmount++;
        GameManager.Instance.gameState = GameManager.GameState.gameOver;
    }

    void ObstacleFading()
    {
        foreach (Transform _obstacle in GameManager.Instance.Obstacle)
        {
            if (Vector3.Distance(transform.position, _obstacle.position) < 8f)
            {
                _obstacle.GetComponent<Renderer>().sharedMaterial = CupMaterial[1];
            }
            else
            {
                _obstacle.GetComponent<Renderer>().sharedMaterial = CupMaterial[0];
            }
        }
    }
    public void AddLevel()                                      //Mỗi lần bắn hạ đối thủ thì sẽ gọi hàm AddLevel
    {
        PlayerLevel++;
        transform.localScale = new Vector3(1f + 0.1f * PlayerLevel, 1f + 0.1f * PlayerLevel, 1f + 0.1f * PlayerLevel); //Khi tăng 1 level thì sẽ tăng Scale của Player thêm 10% so với kích thước khi Start game
        _character.moveSpeed =(1f + 0.05f * PlayerLevel) * 5f;  //Tốc độ di chuyển của Player tăng 5% so với khi Start game.
        changeAttackRange(1.05f * AttackRange);                 //Tăng 5% tầm bắn
    }

    public void weaponSwitching(weaponType _weaponType, weaponMaterialsType[] _weaponMaterial)
    {
        Material[] CurrentWeaponMaterial = new Material[_weaponMaterial.Length];
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (i == (int)_weaponType)
            {
                CurrentWeaponMaterial = weaponArray[i].GetComponent<Renderer>().sharedMaterials;
                CurrentWeaponMaterial = GetWeaponMaterial(_weaponType,_weaponMaterial);
                weaponArray[i].GetComponent<Renderer>().sharedMaterials = CurrentWeaponMaterial;
                weaponArray[i].SetActive(true);
            }
            else
            {
                weaponArray[i].SetActive(false);
            }
        }
    }

    Material[] GetWeaponMaterial(weaponType _weaponType, weaponMaterialsType[] _weaponMaterial)
    {
        Material[] desireMaterials = new Material[_weaponMaterial.Length];
        if (_weaponMaterial.Length == 1)
        {
            Material[] desireMaterial = ListWeaponMaterial[_weaponMaterial[0]];
            return desireMaterial;
        }
        else
        {
            for (int i = 0; i < _weaponMaterial.Length; i++)
            {
                desireMaterials[i] = ListWeaponMaterial[_weaponMaterial[i]][0];
            }
        }
        return desireMaterials;
    }

    
}
