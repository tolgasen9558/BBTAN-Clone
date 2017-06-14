using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbtanController : Singleton<BbtanController> {

    public float speed = 5f;

    private Rigidbody2D rb2d;
    private bool moveTowardsDestination;
    private Vector2 destination;
    [SerializeField] private Vector2 offset;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (moveTowardsDestination == false) { return; }

        //If Moving Right
        if(rb2d.velocity.x > 0 && GetXPosWithOffset() >= destination.x) {
            StopMove();
            return;
        }
        if (rb2d.velocity.x < 0 && GetXPosWithOffset() <= destination.x) {
            StopMove();
            return;
        }
    }

    public void MoveTo(Vector2 pos) {
        this.destination = pos;
        moveTowardsDestination = true;
		if(GetXPosWithOffset() < pos.x){
			rb2d.velocity = Vector2.right * speed;
		}
		else{
			rb2d.velocity = Vector2.left * speed;
		}
    }

    private float GetXPosWithOffset() {
        return rb2d.position.x + offset.x;
    }

    private void StopMove() {
        moveTowardsDestination = false;
        rb2d.velocity = Vector2.zero;
        rb2d.position = destination - offset;
    }
}
