using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    

    private BallDragLaunch ballDragLaunch;
    private BallController ballController;
	private BlockController blockController;



	void Awake (){
		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

        ballDragLaunch = FindObjectOfType<BallDragLaunch>();
        ballController = FindObjectOfType<BallController>();
		blockController = FindObjectOfType<BlockController>();

    }

    // Use this for initialization
    void Start(){
		
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DragFinished(Vector2 launchVector) {
        ballController.CurrentBallCount++;
		print("Ball Count " + ballController.CurrentBallCount);
        StartCoroutine(ballController.LaunchBalls(launchVector));
    }

	public void LastBallHitGround(){
		blockController.SlideBlocksDown();
	}

    
}
