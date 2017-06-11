using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int ballGainEachLevel = 1;
	public bool slideBlocksEnabled = true;
	public bool spawnBlocksEnabled = true;

	public static GameManager instance = null;
    
    private BallDragLaunch ballDragLaunch;
    private BallController ballController;
	private BlockController blockController;
    private ScoreManager scoreManager;
    private UIHandler uiHandler;
    private BbtanController bbtanController;

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
        bbtanController = FindObjectOfType<BbtanController>();

        uiHandler.UpdateBallCount(ballController.CurrentBallCount);
    }

    public void DragFinished(Vector2 launchVector) {
        StartCoroutine(ballController.LaunchBalls(launchVector));
		ballDragLaunch.SetMouseEnabled(false);
    }

    public void FirstBallHitGround(Vector2 pos) {
        bbtanController.MoveTo(pos);
    }

	public void LastBallHitGround(){
		ballController.CurrentBallCount += ballGainEachLevel;
        uiHandler.UpdateScore(ballController.CurrentBallCount);
        uiHandler.UpdateBallCount(ballController.CurrentBallCount);

		if(spawnBlocksEnabled){blockController.SpawnBlocks();}
		if(slideBlocksEnabled){blockController.SlideBlocksDown();}
        
        ballController.InstantiateBallsIfNeeded();
        ballDragLaunch.SetMouseEnabled(true);
        scoreManager.IncreaseScore();
    }

    public void BlockHitGround() {
        ballDragLaunch.SetMouseEnabled(false);
        uiHandler.GameOver();
    }

    public void SetSoundOnOff(bool isOn) {
        if (isOn) {
            print("Sound On");
        }
        else {
            print("Sound Off");
        }
    }
}
