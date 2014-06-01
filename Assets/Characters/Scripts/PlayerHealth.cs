﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 8;						// Maximum amount of health that player can have
	public int health = 8;							// Player's current health
	public float recoveryInvincibilityTime = 3;		// Amount of time that player is invincible after being hurt
	public float respawnTime = 3;					// Time until player respawns
	public AudioClip hurtAudioClip;
	public AudioClip deathAudioClip;

	private int initHealth;							// Stores initial health at start of level to handle respawning
	private bool beingDamaged;						// True if player is currently being damaged
	private bool dead;								// If player is dead

	void Awake() {
		initHealth = health;
	}

	void Start () {
		health = initHealth;
		beingDamaged = false;
		dead = false;
	}

	void Update () {
		if (health <= 0 && !dead) {
			Death();
		}
	}

	public void Damage (int damage) {
		if (!beingDamaged) {
			beingDamaged = true;
			health -= damage;

			if (health <= 0) {
				return;
			}

			audio.clip = hurtAudioClip;
			audio.Play();
			Debug.Log ("Player health is " + health + "/" + maxHealth);
			StartCoroutine (Invincibility (recoveryInvincibilityTime));
		}
	}

	public bool IsDead() {
		return dead;
	}

	protected IEnumerator Invincibility (float time) {
		yield return new WaitForSeconds(time);
		beingDamaged = false;
	}

	protected void Death() {
		if (dead) {
			return;
		}

		dead = true;

		audio.clip = deathAudioClip;
		audio.Play();

		FPSInputController playerInput = GetComponent<FPSInputController> ();

		if (playerInput) {
			playerInput.SetInputEnabled(false);
		}

		StartCoroutine (Respawn (respawnTime));
	}
	
	protected IEnumerator Respawn (float length) {		
		yield return new WaitForSeconds(length);
		Application.LoadLevel (Application.loadedLevel);
	}
}
