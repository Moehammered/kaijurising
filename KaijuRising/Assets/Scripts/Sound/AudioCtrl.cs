using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour {
	
	public void setVolume(Slider s)
	{
		AudioListener.volume = s.value;
	}
}
