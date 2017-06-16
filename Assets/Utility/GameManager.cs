using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	public int ballGainEachLevel = 1;
	public bool slideBlocksEnabled = true;
	public bool spawnBlocksEnabled = true;

	void Awake (){
		if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start(){

    }

    public void DragFinished(Vector2 launchVector) {
        StartCoroutine(BallController.Instance.LaunchBalls(launchVector));
		BallDragLaunch.Instance.SetMouseEnabled(false);
    }

    public void FirstBallHitGround(Vector2 pos) {
        BbtanController.Instance.MoveTo(pos);
    }

	public void LastBallHitGround(){
        BallController.CurrentBallCount += ballGainEachLevel;
        UIHandler.Instance.UpdateScore(BallController.CurrentBallCount);
        UIHandler.Instance.UpdateBallCount(BallController.CurrentBallCount);

		if(spawnBlocksEnabled){BlockController.Instance.SpawnBlocksAndPowerups();}
		if(slideBlocksEnabled){BlockController.Instance.SlideBlocksDown();}
        
        BallController.Instance.InstantiateBallsIfNeeded();
        BallDragLaunch.Instance.SetMouseEnabled(true);
        ScoreManager.Instance.IncreaseScore();
    }

    public void BlockHitGround() {
        BallDragLaunch.Instance.SetMouseEnabled(false);
        UIHandler.Instance.GameOver();
    }

    public void ExitToMainScreen() {
        SceneManager.LoadScene(1);        
    }

    public void StartGameScreen() {
        SceneManager.LoadScene(0);
    }
}
