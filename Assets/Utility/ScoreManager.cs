using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private UIHandler uiHandler;


    private static int m_currentScore;
    private int m_highScore;

    public static int CurrentScore {
        get { return m_currentScore; }
        set { m_currentScore = value; }
    }

    public int HighScore {
        get { return m_highScore; }
        set { m_highScore = value; }
    }

    // Use this for initialization
    void Start () {
        uiHandler = FindObjectOfType<UIHandler>();

        m_currentScore = 1;

        //Initialise if high score not saved
        if (PlayerPrefs.GetInt("high_score") == 0) {
            PlayerPrefs.SetInt("high_score", m_currentScore);
        }
        else {
            m_highScore = PlayerPrefs.GetInt("high_score");
        }
        uiHandler.UpdateScore(m_currentScore);
        uiHandler.UpdateHighScore(m_highScore);

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void IncreaseScore() {
        m_currentScore++;
        uiHandler.UpdateScore(m_currentScore);

        if (m_currentScore > m_highScore) {
            m_highScore = m_currentScore;
            uiHandler.UpdateHighScore(m_highScore);
            PlayerPrefs.SetInt("high_score", m_highScore);
        }
    }
}
