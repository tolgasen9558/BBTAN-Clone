﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakVerticalPowerup : BasePowerUp {

    private LineRenderer lineRenderer;

    private bool flashTrigger = false;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
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
        if (ball != null) {
            isUsed = true;
            ShowLineRenderer();
            BlockController.Instance.BreakVerticalLine(transform.position.x);
            SoundManager.Instance.PlayLineBreak();
        }
    }

    void ShowLineRenderer() {
        Vector3 topPoint = new Vector3(transform.position.x, 5, 0);
        Vector3 bottomPoint = new Vector3(transform.position.x, -4, 0);
        lineRenderer.SetPosition(0, topPoint);
        lineRenderer.SetPosition(1, bottomPoint);
        flashTrigger = true;
    }
}
