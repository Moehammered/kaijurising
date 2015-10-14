using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public struct buttonSound
{
	public AudioClip yumkaxxRoar;
	public AudioClip gozuRoar;
	public AudioClip falsoRoar;
	public AudioClip rexRoar;
	public AudioClip trikarenosRoar;
	public AudioClip vokrouhRoar;
	public AudioClip kemonoRoar;
	public AudioClip mantraRoar;
	
	public AudioClip joinSound;
	public AudioClip defaultSound;
}

public class MenuAudio : MonoBehaviour 
{
	public buttonSound playSound;
	public AudioSource menuSound;
	
	public void menuButton()
	{
		menuSound.Play();
	}
	
	public void playButtonSound(Button button)
	{
	
		switch(button.tag)
		{
		case ("yumkaxx"):
				menuSound.clip = playSound.yumkaxxRoar;
				menuSound.Play();
			break;
			
		case ("gozu"):
				menuSound.clip = playSound.gozuRoar;
				menuSound.Play();
			break;
			
		case ("rex"):
				menuSound.clip = playSound.rexRoar;
				menuSound.Play();
			break;
			
		case ("falsol"):
				menuSound.clip = playSound.falsoRoar;
				menuSound.Play();
			break;
			
		case ("trikarenos"):
				menuSound.clip = playSound.trikarenosRoar;
				menuSound.Play();
			break;
			
		case ("vokrouh"):
				menuSound.clip = playSound.vokrouhRoar;
				menuSound.Play();
			break;
			
		case ("kemono"):
				menuSound.clip = playSound.kemonoRoar;
				menuSound.Play();
			break;
			
		case ("mantra"):
				menuSound.clip = playSound.mantraRoar;
				menuSound.Play();
			break;
		
		case ("enterGame"):
				menuSound.clip = playSound.joinSound;
				menuSound.Play();	
			break;
			
		default:
				menuSound.clip = playSound.defaultSound;
				menuSound.Play();
			break;
			
		}
	}
}
