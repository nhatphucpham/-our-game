using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private uint _strong = 0;
    private uint _defend = 0;
    public Character Enemy = null;
    public Character Owned;

    public GameObject DamageParicle;
    public TextSpript TextShow;
    public bool Attacked = false;

    public uint? DealDamage
    {
        get
        {
            return CalculatorDamage();
        }
    }

    public bool IsMiss { set { CanMiss = value; } }

    private bool CanMiss;

    private uint? CalculatorDamage()
    {
        if (!CanMiss && Enemy != null)
            return (_strong > _defend) ? _strong - _defend : 0;
        else
            return null;
    }

    private void Start()
    {
        Owned = transform.parent.gameObject.GetComponent<Character>();
        _strong = Owned.OficialInfo["Strong"];
    }
    private void OnDisable()
    {
        Attacked = false;
        Enemy = null;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if (GetComponent<BoxCollider2D>().size.x > 1)
        {
            if (collider.gameObject.layer == 9)
            {
                if (Owned.IsPlayer)
                {
                    Enemy = collider.gameObject.GetComponent<Character>();
                    Owned.Enemy = Enemy;
                }
            }
            if (collider.gameObject.layer == 8)
            {
                Enemy = collider.gameObject.GetComponent<Character>();
                Enemy.Enemy = Owned;
            }

            if (Enemy != null)
            {
                print(Enemy != null);
                _defend = Enemy.OficialInfo["Defend"];
                if (!Attacked)
                {
                    ActiveDamage();
                    Attacked = true;
                }
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (9 | 8))
        {
            Enemy = null;

            Attacked = false;

            _defend = 0;
        }
    }
    private void ActiveDamage()
    {
        Vector3 Pos = Enemy.transform.position;
        Pos.z = -1;
        Pos.y += Enemy.Box.size.y / 2 + 0.2f;
        Instantiate(DamageParicle, Pos, Quaternion.identity, Enemy.transform);
        //StartCoroutine("Effect");
        Vector3 ColPos = new Vector3(Enemy.transform.position.x , Enemy.transform.position.y + 3, 0);
        if (Enemy.CurrentHP > 0)
        {
            TextSpript text = (TextSpript)Instantiate(TextShow, ColPos, Owned.transform.rotation);
            text.Damage = (int)DealDamage.Value;
            if ((int)Enemy.CurrentHP - (int)DealDamage <= 0)
            {
                Enemy.Dead();
            }
            else
            {
                Enemy.CurrentHP -= (int?)DealDamage ?? 0;
            }
        }

        if(DealDamage == null)
        {
            MissedShow();
        }
    }

    //private IEnumerator Effect()
    //{
    //    Instantiate(DamageParicle, new Vector3(0, 0, -1), Quaternion.identity, Enemy.transform );
    //    yield return new WaitForFixedUpdate();
    //}

    private void MissedShow()
    {

    }
}

