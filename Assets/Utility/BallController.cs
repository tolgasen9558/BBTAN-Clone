using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallController : MonoBehaviour {

    public GameObject ballPrefab;
    public int CurrentBallCount {
        get { return m_currentBallCount; }
        set { m_currentBallCount = value; }
    }

    private UIHandler uiHandler;

    private int m_currentBallCount = 1;
	private Vector2 m_ballStartPos;

    private List<Ball> ballsArray;
	public Vector2 BallStartPos {
		get { return m_ballStartPos; }
		set { m_ballStartPos = value; }
	}
    private int fallenBalls = 0;

	// Use this for initialization
	void Start () {
        uiHandler = FindObjectOfType<UIHandler>();
        ballsArray = new List<Ball>();
		BallStartPos = new Vector2(0, -3.8f);
		InstantiateBallsIfNeeded();
    }

    // Update is called once per frame
    void Update () {

	}

    public IEnumerator LaunchBalls(Vector2 launchVector) {
        fallenBalls = 0;
        for(int i = 0; i < ballsArray.Count; i++) {
            ballsArray[i].Launch(launchVector);
            uiHandler.UpdateBallCount(CurrentBallCount - i - 1);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void InstantiateBallsIfNeeded() {
        int ballsToInstantiate = CurrentBallCount - ballsArray.Count;
        if (ballsToInstantiate > 0) {
            for (int i = 0; i < ballsToInstantiate; i++) {
                Ball newBall = (Instantiate(ballPrefab, BallStartPos, Quaternion.identity) as GameObject).GetComponent<Ball>();
                ballsArray.Add(newBall);
				newBall.transform.parent = this.transform;
            }
        }
    }

	public void BallHitGround(Ball ball){
		fallenBalls++;
		ball.HitGround();
        //First Ball hit ground
		if (fallenBalls == 1) {
			BallStartPos = ball.GetPosition();
            GameManager.instance.FirstBallHitGround(BallStartPos);
		}
		else {
			ball.MoveTo(BallStartPos);
		}
        //Last Ball hit ground
        if (fallenBalls == ballsArray.Count){
			GameManager.instance.LastBallHitGround();
		}
	}
}
