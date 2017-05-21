using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D rb2d;
	private float groundStopOffset = 0.18f;
	private Ground ground;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		ground = FindObjectOfType<Ground>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Launch(Vector2 direction){
		rb2d.velocity = direction * speed;
	}

	public void HitGround(){
		rb2d.velocity = Vector2.zero;
		float finalYPos = ground.transform.position.y
		                  + groundStopOffset;
		rb2d.position = new Vector2(rb2d.position.x, finalYPos);
	}

    public Vector2 GetPosition() {
        return rb2d.position;
    }

    public void SetPosition(Vector2 pos) {
        rb2d.position = pos;
    }
}
