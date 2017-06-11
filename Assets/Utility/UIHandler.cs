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
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button soundOnButton;
    [SerializeField] private Button soundOffButton;

    private BallController ballController;
    private Animator animator;
    private bool soundOnVisible = true;

    // Use this for initialization
    void Start () {
        ballController = FindObjectOfType<BallController>();
        animator = ballCount.GetComponent<Animator>();
        soundOnButton.gameObject.SetActive(soundOnVisible);
        soundOffButton.gameObject.SetActive(!soundOnVisible);
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

    public void OnPauseButtonPressed() {
        Time.timeScale = 0;
        pauseButton.gameObject.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void OnPlayButtonPressed() {
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void OnSoundButtonPressed() {
        print("here");
        soundOnVisible = !soundOnVisible;
        if (soundOnVisible) {
            soundOnButton.gameObject.SetActive(true);
            soundOffButton.gameObject.SetActive(false);
        }
        else {
            soundOnButton.gameObject.SetActive(false);
            soundOffButton.gameObject.SetActive(true);
        }
        GameManager.instance.SetSoundOnOff(soundOnVisible);
    }
}
