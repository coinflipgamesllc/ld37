using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour
{	
	public float rotationSpeed = 20f;

	void Update ()
	{
		this.transform.LookAt (Vector3.zero);
    	this.transform.RotateAround (Vector3.zero, Vector3.up, -1 * Input.GetAxis("Horizontal") * this.rotationSpeed * Time.deltaTime);
	}
}
