using UnityEngine;
using UnityEngine.UI;

public class Blade : MonoBehaviour {

	public static int score = 0;
	public Text scoreText;
	public Transform parent;
	public GameObject cutText;
	public GameObject trail;
	private GameObject rend;
	private Collider2D col;
	public float minSpeed = 0.001f;
	private bool cutting = false;
	private Vector3 previousPos, newPos;
	private Rigidbody2D rb;
	public static int cut = 0;
	public Color[] hitColors;

	void Start()
	{
		Time.timeScale = 1;
		score = 0;
		cut = 0;
		rb = GetComponent<Rigidbody2D> ();
		col = GetComponent<Collider2D> ();
		previousPos = rb.position;
		newPos = rb.position;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			StartCutting ();
		}else if (Input.GetMouseButtonUp (0)) 
		{
			StopCutting ();
		}

		rb.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		newPos = rb.position;

		if (cutting) 
		{
			if (Vector3.Distance (newPos, previousPos) < minSpeed / Time.deltaTime)
			{
				col.enabled = false;

				ScoreCut ();
			} else 
			{
				col.enabled = true;
			}
		}

		previousPos = newPos;
	}

	void ScoreCut()
	{
		if (cut > 0) 
		{
			Text text = Instantiate (cutText, transform.position, Quaternion.identity).GetComponent<Text> ();

			text.transform.parent = parent;

            if (cut > 1)
                text.text = cut.ToString ();

			float Index = Mathf.Clamp ((float)cut / (float)hitColors.Length, 0, 1);

			int index = Mathf.RoundToInt (Index * hitColors.Length) - 1;
            
			text.color = hitColors [index];

			Destroy (text.gameObject, 1.5f);
		}

        int cutScore = cut * cut;

		score += cutScore;
		scoreText.text = "Score : " + score;
		cut = 0;
	}

	void StartCutting()
	{
		ScoreCut ();
		cutting = true;
		if (rend != null)
			Destroy (rend);
		rend = Instantiate (trail, transform.position, transform.rotation, transform);
		col.enabled = false;
	}

	void StopCutting()
	{
		cutting = false;
		rend.transform.parent = null;
		Destroy (rend, 2);
		ScoreCut ();
		col.enabled = false;
	}
}
