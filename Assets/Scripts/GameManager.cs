using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text highScoreText;
	public GameObject pauseUI;
	private bool paused = false;
	private string levelName;

	void Awake()
	{
		levelName = SceneManager.GetActiveScene ().name;
	}

	void Update()
	{
		if (Input.GetKeyDown ("p")) 
		{
			paused = !paused;

			if (paused) 
			{
				Time.timeScale = 0;
			} else
				Time.timeScale = 1;
			
			pauseUI.SetActive (paused);
		}
	}

	public void GoToLevel(string level)
	{
		SceneManager.LoadScene (level);
	}


	public void Restart()
	{
		GoToLevel (levelName);
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void SetHighScore()
	{
		int highScore = PlayerPrefs.GetInt ("HighScore" + levelName, 0);

		if (highScore < Blade.score)
			highScore = Blade.score;

		highScoreText.text = "HighScore : " + highScore;

		PlayerPrefs.SetInt ("HighScore" + levelName, highScore);
	}
}
