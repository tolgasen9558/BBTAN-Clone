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

    private List<Ball> ballsArray;
    private int m_currentBallCount = 1;


	private Vector2 m_ballStartPos;
	public Vector2 BallStartPos {
		get { return m_ballStartPos; }
		set { m_ballStartPos = value; }
	}
	private bool firstBallHitGround = false;
	private int ballsInPlay = 0;

	// Use this for initialization
	void Start () {
        ballsArray = new List<Ball>();
		BallStartPos = new Vector2(0, -3.8f);
		InstantiateBallsIfNeeded(BallStartPos);
    }

    // Update is called once per frame
    void Update () {
	}

    public IEnumerator LaunchBalls(Vector2 launchVector) {
		firstBallHitGround = false;
        foreach (Ball ball in ballsArray.ToArray()) {
            ball.Launch(launchVector);
			ballsInPlay++;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void InstantiateBallsIfNeeded(Vector2 pos) {
        int ballsToInstantiate = CurrentBallCount - ballsArray.Count;
		print("Balls To Instantiate = " + ballsToInstantiate);
        if (ballsToInstantiate > 0) {
            for (int i = 0; i < ballsToInstantiate; i++) {
                Ball newBall = (Instantiate(ballPrefab, pos, Quaternion.identity) as GameObject).GetComponent<Ball>();
                ballsArray.Add(newBall);
				newBall.transform.parent = this.transform;
            }
        }
    }

	public void BallHitGround(Ball ball){
		ballsInPlay--;
		ball.HitGround();
		if (firstBallHitGround == false) {
			BallStartPos = ball.GetPosition();
			firstBallHitGround = true;
		}
		else {
			ball.MoveTo(BallStartPos);
			if(ballsInPlay == 0){
				GameManager.instance.LastBallHitGround();
			}
		}
		InstantiateBallsIfNeeded(BallStartPos);
	}
}
