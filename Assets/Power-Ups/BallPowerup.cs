using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerup : MonoBehaviour {

    private BallController ballController;

	// Use this for initialization
	void Start () {
        ballController = FindObjectOfType<BallController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if(ball != null) {
            ballController.CurrentBallCount++;
        }
        Destroy(gameObject);
    }
}
