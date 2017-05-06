using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEF : MonoBehaviour {

    public ParticleSystem thisParticle;
	// Use this for initialization
	void Start () {
        thisParticle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (thisParticle.isPlaying)
            return;
        Destroy(gameObject);
	}
}
