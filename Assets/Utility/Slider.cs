﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector2 destination;
    private bool moveToDestination;
    private bool destroyAfterSlide = false;

    // Use this for initialization
    void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (moveToDestination && Mathf.Abs(rb2d.position.y - destination.y) < 0.05f) {
            moveToDestination = false;
            rb2d.position = destination;
            rb2d.velocity = Vector2.zero;
            if (destroyAfterSlide) {
                Destroy(gameObject);
            }
        }
    }

    public void SlideDown(float slideSpeed) {
        moveToDestination = true;
        destination = (Vector2)transform.position + Vector2.down;
        rb2d.velocity = Vector2.down * slideSpeed;

        BasePowerUp powerup = GetComponent<BasePowerUp>();
        if (powerup != null && (destination.y - transform.parent.position.y <= 0 || powerup.isUsed)) {
            destroyAfterSlide = true;
        }
    }
}
