using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 3f;

	private Rigidbody2D rb2d;
	private float groundStopOffset = 0.18f;
	private Ground ground;
	private bool moveTowardsCollect = false;
	private Vector2 collectedPos;

    void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		ground = FindObjectOfType<Ground>();
	}
	
	// Update is called once per frame
	void Update () {
		if(moveTowardsCollect && Mathf.Abs(rb2d.position.x - collectedPos.x) < 0.1f ){
			moveTowardsCollect = false;
			rb2d.velocity = Vector2.zero;
			rb2d.position = collectedPos;
		}
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

	public void MoveTo(Vector2 pos){
		this.collectedPos = pos;
		moveTowardsCollect = true;
		if(rb2d.position.x < pos.x){
			rb2d.velocity = Vector2.right * speed;
		}
		else{
			rb2d.velocity = Vector2.left * speed;
		}
	}

    public void ChangeMovementRandomly() {
        Vector2 randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 2f)).normalized;
        rb2d.velocity = randomVector * speed;
    }
}
