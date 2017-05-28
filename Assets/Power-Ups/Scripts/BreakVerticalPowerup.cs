﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakVerticalPowerup : MonoBehaviour {

    private BlockController blockController;


    // Use this for initialization
    void Start () {
        blockController = FindObjectOfType<BlockController>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null) {
            blockController.BreakVerticalLine(transform.position.x);
        }
    }
}
