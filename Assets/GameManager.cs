using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private Ball ball;

	void Awake () {
		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start(){
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BallHitGround(){
		ball.HitGround();
	}
}
