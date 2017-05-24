using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour {

    [SerializeField]
    private Text gameOverText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
    }

}
