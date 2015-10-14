using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCommands : MonoBehaviour {

	public GameObject menuMain;
	public GameObject menuOptions;
	public GameObject menuKaijuSelect;
	public GameObject menuMatch;
	public GameObject menuServer;
	
	
	void Awake()
	{
//		DontDestroyOnLoad(menuMain);
		DontDestroyOnLoad(menuServer);
	}
	
	void Start () 
	{
		hideAllMenus();
		menuMain.SetActive(true);
	}
	
	private void hideAllMenus()
	{
		menuMain.SetActive(false);
		menuMatch.SetActive(false);
		menuKaijuSelect.SetActive(false);
		menuOptions.SetActive(false);
		menuServer.SetActive(false);
	}
	
	public void showMain()
	{
		hideAllMenus();
		menuMain.SetActive(true);
	}
	
	public void showOptions()
	{
		hideAllMenus();
		menuOptions.SetActive(true);
	}
	
	public void showMatch()
	{
		hideAllMenus();
		menuMatch.SetActive(true);
	}
	
	public void showKaijuSelect()
	{
		hideAllMenus();
		menuKaijuSelect.SetActive(true);
	}
	
	public void showServer()
	{
		hideAllMenus();
		menuServer.SetActive(true);
	}
	
	public void hideServer()
	{
		hideAllMenus();
	}
	
	public void quit()
	{
		Application.Quit();
	}

}
