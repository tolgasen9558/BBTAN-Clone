using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    public int HitLeft {
        get { return hitLeft; }
        set { hitLeft = value; }
    }

    [SerializeField]
	private int hitLeft;

    private BlockController blockController;

	private Text hitText;
	private Material material;

	private Rigidbody2D rb2d;
	private Vector2 destination;
	private bool moveToDestination;

	// Use this for initialization
	void Awake () {
        blockController = FindObjectOfType<BlockController>();
		rb2d = GetComponent<Rigidbody2D>();
		material = GetComponent<Renderer>().material;
		hitText = GetComponentInChildren<Text>();
	}

    void Start() {
		UpdateBoxColor();
		UpdateHitLeftText();
    }

    // Update is called once per frame
    void Update () {

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

    public Vector2 GetPosition() {
        return rb2d.position;
    }

    public virtual void DisableCollider() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

}
