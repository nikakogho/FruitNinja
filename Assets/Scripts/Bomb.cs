using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public void Explode()
	{
		FindObjectOfType<Animator>().SetTrigger ("Boom");
		WaveSpawner.instance.gameOver = true;
		Time.timeScale = 0.1f;
	}
}
