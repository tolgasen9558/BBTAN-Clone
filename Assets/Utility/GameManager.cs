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
        uiHandler.UpdateScore(ballController.CurrentBallCount);
        HandleHighScore();
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
        uiHandler.UpdateScore(ballController.CurrentBallCount);
		blockController.SlideBlocksDown();
        ballController.InstantiateBallsIfNeeded();
        ballDragLaunch.SetMouseEnabled(true);
        HandleHighScore();
    }

    public void BlockHitGround() {
        ballDragLaunch.SetMouseEnabled(false);
        uiHandler.GameOver();
    }

    //TODO: Wrap this with PlayerPrefsManager
    private void HandleHighScore() {
        //Initialise if high score not saved
        if(PlayerPrefs.GetInt("high_score") == 0) {
            PlayerPrefs.SetInt("high_score", 1);
        }
        if(ballController.CurrentBallCount > PlayerPrefs.GetInt("high_score")) {
            PlayerPrefs.SetInt("high_score", ballController.CurrentBallCount);
        }
        uiHandler.UpdateHighScore(PlayerPrefs.GetInt("high_score"));
    }

}
