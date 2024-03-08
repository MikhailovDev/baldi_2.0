using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour
{
	void Awake()
	{
		if (SceneManager.GetActiveScene().buildIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			PlayerPrefs.SetInt("PassedTests", 0);
		}
	}

	void Start()
	{
		StopGameLogic.LoadObjects();
	}
}