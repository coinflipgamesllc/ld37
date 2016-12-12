using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGame : MonoBehaviour
{
	public void StartNewGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
