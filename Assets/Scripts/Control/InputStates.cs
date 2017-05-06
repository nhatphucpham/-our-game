using UnityEngine;

public class InputStates
{
    public static bool IsRight
    {
        get
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static bool IsLeft
    {
        get
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static bool IsJump
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public static bool IsNormalAttack
    {
        get
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static bool IsSpecialAttack
    {
        get;
        set;
    }

    public static bool IsInput
    {
        get
        {
            return Input.anyKey ? true : false;
        }
    }
}

