using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof (Rigidbody))]
public class FaceUp : MonoBehaviour
{
	[SerializeField]
	private int Face;

	private List<Vector3> directions;
	private List<int> sideValues;

	private Rigidbody rbody;
 
	void Awake ()
	{
		this.rbody = this.GetComponent<Rigidbody> ();

		this.directions = new List<Vector3>(6);
		this.sideValues = new List<int>(6);

		// For the sake of this example we assume a regular cube dice if 
		// directions haven't been specified in the editor. Sum of opposite
		// sides is 7, haven't consider exact real layout though.
		if (this.directions.Count == 0) {
			// Object space directions
			this.directions.Add(Vector3.up);
			this.sideValues.Add(2); // up
			this.directions.Add(Vector3.down);
			this.sideValues.Add(5); // down

			this.directions.Add(Vector3.left);
			this.sideValues.Add(6); // left
			this.directions.Add(Vector3.right);
			this.sideValues.Add(1); // right

			this.directions.Add(Vector3.forward);
			this.sideValues.Add(4); // fw
			this.directions.Add(Vector3.back);
			this.sideValues.Add(3); // back
		}

		// Assert equal side of lists
		if (this.directions.Count != this.sideValues.Count) {
			Debug.LogError("Not consistent list sizes");
		}
	}

	void Update () 
	{
		// Save some cpu cycles :)
		if (this.Face != -1 && !this.rbody.IsSleeping ()) {
			this.Face = -1;
			return;
		}

		// For sake of example, get number based on current orientation
		// This makes it possible to test by just rotating it in the editor and hitting play
		// Allowing 30 degrees error so will give (the side that is mostly upwards)
		// but will give -1 on "tie"
		this.Face = GetNumber(Vector3.up, 30f);
	}
		
	// Gets the number of the side pointing in the same direction as the reference vector,
	// allowing epsilon degrees error.
	public int GetNumber(Vector3 referenceVectorUp, float epsilonDeg = 5f) {
		// here I would assert lookup is not empty, epsilon is positive and larger than smallest possible float etc
		// Transform reference up to object space
		Vector3 referenceObjectSpace = transform.InverseTransformDirection(referenceVectorUp);

		// Find smallest difference to object space direction
		float min = float.MaxValue;
		int mostSimilarDirectionIndex = -1;
		for (int i=0; i < this.directions.Count; ++i) {
			float a = Vector3.Angle(referenceObjectSpace, this.directions[i]);
			if (a <= epsilonDeg && a < min) {
				min = a;
				mostSimilarDirectionIndex = i;
			}
		}

		// -1 as error code for not within bounds
		return (mostSimilarDirectionIndex >= 0) ? this.sideValues[mostSimilarDirectionIndex] : -1; 
	}
}
