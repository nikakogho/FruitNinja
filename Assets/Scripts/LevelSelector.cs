using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

	public void OpenLevel(string level)
	{
		SceneManager.LoadScene (level);
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void Reset()
	{
		PlayerPrefs.DeleteAll ();
	}
}
