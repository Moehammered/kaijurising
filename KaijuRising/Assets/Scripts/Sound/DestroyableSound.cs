using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyableSound : NetworkBehaviour {

	public AudioSource audioSource;
	public AudioClip[] audioClips;
	private float time;

	public float seconds;

	void Start() 
	{
		time = audioClips[0].length;
		StartCoroutine (destroyClip ());
	} 

	private IEnumerator destroyClip() 
	{
		while (time + 1f > 0)
		{
			time -= Time.deltaTime;
			yield return null; //wait for a frame
		}
		CmdEndEvent();
	}

	private void CmdEndEvent() 
	{
		Destroy (gameObject, 0);
	}
}
