using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour {

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Button pauseButton;

    // Use this for initialization
    void Start () {

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
}
