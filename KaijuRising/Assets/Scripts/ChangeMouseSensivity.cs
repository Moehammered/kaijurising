using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeMouseSensivity : MonoBehaviour 
{
	public Slider sensSlider;

	public void changeMouseSnsivity(float mouseSens)
	{
		PlayerPrefs.SetFloat("MouseSensivity", sensSlider.value);
	}
}