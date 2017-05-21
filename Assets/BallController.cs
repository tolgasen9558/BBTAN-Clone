using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public GameObject ballPrefab;
    public int CurrentBallCount {
        get { return m_currentBallCount; }
        set { m_currentBallCount = value; }
    }

    private List<Ball> ballsArray;
    private int m_currentBallCount = 1;

	// Use this for initialization
	void Start () {
        ballsArray = new List<Ball>();
    }

    // Update is called once per frame
    void Update () {
	}

    public IEnumerator LaunchBalls(Vector2 launchVector) {
        foreach (Ball ball in ballsArray.ToArray()) {
            ball.Launch(launchVector);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void InstantiateBallsIfNeeded(Vector2 pos) {
        int ballsToInstantiate = CurrentBallCount - ballsArray.Count;
        if (ballsToInstantiate > 0) {
            for (int i = 0; i < ballsToInstantiate; i++) {
                Ball newBall = (Instantiate(ballPrefab, pos, Quaternion.identity) as GameObject).GetComponent<Ball>();
                ballsArray.Add(newBall);
            }
        }
    }
}
