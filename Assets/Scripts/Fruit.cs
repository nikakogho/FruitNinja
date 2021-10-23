using UnityEngine;

public class Fruit : MonoBehaviour {

	public float lifeTime = 4;
	public bool isBomb = false;
	public GameObject slicedVersion;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Blade")) 
		{
			Die (other.transform);
		}
	}

	void Start()
	{
		Invoke ("End", lifeTime);
	}

	void End()
	{
		if (!isBomb) 
		{
			WaveSpawner.instance.lives--;
		}

		Destroy (gameObject);
	}

	void Die(Transform tr)
	{
		if (!isBomb)
		{
			Vector3 dir = (tr.position - transform.position).normalized;

			Quaternion rotation = Quaternion.LookRotation (dir);
			rotation.y = 0;

			Destroy(Instantiate (slicedVersion, transform.position, rotation), 3);
			Blade.cut++;
			Destroy (gameObject);
		} else
		{
			GetComponent<Bomb> ().Explode ();
		}
	}
}
