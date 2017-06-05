using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour {

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text ballCount;
    [SerializeField] private Button pauseButton;

    private BallController ballController;
    private Animator animator;

    // Use this for initialization
    void Start () {
        ballController = FindObjectOfType<BallController>();
        animator = ballCount.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
    }

    public void UpdateScore(int newScore) {
        scoreText.text = newScore.ToString();
    }

    public void UpdateHighScore(int newScore) {
        highScoreText.text = "TOP\n" + newScore.ToString();
    }

    public void UpdateBallCount(int newBallCount) {
        ballCount.text = "x" + newBallCount.ToString();
        if(newBallCount == ballController.CurrentBallCount) {
            animator.SetTrigger("open_trigger");
        }
        else if(newBallCount == 0) {
            animator.SetTrigger("close_trigger");
        }
    }
}
