using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpript : MonoBehaviour {
    public int Damage = 0;
    public float Alpha = 255;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Alpha <= 0)
            Destroy(gameObject);
        if (Damage == 0)
            GetComponent<TextMesh>().text = "No Damage";
        else
            GetComponent<TextMesh>().text = string.Format("{0}", Damage);
        GetComponent<TextMesh>().color = new Color(255, 255, 255, Alpha/255);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 7;
        Alpha -= 20;
    }
}
