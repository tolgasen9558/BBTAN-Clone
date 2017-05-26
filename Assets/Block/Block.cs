using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	public int HitLeft{get {return hitLeft;} }

	[SerializeField]
	private int hitLeft;
	[SerializeField]
	private float slideSpeed = 1f;

    private BlockController blockController;

	private Text hitText;
	private Material material;

	private Rigidbody2D rb2d;
	private Vector2 destination;
	private bool moveToDestination;

	// Use this for initialization
	void Start () {
        blockController = FindObjectOfType<BlockController>();
		rb2d = GetComponent<Rigidbody2D>();
		material = GetComponent<Renderer>().material;
		hitText = GetComponentInChildren<Text>();
		UpdateBoxColor();
		UpdateHitLeftText();
	}
	
	// Update is called once per frame
	void Update () {
		if(moveToDestination && Mathf.Abs(rb2d.position.y - destination.y) < 0.05f){
			moveToDestination = false;
			rb2d.position = destination;
			rb2d.velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		hitLeft--;
		if(hitLeft <= 0){
            blockController.DestroyBlock(this);
		}
		UpdateBoxColor();
		UpdateHitLeftText();
	}

	void UpdateHitLeftText(){
		hitText.text = hitLeft.ToString();
		hitText.color = material.color;
	}

	void UpdateBoxColor(){
		float maxNumHit = (float) Constants.FindMaxNumHit();
		float newGValue = 1 - hitLeft / maxNumHit;
		material.color = new Color(material.color.r, newGValue, material.color.b);
	}

	public void SlideDown(){
		moveToDestination = true;
		destination = (Vector2)transform.position + Vector2.down;
		rb2d.velocity = Vector2.down * slideSpeed;
	}

    public Vector2 GetPosition() {
        return rb2d.position;
    }

    public virtual void DisableCollider() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
