using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]public enum AnimState {Attack, Dance, Idle, Death,Run, Win, Ulti}
    AnimState lastState = AnimState.Idle;
    public static Character CharacterAnim;
    public CharacterInfo _character;
    [SerializeField] private Animator anim;
    public bool Attack, Dance, Idle, Death, Run, Win, Ulti;
    private void Start()
    {
        _character = GetComponent<CharacterInfo>();
        
    }
    public void attack()
    { 

    }

    public virtual void move()
    {

    }

    public void _onHit()
    {

    }

    public void _onTakeDamage()
    {

    }

    public void _onDeath()
    {

    }

    public void CharactorAnim(AnimState _State)
    {
        if(lastState!= _State)
        {
            switch (_State)
            {
                case AnimState.Attack:
                    anim.SetTrigger("Attack");
                    break;
                case AnimState.Dance:
                    anim.SetTrigger("Dance");
                    break;
                case AnimState.Idle:
                    anim.SetTrigger("Idle");
                    break;
                case AnimState.Death:
                    anim.SetTrigger("Death");
                    break;
                case AnimState.Run:
                    anim.SetTrigger("Run");
                    break;
                case AnimState.Win:
                    anim.SetTrigger("Win");
                    break;
                case AnimState.Ulti:
                    anim.SetTrigger("Ulti ");
                    break;
            }
            lastState = _State;
        }
    }
}
