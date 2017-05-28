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
    private int m_currentBallCount = 10;


	private Vector2 m_ballStartPos;
	public Vector2 BallStartPos {
		get { return m_ballStartPos; }
		set { m_ballStartPos = value; }
	}
    private int fallenBalls = 0;

	// Use this for initialization
	void Start () {
        ballsArray = new List<Ball>();
		BallStartPos = new Vector2(0, -3.8f);
		InstantiateBallsIfNeeded();
    }

    // Update is called once per frame
    void Update () {

	}

    public IEnumerator LaunchBalls(Vector2 launchVector) {
        fallenBalls = 0;
        foreach (Ball ball in ballsArray.ToArray()) {
            ball.Launch(launchVector);
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
		if (fallenBalls == 1) {
			BallStartPos = ball.GetPosition();
		}
		else {
			ball.MoveTo(BallStartPos);
		}
		if(fallenBalls == ballsArray.Count){
			GameManager.instance.LastBallHitGround();
		}
	}
}
