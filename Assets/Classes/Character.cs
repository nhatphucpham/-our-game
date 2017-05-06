using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Transform AttackBox;
    protected IControl Control;

    public Vector3 AttackOffset;
    public Vector3 AttackSize;

    public static Character[] Characters;
    private Dictionary<string, uint> _baseInfo = new Dictionary<string, uint>
    {
        { "Level", 1 },
        { "HP", 10},
        { "Str", 1 },
        { "Magic", 0 },
        { "Skill", 0 },
        { "Spd", 0 },
        { "Def", 0 },
        { "Lck", 0 },
        { "Res", 0 },
        { "Move", 0 },

        { "Max Level", 0 },
        { "Max HP", 0 },
        { "Max Str", 0 },
        { "Max Magic", 0 },
        { "Max Skill", 0 },
        { "Max Spd", 0 },
        { "Max Def", 0 },
        { "Max Lck", 0 },
        { "Max Res", 0 },
        { "Max Move", 0 },

        { "HP Growth", 0 },
        { "Str Growth", 0 },
        { "Magic Growth", 0 },
        { "Skill Growth", 0 },
        { "Spd Growth", 0 },
        { "Def Growth", 0 },
        { "Lck Growth", 0 },
        { "Res Growth", 0 }
    };

    public bool IsPlayer;
    public bool Active;
    public Directions SpriteDirection;
    public Directions Direction;
    public Character Enemy;

    /// <summary>
    /// Current HP of the Character
    /// </summary>
    public float CurrentHP { get; set; }

    /// <summary>
    /// Base HP of the Character
    /// </summary>
    public uint MaxHP;

    public float FirstTime
    {
        set
        {
            if (Control != null)
            {
                if (Control.GetType() == typeof(AIControl))
                    ((AIControl)Control).firstTimeB = value;
                else
                    ((PlayerControl)Control).firstTime = value;
            }
        }
    }
    public bool IsGrounded { get; set; }
    public Animator Anim { get; set; }
    public bool Attacking { get; set; }
    public Rigidbody2D Rigid { get; set; }
    public BoxCollider2D Box { get; set; }

    //Get Only
    public SortedList<string, uint> OficialInfo
    {
        get
        {
            Info["Strong"] = _baseInfo["Str"];
            Info["Defend"] = _baseInfo["Def"];
            return Info;
        }
    }

    SortedList<string, uint> Info = new SortedList<string, uint>(2);

    //Set first value 
    virtual protected void Awake()
    {
        Rigid = gameObject.GetComponent<Rigidbody2D>();
        Box = gameObject.GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();
        Attacking = false;
        MaxHP = _baseInfo["HP"];
        CurrentHP = (int)MaxHP;
        if (IsPlayer)
            Control = new PlayerControl();
        else
        {
            if (Enemy == null)
                Enemy = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            Control = new AIControl(Enemy);
        }
        if (Characters == null)
        {
            Characters = Resources.LoadAll<Character>("Prefab");
        }
    }

    virtual protected void Start()
    {
        if (!IsPlayer)
        {
            GameObject HP = Resources.Load("Prefab/HPBar") as GameObject;
            Instantiate(HP, gameObject.transform, false);
        }
        Transform[] gOs = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in gOs)
        {
            if (t.gameObject.name == "AttackBox")
            {
                AttackBox = t;
                break;
            }
        }
        AttackSize = transform.FindChild("AttackBox").GetComponent<BoxCollider2D>().size;
        AttackOffset = transform.FindChild("AttackBox").GetComponent<BoxCollider2D>().offset;
    }

    virtual protected void Update()
    {
        if (IsPlayer & GetComponentInChildren<HP>() != null)
        {
            Destroy(GetComponentInChildren<HP>().gameObject);
        }
        #region Set Direction
        if (Rigid.velocity.x > 0.1)
            Direction = Directions.Right;
        else if (Rigid.velocity.x < -0.1)
            Direction = Directions.Left;
        if (Direction == Directions.Left)
        {
            var scale = transform.localScale;

            if (SpriteDirection == Directions.Left)
            {
                scale.x = 1;
            }
            else
            {
                scale.x = -1;
            }
            transform.localScale = scale;
        }
        else
        {
            var scale = transform.localScale;
            if (SpriteDirection == Directions.Left)
            {
                scale.x = -1;
            }
            else
            {
                scale.x = 1;
            }
            transform.localScale = scale;
        }
        #endregion

        #region Set Attack State
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !Anim.GetCurrentAnimatorStateInfo(0).IsName("Fly"))
        {
            Attacking = true;
        }
        else
        {
            Attacking = false;
        }
        Anim.SetBool("Attacking", Attacking);
        #endregion

        #region Attack Time
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") & !Anim.GetCurrentAnimatorStateInfo(0).IsName("Fly"))
        {
            Rigid.velocity = Vector2.zero;
            if (!AttackBox.gameObject.activeInHierarchy)
            {
                AttackBox.gameObject.SetActive(true);
            }
            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                AttackBox.GetComponent<BoxCollider2D>().offset = AttackOffset;
                AttackBox.GetComponent<BoxCollider2D>().size = AttackSize;
            }
            else
            {
                AttackBox.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
                AttackBox.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            }
        }
        else
        {
            if (AttackBox.gameObject.activeInHierarchy)
            {
                AttackBox.gameObject.SetActive(false);
                AttackBox.GetComponent<Damage>().Attacked = false;
            }
        }
        #endregion

        if (Active)
        {
            if (Control.GetType() == typeof(PlayerControl))
            {
                ((PlayerControl)Control).InputControl(8, this);
            }
            if (Control.GetType() == typeof(AIControl))
            {
                Control.AttackMove("Normal", this);
            }
        }

        Anim.SetBool("Grounded", IsGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<Character>().GetType() == typeof(Soldier))
            print("sodier");
        BoxCollider2D box = collision.gameObject.GetComponent<BoxCollider2D>();
        if (collision.gameObject.tag == "Ground" &&
            transform.position.y - Box.size.y / 2 + Box.offset.y >= collision.gameObject.transform.position.y + box.size.y / 2 - 0.2f)
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        BoxCollider2D box = collision.gameObject.GetComponent<BoxCollider2D>();
        if (collision.gameObject.tag == "Ground" && transform.position.y - Box.size.y / 2 + Box.offset.y >= collision.gameObject.transform.position.y + box.size.y / 2)
        {
            IsGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.DrawLine(Vector3.zero, Vector3.down * 3, Color.white);
    }

    #region Manual Method
    /// <summary>
    /// Create method, allow to create infomation off the character
    /// </summary>
    public void Create(uint Level, uint HP, uint Str, uint Magic, uint Skill, uint Spd, uint Def, uint Lck, uint Res, uint Move)
    {
        var dictionaries = new Dictionary<string, uint>
        {
            { "Level", Level },
            { "HP", HP },
            { "Str", Str },
            { "Magic", Magic },
            { "Skill", Skill },
            { "Spd", Spd },
            { "Def", Def },
            { "Lck", Lck },
            { "Res", Res },
            { "Move", Move },

            { "Max Level", 0 },
            { "Max HP", 0 },
            { "Max Str", 0 },
            { "Max Magic", 0 },
            { "Max Skill", 0 },
            { "Max Spd", 0 },
            { "Max Def", 0 },
            { "Max Lck", 0 },
            { "Max Res", 0 },
            { "Max Move", 0 },

            { "HP Growth", 0 },
            { "Str Growth", 0 },
            { "Magic Growth", 0 },
            { "Skill Growth", 0 },
            { "Spd Growth", 0 },
            { "Def Growth", 0 },
            { "Lck Growth", 0 },
            { "Res Growth", 0 }
        };
        _baseInfo = dictionaries;
        this.MaxHP = HP;
        this.CurrentHP = (int)MaxHP;
    }
    protected void SetRate(string Key, uint Value)
    {
        uint value;
        _baseInfo.TryGetValue("{0} Growth", out value);
        value = Value;
    }
    protected void IncreaseInfo(string Key, uint Value)
    {
        uint value, maxvalue;
        _baseInfo.TryGetValue(Key, out value);
        _baseInfo.TryGetValue(string.Format("Max {0}", Key), out maxvalue);

        value += Value;
        if (value > maxvalue)
        {
            value = maxvalue;
            print(string.Format("{0} is max value", Key));
        }
    }
    public uint? GetValue(string Key)
    {
        if (_baseInfo == null || !_baseInfo.ContainsKey(Key))
            return null;
        return _baseInfo[Key];
    }
    public uint? GetMaxValue(string Key)
    {
        string keyFixed = string.Format("Max {0}", Key);
        if (!_baseInfo.ContainsKey(keyFixed))
            return null;
        return _baseInfo[keyFixed];
    }
    public Dictionary<string, uint> LeveUp()
    {
        var IncreaseValues = new Dictionary<string, uint>
        {
            { "HP", 0 },
            { "Str", 0 },
            { "Magic", 0 },
            { "Skill", 0 },
            { "Spd", 0 },
            { "Def", 0 },
            { "Lck", 0 },
            { "Res", 0 }
        };
        uint value, maxvalue;
        _baseInfo.TryGetValue("Level", out value);
        _baseInfo.TryGetValue("Max Level", out maxvalue);

        if (value < maxvalue)
        {
            value++;
        }

        foreach (string key in IncreaseValues.Keys)
        {
            if (_baseInfo[key] < _baseInfo[string.Format("Max {0}")])
            {
                IncreaseValues.TryGetValue(key, out value);
                value = (Random.Range(1, 100) <= _baseInfo[string.Format("{0} Growth", key)]) ?
                    (uint)1 : (uint)0;
            }
        }

        return IncreaseValues;
    }
    
    public void SetCanAttack(bool value)
    {
        if (Control.GetType() == typeof(AIControl))
            ((AIControl)Control).CanAttack = value;
    }
    public void Dead()
    {
        Character CharClone = null;
        // Do something

        if (Control.GetType() == typeof(AIControl))
        {
            CharClone = Characters[Random.Range(0, Characters.Length - 1)];
            Instantiate(CharClone, new Vector3(Random.Range(-12, 12), transform.position.y), transform.rotation);
            CharClone = Characters[Random.Range(0, Characters.Length - 1)];
            Instantiate(CharClone, new Vector3(Random.Range(-12, 12), transform.position.y), transform.rotation);
        }
        else
        {
            Enemy.IsPlayer = true;
            Enemy.Control = new PlayerControl();
            Enemy.gameObject.layer = 8;
            Enemy.tag = "Player";

            CharClone = Characters[Random.Range(0, Characters.Length - 1)];
            Instantiate(CharClone, new Vector3(Random.Range(-12, 12), transform.position.y), transform.rotation);

            GameObject[] objs = GameObject.FindGameObjectsWithTag("Character");
            foreach(GameObject ob in objs)
            {
                ob.GetComponent<Character>().Control = new AIControl(Enemy);
                ob.layer = 9;
                ob.tag = "Character";
            }
        }


        GameObject.Destroy(gameObject);
    }


    
    public string GetInformation()
    {
        string value = "";
        foreach (var Item in _baseInfo)
        {
            value += string.Format("{0}: {1}\n", Item.Key, Item.Value);
        }
        return value;
    }

    #endregion // Manual Method
}

public enum Directions
{
    Left = 1,
    Right = -1
}

