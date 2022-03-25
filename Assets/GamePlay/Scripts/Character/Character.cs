using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [HideInInspector] public enum CharacterName { ABI, Uniqlo, Bitis, Vinamilk, KoBaYaShi, Ford, Vinfast, ToYoTa, Yamato, Biden, Biladen, Vodka, Yamaha, Honda, Suzuki, NiShiNo, Furuki }
    [HideInInspector]public enum weaponType { Arrow, Axe_0, Axe_1, boomerang, candy_0, candy_1, candy_2, candy_4, Hammer, knife, uzi, Z}
    [HideInInspector]public enum AddPowerType {addition, multiplication}
    [HideInInspector]public enum weaponMaterialsType 
    { 
        Arrow,
        Axe0, Axe0_2,
        Axe1, Axe1_2, 
        Boomerang_1, Boomerang_2, 
        Candy0_1, Candy0_2, 
        candy1_1, candy1_2, candy1_3, candy1_4, 
        Candy2_1, Candy2_2, 
        Candy4_1, Candy4_2, 
        Hammer_1, Hammer_2, 
        Knife_1, Knife_2, 
        Uzi_1, Uzi_2,
        Z,
        Azure, Black,
        Blue, Chartreuse,
        Cyan, Green,
        Magenta, Orange,
        Red, Rose,
        SpringGreen, Violet,
        White, Yellow
    }
    [HideInInspector] public enum clothesType 
    { 
        Arrow, Cowboy, Crown, Ear, Hat, Hat_Cap, Hat_Yellow, HeadPhone, Rau, Khien, Shield,
        Batman, Chambi, comy, dabao, onion, pokemon, rainbow, Skull, Vantim,
        Devil, Angel, Witch, Deadpool, Thor
    }
    [HideInInspector]
    public enum SetFullOrNormal{SetFull, Normal}
    public SetFullOrNormal lastClothes;
    [SerializeField] private Animator anim;
    public UnityAction OnAttack;
    public UnityAction OnRun;
    public UnityAction OnIdle;
    public UnityAction OnDeath;
    public UnityAction OnWin;
    public UnityAction OnDance;
    public UnityAction OnUlti;
    public GameObject attackScript;
    public float AttackRange;
    public float AttackSpeed;
    public float MoveSpeed;
    public ClothesInfo CharacterClothes;
    public ClothesPower clothesPower;
    public Transform ShieldPosition;
    public Transform LeftHandPosition;
    public Transform HeadPosition;
    public Transform TailPosition;
    public Transform BackPosition;
    public GameObject PantPosition;
    public GameObject SkinPosition;
    public Transform weaponPosition;                        //GameObject chứa weapon trên tay Character.
    public GameObject[] weaponArray = new GameObject[12];   //Mảng dùng để quản lý weapon trên tay Character
    public Animator characterCanvasAnim;
    public WeaponInfo _weapon;
    public EnemyRandomSkin _enemySkin;
    public bool enableToAttackFlag=false;
    public float distanceToNearistEnemy;
    public Vector3 nearistEnemyPosition;
    public int opponentID;
    public int EnemySkinID;
    public bool IsDeath;
    public Dictionary<weaponMaterialsType, Material[]> ListWeaponMaterial = new Dictionary<weaponMaterialsType, Material[]>();

    private void Start()
    {
        _weapon = GetComponent<WeaponInfo>();
    }
    public virtual void attack()
    { 

    }

    public virtual void move() 
    {

    }

    public void weaponListCreate() //Thêm vũ khí vào weaponList
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            weaponArray[i] = weaponPosition.GetComponent<Transform>().transform.GetChild(i).gameObject;
        }
    }
    
    public Vector3 FindNearistEnemy(float attackRange)
    {
        distanceToNearistEnemy = 1000f;
        foreach (GameObject character in GameManager.Instance.CharacterList)
        {
            if (character.GetInstanceID() != gameObject.GetInstanceID()&&Vector3.Distance(character.transform.position,gameObject.transform.position)< attackRange&&character.activeSelf)
            {
                if (character.CompareTag("Enemy"))
                {
                    if (Vector3.Distance(character.transform.position, gameObject.transform.position) < distanceToNearistEnemy && character.GetComponent<EnemyController>().IsDeath == false)
                    {
                        distanceToNearistEnemy = Vector3.Distance(character.transform.position, gameObject.transform.position);
                        nearistEnemyPosition = character.transform.position;
                        opponentID = character.GetInstanceID(); //Lấy ID của đối phương
                    }
                }
                else if (character.CompareTag("Player"))
                {
                    if (Vector3.Distance(character.transform.position, gameObject.transform.position) < distanceToNearistEnemy && character.GetComponent<PlayerController>().IsDeath == false)
                    {
                        distanceToNearistEnemy = Vector3.Distance(character.transform.position, gameObject.transform.position);
                        nearistEnemyPosition = character.transform.position;
                        opponentID = character.GetInstanceID(); //Lấy ID của đối phương
                    }
                }
            }
        }
        if (distanceToNearistEnemy > 900f) return Vector3.zero;
        else return nearistEnemyPosition;
    }

    public void AddWeaponPower()     //Đang cầm loại nào thì sẽ cộng thêm AttackRange và AttackSpeed tương ứng vào.
    {
        for (int i = 0; i < weaponArray.Length; i++)
        {
            if (weaponArray[i].activeSelf)
            {
                AttackRange += _weapon.AddAttackRange[i];
                AttackSpeed += _weapon.AddAttackSpeed[i];
                break;
            }
        }
    }
    #region Create List of Weapon Materials
    public void CreateListOfWeaponMaterial()
    {
        ListWeaponMaterial.Add(weaponMaterialsType.Arrow, _weapon.ArrowDefaultMaterials);
        ListWeaponMaterial.Add(weaponMaterialsType.Axe0, new Material[]{ _weapon.Axe0DefaultMaterials[0], _weapon.Axe0DefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Axe0_2, new Material[]{ _weapon.Axe0DefaultMaterials[1], _weapon.Axe0DefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Axe1, new Material[]{ _weapon.Axe1DefaultMaterials[0], _weapon.Axe1DefaultMaterials[0], _weapon.Axe1DefaultMaterials[0] });
        ListWeaponMaterial.Add(weaponMaterialsType.Axe1_2, new Material[]{ _weapon.Axe1DefaultMaterials[1], _weapon.Axe1DefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Boomerang_1, new Material[]{ _weapon.BoomerangDefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Boomerang_2, new Material[]{ _weapon.BoomerangDefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Candy0_1, new Material[]{ _weapon.Candy0DefaultMaterials[0], _weapon.Candy0DefaultMaterials[0], _weapon.Candy0DefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Candy0_2, new Material[]{ _weapon.Candy0DefaultMaterials[1], _weapon.Candy0DefaultMaterials[1], _weapon.Candy0DefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.candy1_1, new Material[]{ _weapon.Candy1DefaultMaterials[0], _weapon.Candy1DefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.candy1_2, new Material[]{ _weapon.Candy1DefaultMaterials[1], _weapon.Candy1DefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.candy1_3, new Material[]{ _weapon.Candy1DefaultMaterials[2], _weapon.Candy1DefaultMaterials[2]});
        ListWeaponMaterial.Add(weaponMaterialsType.candy1_4, new Material[]{ _weapon.Candy1DefaultMaterials[3], _weapon.Candy1DefaultMaterials[3]});
        ListWeaponMaterial.Add(weaponMaterialsType.Candy2_1, new Material[]{ _weapon.Candy2DefaultMaterials[0], _weapon.Candy2DefaultMaterials[0], _weapon.Candy2DefaultMaterials[0] });
        ListWeaponMaterial.Add(weaponMaterialsType.Candy2_2, new Material[]{ _weapon.Candy2DefaultMaterials[1], _weapon.Candy2DefaultMaterials[1], _weapon.Candy2DefaultMaterials[1] });
        ListWeaponMaterial.Add(weaponMaterialsType.Candy4_1, new Material[]{ _weapon.Candy4DefaultMaterials[0], _weapon.Candy4DefaultMaterials[0], _weapon.Candy4DefaultMaterials[0] });
        ListWeaponMaterial.Add(weaponMaterialsType.Candy4_2, new Material[]{ _weapon.Candy4DefaultMaterials[1], _weapon.Candy4DefaultMaterials[1], _weapon.Candy4DefaultMaterials[1] });
        ListWeaponMaterial.Add(weaponMaterialsType.Hammer_1, new Material[]{ _weapon.HammerDefaultMaterials[0], _weapon.HammerDefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Hammer_2, new Material[]{ _weapon.HammerDefaultMaterials[1], _weapon.HammerDefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Knife_1, new Material[]{ _weapon.KnifeDefaultMaterials[0], _weapon.KnifeDefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Knife_2, new Material[]{ _weapon.KnifeDefaultMaterials[1], _weapon.KnifeDefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Uzi_1, new Material[]{ _weapon.UziDefaultMaterials[0], _weapon.UziDefaultMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Uzi_2, new Material[]{ _weapon.UziDefaultMaterials[1], _weapon.UziDefaultMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Z, _weapon.ZDefaultMaterials);
        ListWeaponMaterial.Add(weaponMaterialsType.Azure, new Material[] { _weapon.CustomMaterials[0]});
        ListWeaponMaterial.Add(weaponMaterialsType.Black, new Material[] { _weapon.CustomMaterials[1]});
        ListWeaponMaterial.Add(weaponMaterialsType.Blue, new Material[] { _weapon.CustomMaterials[2]});
        ListWeaponMaterial.Add(weaponMaterialsType.Chartreuse, new Material[] { _weapon.CustomMaterials[3]});
        ListWeaponMaterial.Add(weaponMaterialsType.Cyan, new Material[] { _weapon.CustomMaterials[4]});
        ListWeaponMaterial.Add(weaponMaterialsType.Green, new Material[] { _weapon.CustomMaterials[5]});
        ListWeaponMaterial.Add(weaponMaterialsType.Magenta, new Material[] { _weapon.CustomMaterials[6]});
        ListWeaponMaterial.Add(weaponMaterialsType.Orange, new Material[] { _weapon.CustomMaterials[7]});
        ListWeaponMaterial.Add(weaponMaterialsType.Red, new Material[] { _weapon.CustomMaterials[8]});
        ListWeaponMaterial.Add(weaponMaterialsType.Rose, new Material[] { _weapon.CustomMaterials[9]});
        ListWeaponMaterial.Add(weaponMaterialsType.SpringGreen, new Material[] { _weapon.CustomMaterials[10]});
        ListWeaponMaterial.Add(weaponMaterialsType.Violet, new Material[] { _weapon.CustomMaterials[11]});
        ListWeaponMaterial.Add(weaponMaterialsType.White, new Material[] { _weapon.CustomMaterials[12]});
        ListWeaponMaterial.Add(weaponMaterialsType.Yellow, new Material[] { _weapon.CustomMaterials[13]});
    }
    #endregion

    #region ChangeClothes
    public void ChangeClothes(clothesType _ClothesType)
    {
        SetFullOrNormal _setfullOrNormal;
        _setfullOrNormal = ((int)_ClothesType > 19) ? (SetFullOrNormal.SetFull) : (SetFullOrNormal.Normal);
        if (_setfullOrNormal!=lastClothes|| _setfullOrNormal == SetFullOrNormal.SetFull) ResetClothes();
        switch (_ClothesType)
        {
            case clothesType.Arrow:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[0],HeadPosition);
                    break;
                }
            case clothesType.Cowboy:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[1], HeadPosition);
                    break;
                }
            case clothesType.Crown:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[2], HeadPosition);
                    break;
                }
            case clothesType.Ear:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[3], HeadPosition);
                    break;
                }
            case clothesType.Hat:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[4], HeadPosition);
                    break;
                }
            case clothesType.Hat_Cap:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[5], HeadPosition);
                    break;
                }
            case clothesType.Hat_Yellow:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[6], HeadPosition);
                    break;
                }
            case clothesType.HeadPhone:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[7], HeadPosition);
                    break;
                }
            case clothesType.Rau:
                {
                    Instantiate(CharacterClothes.HeadPosition[8], HeadPosition);
                    break;
                }
            case clothesType.Khien:
                {
                    ResetShieldPosition();
                    Instantiate(CharacterClothes.LeftHandPosition[2], LeftHandPosition);
                    break;
                }
            case clothesType.Shield:
                {
                    ResetShieldPosition();
                    Instantiate(CharacterClothes.LeftHandPosition[3], LeftHandPosition);
                    break;
                }
            case clothesType.Batman:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[4];
                    break;
                }
            case clothesType.Chambi:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[5];
                    break;
                }
            case clothesType.comy:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[6];
                    break;
                }
            case clothesType.dabao:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[7];
                    break;
                }
            case clothesType.onion:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[8];
                    break;
                }
            case clothesType.pokemon:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[9];
                    break;
                }
            case clothesType.rainbow:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[10];
                    break;
                }
            case clothesType.Skull:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[11];
                    break;
                }
            case clothesType.Vantim:
                {
                    ResetClothes();
                    PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[12];
                    break;
                }
            case clothesType.Devil:
                {
                    ResetClothes();
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[10],HeadPosition);
                    Instantiate(CharacterClothes.BackPosition[2],BackPosition);
                    Instantiate(CharacterClothes.TailPosition[0],TailPosition);
                    SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[3];
                    break;
                }
            case clothesType.Angel:
                {
                    ResetHeadPosition();
                    ResetLeftHandPosition();
                    Instantiate(CharacterClothes.HeadPosition[9], HeadPosition);
                    Instantiate(CharacterClothes.BackPosition[0], BackPosition);
                    Instantiate(CharacterClothes.LeftHandPosition[0], LeftHandPosition);
                    SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[0];
                    break;
                }
            case clothesType.Witch:
                {
                    ResetHeadPosition();
                    ResetLeftHandPosition();
                    Instantiate(CharacterClothes.HeadPosition[12], HeadPosition);
                    Instantiate(CharacterClothes.LeftHandPosition[1], LeftHandPosition);
                    SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[6];
                    break;
                }
            case clothesType.Deadpool:
                {
                    Instantiate(CharacterClothes.BackPosition[1], BackPosition);
                    SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[1];
                    break;
                }
            case clothesType.Thor:
                {
                    ResetHeadPosition();
                    Instantiate(CharacterClothes.HeadPosition[11], HeadPosition);
                    SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[5];
                    break;
                }
        }
        lastClothes = _setfullOrNormal;
    }

    public void ResetClothes()
    {
        foreach (Transform item in TailPosition)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in BackPosition)
        {
            Destroy(item.gameObject);
        }
        PantPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.PantsMaterials[3];
        if(gameObject.CompareTag("Player")) SkinPosition.GetComponent<Renderer>().sharedMaterial = CharacterClothes.SkinMaterials[8];   //Nếu là Player thì cho màu vàng là default
        else                                                                                                                            //Nếu là Enemy thì chọn màu random
        {
            EnemySkinID = Random.Range(0, 21);
            SkinPosition.GetComponent<Renderer>().sharedMaterial = _enemySkin.EnemyColor[EnemySkinID];
        }
        
    }

    public void ResetShieldPosition()
    {
        foreach (Transform item in ShieldPosition)
        {
            Destroy(item.gameObject);
        }
    }

    public void ResetLeftHandPosition()
    {
        foreach (Transform item in LeftHandPosition)
        {
            Destroy(item.gameObject);
        }
    }

    public void ResetHeadPosition()
    {
        foreach (Transform item in HeadPosition)
        {
            Destroy(item.gameObject);
        }
    }
    #endregion
}
