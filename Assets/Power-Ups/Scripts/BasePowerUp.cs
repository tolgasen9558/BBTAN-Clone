using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : MonoBehaviour {

    [HideInInspector]
    public bool isUsed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        print("isused");
        isUsed = true;
    }
}
