using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadPowerup : MonoBehaviour {

    private SoundManager soundManager;

    // Use this for initialization
    void Start () {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null) {
            ball.ChangeMovementRandomly();
            soundManager.PlaySpreadPowerup();
        }
    }
}
