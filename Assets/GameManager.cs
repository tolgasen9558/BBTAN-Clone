using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    public Vector2 BallStartPos {
        get { return m_ballStartPos; }
        set { m_ballStartPos = value; }
    }

    private BallDragLaunch ballDragLaunch;
    private BallController ballController;
    private Vector2 m_ballStartPos;
    private bool firstBallHitGround = false;


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
    }

    // Use this for initialization
    void Start(){
        BallStartPos = new Vector2(0, -3.8f);
        ballController.InstantiateBallsIfNeeded(BallStartPos);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DragFinished(Vector2 launchVector) {
        ballController.CurrentBallCount++;
        StartCoroutine(ballController.LaunchBalls(launchVector));
        firstBallHitGround = false;
    }

    public void BallHitGround(Ball ball){
        ball.HitGround();
        if (firstBallHitGround == false) {
            BallStartPos = ball.GetPosition();
            firstBallHitGround = true;
        }
        else {
            ball.MoveTo(BallStartPos);
        }
        ballController.InstantiateBallsIfNeeded(BallStartPos);
    }
}
