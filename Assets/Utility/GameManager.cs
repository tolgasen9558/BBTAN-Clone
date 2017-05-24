using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    
    private BallDragLaunch ballDragLaunch;
    private BallController ballController;
	private BlockController blockController;
    private UIHandler uiHandler;

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
        uiHandler = FindObjectOfType<UIHandler>();

    }

    // Use this for initialization
    void Start(){
		
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DragFinished(Vector2 launchVector) {
        ballController.CurrentBallCount++;
        StartCoroutine(ballController.LaunchBalls(launchVector));
		ballDragLaunch.SetMouseEnabled(false);
    }

	public void LastBallHitGround(){
		blockController.SlideBlocksDown();
        ballController.InstantiateBallsIfNeeded();
        ballDragLaunch.SetMouseEnabled(true);
	}

    public void BlockHitGround() {
        print("here");
        ballDragLaunch.SetMouseEnabled(false);
        uiHandler.GameOver();
    }
}
