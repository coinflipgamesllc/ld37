using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RecordPoint : MonoBehaviour
{
	public Text PlayerScoreText;
	public Text OpponentScoreText;
	public Text ScoreText;

	public Text GameOverPlayerScoreText;
	public Text GameOverOpponentScoreText;
	public Text GameOverScoreText;

	public GameObject PlayerParticlesPrefab;
	public GameObject OpponentParticlesPrefab;
	public GameObject PlainParticlesPrefab;

	[SerializeField]
	private int PlayerScore;

	[SerializeField]
	private int OpponentScore;

	void Start ()
	{
		this.Reset ();
	}

	void Reset ()
	{
		this.PlayerScore = 0;
		this.OpponentScore = 0;
	}

	void Update ()
	{
		this.PlayerScoreText.text = GameOverPlayerScoreText.text = String.Format("+{0}", this.PlayerScore);
		this.OpponentScoreText.text = GameOverOpponentScoreText.text = String.Format("-{0}", this.OpponentScore);
		this.ScoreText.text = GameOverScoreText.text = String.Format("{0}", this.PlayerScore - this.OpponentScore);
	}

	void OnTriggerEnter (Collider other)
	{
		GameObject particles;
		if (other.gameObject.CompareTag ("Player")) {
			this.PlayerScore++;
			particles = (GameObject)Instantiate(this.PlayerParticlesPrefab, other.transform.position, other.transform.rotation);
		} else if (other.gameObject.CompareTag ("Opponent")) {
			this.OpponentScore++;
			particles = (GameObject)Instantiate(this.OpponentParticlesPrefab, other.transform.position, other.transform.rotation);
		} else {
			particles = (GameObject)Instantiate(this.PlainParticlesPrefab, other.transform.position, other.transform.rotation);
		}

		Destroy (other.gameObject); // If it fell, remove it!
		Destroy(particles, 1f);
	}
}
