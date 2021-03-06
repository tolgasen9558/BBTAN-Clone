﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField] AudioClip blockHitSound;
    [SerializeField] AudioClip lineBreakPowerupSound;
    [SerializeField] AudioClip ballPowerupSound;
    [SerializeField] AudioClip spreadPowerUpSound;


    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayBlockHit() {
        audioSource.clip = blockHitSound;
        audioSource.Play();
    }

    public void PlayLineBreak() {
        audioSource.clip = lineBreakPowerupSound;
        audioSource.Play();
    }

    public void PlayBallPowerup() {
        audioSource.clip = ballPowerupSound;
        audioSource.Play();
    }

    public void PlaySpreadPowerup() {
        audioSource.clip = spreadPowerUpSound;
        audioSource.Play();
    }

    public void SetSoundOnOff(bool isOn) {
        if (isOn) {
            print("Sound On");
            audioSource.mute = false;
        }
        else {
            print("Sound Off");
            audioSource.mute = true;
        }
    }
}
