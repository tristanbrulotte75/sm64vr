﻿using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
    public GameObject menu;
    public GameObject titleActionText;
    public float titleActionFlickerSpeed = 1;

    protected float titleActionTimer;
    protected bool titleActionActive;

	protected void Start () {
        menu.SetActive (false);
        titleActionTimer = titleActionFlickerSpeed;
        titleActionActive = true;
	}
	
    protected void Update () {
        FlickerActionText ();
        UpdateAction ();
	}

    protected void FlickerActionText() {
        if (!titleActionActive) {
            return;
        }

        titleActionTimer -= Time.deltaTime;
        if (titleActionTimer < 0) {
            titleActionText.SetActive(!titleActionText.activeSelf);
            titleActionTimer = titleActionFlickerSpeed;
        }
    }

    protected void UpdateAction () {        
        // Keyboard
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)) {
            menu.SetActive (true);
            titleActionText.SetActive(false);
            titleActionActive = false;
        }
    }
}