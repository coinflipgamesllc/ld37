using UnityEngine;
using System.Collections;

[RequireComponent(typeof (AudioSource))]
public class Collide : MonoBehaviour
{
	private AudioSource audio;

	void Start ()
	{
		this.audio = this.GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision c)
	{
		if (!c.gameObject.CompareTag ("Finish")) {
			audio.Play ();
		}
	}
}
