﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

	public bool menuVisible = false;
	public GameObject gameMenu;

	void Update()
	{
		escPressed();
		showMenu();
	}

	void escPressed()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			menuTriggered();
		}
	}

	public void menuTriggered()
	{
		if (menuVisible)
		{
			menuVisible = false;
		}
		else
		{
			menuVisible = true;
		}
	}

	public void showMenu()
	{
		if (menuVisible)
		{
			gameMenu.SetActive(true);
		}
		else
		{
			gameMenu.SetActive(false);
		}
	}

	public void quit()
	{
		Application.Quit();
	}
}
