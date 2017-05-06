using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : IControl
{
    private float DelayOfMoveSteps;
    public float firstTime = 0.0f;
    private float Span = 0.0f;

    public void AttackMove(string AttackType, Character gameObject)
    {
        switch (AttackType)
        {
            case "Normal":
                DelayOfMoveSteps = 1.0f;
                Span = DelayOfMoveSteps;
                if (Time.time - firstTime >= DelayOfMoveSteps)
                {
                    firstTime = Time.time;
                    gameObject.Anim.Play("SetAttack");
                }
                break;
            case "Special":
                break;
            default:
                break;
        }
    }

    public  string ControlType()
    {
        return "Player";
    }

    public  void DefenderMove(Character gameObject)
    {
        //Dodge
    }

    public  float GetDelayOfMoveSteps()
    {
        return DelayOfMoveSteps;
    }

    public  bool OtherMove()
    {
        throw new NotImplementedException();
    }

    public void InputControl(int speedMove, Character gameObject)
    {
        //Time delay
        if(Span > 0)
        {
            Span -= Time.deltaTime;
        }

        //Move when don't attack
        if (!gameObject.Attacking)
        {
            if (InputStates.IsRight)
            {
                gameObject.Rigid.velocity = new Vector2(1 * speedMove, gameObject.Rigid.velocity.y);
            }

            if (InputStates.IsLeft)
            {
                gameObject.Rigid.velocity = new Vector2(-1 * speedMove, gameObject.Rigid.velocity.y);
            }
        }
        else
        {
            //When Attack, Character still stading in the air
            if(gameObject.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                gameObject.Rigid.velocity = Vector2.zero;
        }

        //When doesn't have the input control, characters don't move
        if (!InputStates.IsInput || (!InputStates.IsRight && !InputStates.IsLeft))
        {
            gameObject.Rigid.velocity = new Vector2(0, gameObject.Rigid.velocity.y);
        }

        //when characters grounded and don't attack, he can be jump
        if (InputStates.IsJump && gameObject.IsGrounded && !gameObject.Attacking)
        {
            gameObject.Rigid.AddForce(Vector2.up * gameObject.Rigid.mass * 800);
        }

        //Attack in a time span
        if (InputStates.IsNormalAttack && Span <= 0)
        {
            gameObject.Rigid.velocity = new Vector2(gameObject.Rigid.velocity.x, 0);
            AttackMove("Normal", gameObject);
        }

        //limit distance when character jump
        if (gameObject.Rigid.velocity.y > 15)
            gameObject.Rigid.velocity = new Vector2(gameObject.Rigid.velocity.x, 15);
    }
}

