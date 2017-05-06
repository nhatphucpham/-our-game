using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UiImage = UnityEngine.UI.Image;

public class HP_Scripts : MonoBehaviour
{
    public RectTransform Shadow;
    public Character MainCharacter;
    public Text HPText;
    public Directions Direction;
    public float _CurrentHP;

    private Vector2 PosXAndWidth;
    private uint _MaxHP;
    public Vector2 aPos;
    private Vector2 aSize;
    public float width;
    private bool isMissed;
    private RectTransform rectTransform;

    // Use this for initialization
    void Start()
    {
        Shadow = transform.parent.GetComponent<RectTransform>();
        HPText = transform.parent.parent.GetComponentInChildren<Text>();

        rectTransform = GetComponent<RectTransform>();

        PosXAndWidth = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.x);

        MainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharacter != null)
        {
            _CurrentHP = MainCharacter.CurrentHP;
            _MaxHP = MainCharacter.GetValue("HP").Value;
            aPos = rectTransform.anchoredPosition;
            aSize = rectTransform.sizeDelta;
            width = _CurrentHP / _MaxHP * PosXAndWidth.y;
            aPos.x = PosXAndWidth.x - (PosXAndWidth.y - width) / 2 * (int)Direction;
            rectTransform.anchoredPosition = aPos;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
        else
        {
            width = PosXAndWidth.y;
            MainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            if (MainCharacter == null)
            {
                width = 0;
                aPos.x = PosXAndWidth.x - (PosXAndWidth.y - width) / 2 * (int)Direction;
                rectTransform.anchoredPosition = aPos;
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            }
        }
        Shadow.anchoredPosition = rectTransform.anchoredPosition;
        if (width < PosXAndWidth.y - 10f)
            Shadow.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width + 10f*(Shadow.sizeDelta.x/PosXAndWidth.y));
        else
            Shadow.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, PosXAndWidth.y);
        if (width == 0)
        {
            Shadow.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        }
        HPText.text = string.Format("HP: {0}", _CurrentHP);
    }
}
