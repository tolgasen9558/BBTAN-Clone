using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	public int HitLeft{get {return hitLeft;} }

	[SerializeField]
	private int hitLeft;
	private Text hitText;
	private Material material;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
		hitText = GetComponentInChildren<Text>();
		UpdateBoxColor();
		UpdateHitLeftText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		hitLeft--;
		if(hitLeft <= 0){
			Destroy(gameObject);
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
}
