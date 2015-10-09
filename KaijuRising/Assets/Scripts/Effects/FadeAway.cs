using UnityEngine;
using System.Collections;

//

public class FadeAway : MonoBehaviour 
{
	public bool fadeNow = true;
	//time until fading happens
	public float delay = 1;
	//how long it takes to fade away
	public float durationOfFade = 1;
	public float timeUntilDestroyed = 3;
	

	private bool startLerp;
	private Color newColor;
	private Color originalColor;


	private void Update () 
	{
		if (fadeNow == true) 
		{
			fadeNow = false;
			StartCoroutine (delayFade ());
		}

		if (startLerp == true)
		{
			gameObject.GetComponent <Renderer> ().material.color = Color.Lerp (gameObject.GetComponent <Renderer> ().material.color, newColor, Time.deltaTime / durationOfFade);
			timeUntilDestroyed -= Time.deltaTime;
			if (timeUntilDestroyed < 0)
			{
				Destroy (gameObject);
			}
		}
	}
	
	public IEnumerator delayFade ()
	{
		setupColor ();
		yield return new WaitForSeconds (delay);
		startLerp = true;
	}

	private void setupColor ()
	{
		Renderer rend = gameObject.GetComponent <Renderer> ();
		if (rend != null) 
		{
			Material mat = rend.material;
			if (mat != null)
			{
				originalColor = mat.color;
				checkRenderingMode (mat);
			}
		}
		else
		{
			print ("No renderer attached to fading object");
		}
		newColor = new Color (originalColor.r, originalColor.g, originalColor.b, 0);
	}

	void checkRenderingMode (Material mat)
	{
		if (mat.GetFloat ("_Mode") != 3)
		{
			Debug.LogError ("Make sure the fadable material has a rendering mode of Transparent"); 
		}
	}

}
