using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LaunchDie : MonoBehaviour
{
	public Transform origin;
	public GameObject diePrefab;
	
	public float force = 100;
	public float forceStep = 50;
	public float minForce = 0;
	public float maxForce = 1000;
	public Slider ForceIndicator;

	public float coolDownReset = 1.0f;
	private float coolDown = 0f;

	public int dice = 30;
	public Text DiceLeftText;

	private float gameOverTime = 5f;
	public GameObject GameOverPanel;

	void Update ()
	{
		// Update our dice count text
		this.DiceLeftText.text = String.Format("x{0}", this.dice);

		// If the game is over...
		if (this.IsGameOver ()) {
			// Show the game over screen (but not right away!)
			if (this.gameOverTime > 0) {
				this.gameOverTime -= Time.deltaTime;
			} else {
				this.GameOverPanel.SetActive(true);
			}
			return;
		}

		// Enforce cool down timer
		if (this.coolDown > 0f) {
			this.coolDown -= Time.deltaTime;
			return;
		}

		// If we've clicked
		if (Input.GetButton("Fire1")) {
			// Start adjusting the force amount 
			this.force = Mathf.Min(this.force + this.forceStep, this.maxForce);
			this.ForceIndicator.value = this.force / this.maxForce;
		}

		// If we've let the mouse up...
		if (Input.GetButtonUp("Fire1")) {
			// Get the world position of where we clicked
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
				// Make our die and launch it!
				var inst = (GameObject)Instantiate(this.diePrefab, origin.position, UnityEngine.Random.rotation);
				inst.GetComponent<Rigidbody> ().AddForce ((hit.point - origin.position).normalized * this.force);
			}

			// Reset force amount
			this.force = this.minForce;
			this.ForceIndicator.value = this.minForce / this.maxForce;

			// Reset fire cool down
			this.coolDown = this.coolDownReset;

			// Reduce number of dice left to throw
			this.dice--;
		}
	}

	private bool IsGameOver()
	{
		// Game is over if we have no more dice to throw
		// or if there are no more points to be obtained
		return this.dice <= 0 || GameObject.FindGameObjectsWithTag ("Player").Length == 0;
	}
}
