using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [HideInInspector] public enum CharacterAnimState { Attack, Dance, Idle, Death, Run, Win, Ulti }

    private Animator animator;

    protected bool isAttack;
    protected bool isDance;
    protected bool isIdle;
    protected bool isDeath;
    protected bool isRun;
    protected bool isWin;
    protected bool isUlti;
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool IsDance { get => isDance; set => isDance = value; }
    public bool IsIdle { get => isIdle; set => isIdle = value; }
    public bool IsDeath { get => isDeath; set => isDeath = value; }
    public bool IsRun { get => isRun; set => isRun = value; }
    public bool IsWin { get => isWin; set => isWin = value; }
    public bool IsUlti { get => isUlti; set => isUlti = value; }

    // Start is called before the first frame update
    void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        isAttack = false;
        isDance = false;
        isIdle = false;
        isDeath = false;
        isRun = false;
        isWin = false;
        isUlti = false;
        SetAnim(CharacterAnimState.Idle);
    }

    #region Set Character Animation
    public void SetAnim(CharacterAnimState _CharacterAnimation)
    {
        switch (_CharacterAnimation)
        {
            case CharacterAnimState.Attack:
                isAttack = true;
                isDance = false;
                isIdle = false;
                isDeath = false;
                isRun = false;
                isWin = false;
                isUlti = false;
                break;
            case CharacterAnimState.Dance:
                isAttack = false;
                isDance = true;
                isIdle = false;
                isDeath = false;
                isRun = false;
                isWin = false;
                isUlti = false;
                break;
            case CharacterAnimState.Idle:
                isAttack = false;
                isDance = false;
                isIdle = true;
                isDeath = false;
                isRun = false;
                isWin = false;
                isUlti = false;
                break;
            case CharacterAnimState.Death:
                isAttack = false;
                isDance = false;
                isIdle = false;
                isDeath = true;
                isRun = false;
                isWin = false;
                isUlti = false;
                break;
            case CharacterAnimState.Run:
                isAttack = false;
                isDance = false;
                isIdle = false;
                isDeath = false;
                isRun = true;
                isWin = false;
                isUlti = false;
                break;
            case CharacterAnimState.Win:
                isAttack = false;
                isDance = false;
                isIdle = false;
                isDeath = false;
                isRun = false;
                isWin = true;
                isUlti = false;
                break;
            case CharacterAnimState.Ulti:
                isAttack = false;
                isDance = false;
                isIdle = false;
                isDeath = false;
                isRun = false;
                isWin = false;
                isUlti = true;
                break;
        }
        animator.SetBool("IsAttack", isAttack);
        animator.SetBool("IsDance", isDance);
        animator.SetBool("IsIdle", isIdle);
        animator.SetBool("IsDeath", isDeath);
        animator.SetBool("IsRun", isRun);
        animator.SetBool("IsWin", isWin);
        animator.SetBool("IsUlti", isUlti);
    }
    #endregion


    #region Play Character Animation
    public void AttackAnimation()
    {
        SetAnim(CharacterAnimState.Attack);
    }
    public void DanceAnimation()
    {
        SetAnim(CharacterAnimState.Dance);
    }
    public void IdleAnimation()
    {
        SetAnim(CharacterAnimState.Idle);
    }
    public void DeathAnimation()
    {
        SetAnim(CharacterAnimState.Death);
    }
    public void RunAnimation()
    {
        SetAnim(CharacterAnimState.Run);
    }
    public void WinAnimation()
    {
        SetAnim(CharacterAnimState.Win);
    }
    public void UltiAnimation()
    {
        SetAnim(CharacterAnimState.Ulti);
    }
    #endregion
}
