using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerup : MonoBehaviour {

    private BallController ballController;
    private SoundManager soundManager;

	// Use this for initialization
	void Start () {
        ballController = FindObjectOfType<BallController>();
        soundManager = FindObjectOfType<SoundManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if(ball != null) {
            ballController.CurrentBallCount++;
            soundManager.PlayBallPowerup();
        }
        Destroy(gameObject);
    }
}
