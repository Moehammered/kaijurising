using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PulseButton : MonoBehaviour {

	public float timeScaleAmount;
	public bool specialReady;
	private Vector3 currentScale;
	private bool getBigger;

	void Awake()
	{
		currentScale = gameObject.transform.localScale;
	}

	void Update()
	{
		getScale();

		if (specialReady)
		{
			changeSize();
			pulse ();
		}
	}

	void getScale()
	{
		currentScale = gameObject.transform.localScale;
	}

	void pulse()
	{
		if (currentScale.x >= 1)
		{
			getBigger = false;
		}
		else if (currentScale.x <= 0.2f)
		{
			getBigger = true;
		}
	}

	void changeSize()
	{
		if (getBigger)
		{
			currentScale.x += timeScaleAmount * Time.deltaTime;
			currentScale.y += timeScaleAmount * Time.deltaTime;
			currentScale.z += timeScaleAmount * Time.deltaTime;
			gameObject.transform.localScale = currentScale;
		}
		else if (getBigger == false)
		{
			currentScale.x -= timeScaleAmount * Time.deltaTime;
			currentScale.y -= timeScaleAmount * Time.deltaTime;
			currentScale.z -= timeScaleAmount * Time.deltaTime;
			gameObject.transform.localScale = currentScale;
		}
	}
}
