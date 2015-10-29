using UnityEngine;
using System.Collections;

public class TutorialMenu : MonoBehaviour 
{
	public float characterSelectTime;
	public MenuCommands menuCmds;
	public GameObject tutorialCanvas;

	public void stallCanvas()
	{
		tutorialCanvas.SetActive(true);
		menuCmds.gameObject.SetActive(false);
		StartCoroutine(olives());
	}
	
	private IEnumerator olives()
	{
		while(characterSelectTime > 0)
		{
			characterSelectTime -= Time.deltaTime;
			yield return null;
		}
		menuCmds.showKaijuSelect();
	}
}
