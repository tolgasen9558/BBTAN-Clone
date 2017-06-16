using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakHorizontalPowerup : BasePowerUp {

    private LineRenderer lineRenderer;
    private bool flashTrigger = false;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (flashTrigger) {
            lineRenderer.enabled = true;
            flashTrigger = false;
        }
        else {
            lineRenderer.enabled = false;
        }

	}

    void OnTriggerEnter2D(Collider2D other) {
        Ball ball = other.GetComponent<Ball>();
        if(ball != null) {
            isUsed = true;
            flashTrigger = true;
            BlockController.Instance.BreakHorizontalLine(transform.position.y);
            SoundManager.Instance.PlayLineBreak();
        }
    }
}
