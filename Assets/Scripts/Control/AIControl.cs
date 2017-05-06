using System;
using UnityEngine;

public class AIControl : IControl
{
    float DelayOfMoveSteps;
    float firstTimeA;
    public float firstTimeB;
    public bool CanAttack;
    Character Player;

    public AIControl(Character Player)
    {
        DelayOfMoveSteps = 0.0f;
        firstTimeA = 0.0f;
        firstTimeB = 0.0f;
        this.Player = Player;
    }

    public void AttackMove(string AttackType, Character gameObject)
    {

        switch (AttackType)
        {
            case "Normal":
                if (CanAttack)
                {
                    DelayOfMoveSteps = 1.5f;
                    gameObject.Rigid.velocity = new Vector2(0, gameObject.Rigid.velocity.y);
                    if (Time.time - firstTimeA >= DelayOfMoveSteps)
                    {
                        if(Player.transform.position.x - gameObject.transform.position.x > 0.2)
                        {
                            gameObject.Direction = Directions.Right;
                        }
                        if (Player.transform.position.x - gameObject.transform.position.x < -0.2)
                        {
                            gameObject.Direction = Directions.Left;
                        }
                        firstTimeA = Time.time;
                        gameObject.Anim.Play("SetAttack");
                    }
                    firstTimeB = Time.time;
                }
                else
                {
                    DelayOfMoveSteps = 2.0f;
                    if (!CanAttack && Time.time - firstTimeB >= DelayOfMoveSteps)
                    {
                        if(Time.time - firstTimeB >= DelayOfMoveSteps*2)
                            firstTimeB = Time.time;

                        if (Player.transform.position.x - gameObject.transform.position.x > 0.2)
                        {
                            gameObject.Direction = Directions.Right;
                        }
                        if (Player.transform.position.x - gameObject.transform.position.x < -0.2)
                        {
                            gameObject.Direction = Directions.Left;
                        }

                        gameObject.Rigid.velocity = new Vector2(-5 * (int)gameObject.Direction, gameObject.Rigid.velocity.y);

                    }
                    else
                    {
                        gameObject.Rigid.velocity = new Vector2(0, gameObject.Rigid.velocity.y);

                    }
                }
                break;
            case "Special":
                break;
            default:
                break;
        }
    }
    public float GetDelayOfMoveSteps()
    {
        return DelayOfMoveSteps;
    }

    public string ControlType()
    {
        return "AI";
    }

    public void DefenderMove(Character gameObject)
    {
        DelayOfMoveSteps = 3.0f;
        if(Time.time - firstTimeB >= DelayOfMoveSteps)
        {
            if(Player.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && CanAttack)
            {
                gameObject.Rigid.velocity = new Vector2(-1*10 * (int)gameObject.Direction, gameObject.Rigid.velocity.y);
            }
        }
    }

    public bool OtherMove()
    {
        throw new NotImplementedException();
    }
}
