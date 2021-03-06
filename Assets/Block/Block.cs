﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

    public int HitLeft {
        get { return hitLeft; }
        set { hitLeft = value; }
    }
	public GameObject explosion;

    [SerializeField]
	private int hitLeft;

	private Text hitText;
	private Material material;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
		explosion.GetComponent<ParticleSystem>().Stop();
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

	public void OnCollisionEnter2D(Collision2D collision){
        GotHit();
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

    public void GotHit() {
        hitLeft--;
        SoundManager.Instance.PlayBlockHit();
        if (hitLeft <= 0) {
			Expload();
            BlockController.Instance.DestroyBlock(this);
            return;
        }
        UpdateBoxColor();
        UpdateHitLeftText();
    }

	private void Expload(){
		Instantiate(explosion, transform.position, Quaternion.identity, BlockController.Instance.transform);
	}
}
