using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    
    private BallDragLaunch ballDragLaunch;
    private BallController ballController;
	private BlockController blockController;
    private ScoreManager scoreManager;
    private UIHandler uiHandler;

	void Awake (){
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
        ballDragLaunch = FindObjectOfType<BallDragLaunch>();
        ballController = FindObjectOfType<BallController>();
        blockController = FindObjectOfType<BlockController>();
        uiHandler = FindObjectOfType<UIHandler>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DragFinished(Vector2 launchVector) {
        ballController.CurrentBallCount++;
        ballController.CurrentBallCount++;

        StartCoroutine(ballController.LaunchBalls(launchVector));
		ballDragLaunch.SetMouseEnabled(false);
    }

	public void LastBallHitGround(){
        uiHandler.UpdateScore(ballController.CurrentBallCount);
		blockController.SlideBlocksDown();
        ballController.InstantiateBallsIfNeeded();
        ballDragLaunch.SetMouseEnabled(true);
        scoreManager.IncreaseScore();
    }

    public void BlockHitGround() {
        ballDragLaunch.SetMouseEnabled(false);
        uiHandler.GameOver();
    }
}
