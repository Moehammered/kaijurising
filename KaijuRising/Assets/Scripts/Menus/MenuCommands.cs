using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCommands : MonoBehaviour {

	public GameObject menuMain;
	public GameObject menuStart;
	public GameObject menuOptions;
//	public GameObject menuMultiplayer;

	void Start () 
	{
		hideAllMenus();
		menuMain.SetActive(true);
	}

	private void hideAllMenus()
	{
		menuMain.SetActive(false);
		menuStart.SetActive(false);
		menuOptions.SetActive(false);
//		menuMultiplayer.SetActive(false);
	}

	public void showStart()
	{
		hideAllMenus();
		menuStart.SetActive(true);
	}

	public void hideStart()
	{
		hideAllMenus();
		menuMain.SetActive(true);
	}

	public void showOptions()
	{
		hideAllMenus();
		menuOptions.SetActive(true);
	}

	public void hideOptions()
	{
		hideAllMenus();
		menuMain.SetActive(true);
	}

	public void showMultiplayer()
	{
		hideAllMenus();
//		menuMultiplayer.SetActive(true);
	}

	public void hideMultiplayer()
	{
		hideAllMenus();
		menuStart.SetActive(true);
	}

	public void quit()
	{
		Application.Quit();
	}

}
