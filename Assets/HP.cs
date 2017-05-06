using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {

    Character MainCharacter;
    Vector3 scale;
    Vector3 pos;
	// Use this for initialization
	void Start () {
        MainCharacter = transform.parent.GetComponent<Character>();
        scale = transform.localScale;
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float newScaleX =  (MainCharacter.CurrentHP / MainCharacter.GetValue("HP").Value) * scale.x;
        transform.localScale = new Vector3(newScaleX, scale.y, scale.z);
	}
}
