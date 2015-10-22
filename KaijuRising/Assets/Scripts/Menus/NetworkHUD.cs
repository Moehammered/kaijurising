using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

public class NetworkHUD : NetworkBehaviour {

	public bool isAutoConnecting = false;
	public NetworkManager manager;
	public InputField ipAddressInput;
	public InputField portInput;
	public GameObject menuServer;
	public Text portNo;
	private GameObject loadingScreen;
	
	protected FileInfo theSourceFile = null;
	protected StreamReader reader = null;
	protected StreamWriter writer = null;
	protected string text = ""; // assigned to allow first line to be read below

	// Runtime variable
//	bool showServer = false;
	
	void Awake()
	{
		// Initialise fields
		ipAddressInput.text = "localhost";
		portInput.text = "7777";

		if(isAutoConnecting == true)
		{
			autoConnect();
		}
	}

	private void autoConnect()
	{
		//loadingScreen = GameObject.Find("LoadingScreen");
		//print (loadingScreen.name);
		//DontDestroyOnLoad(transform.gameObject);

		theSourceFile = new FileInfo (Application.dataPath + "/IPConfig.txt");
		reader = theSourceFile.OpenText();
		text = reader.ReadLine();
		reader.Close();
		//Console.WriteLine(text);
		ipAddressInput.text = text;
		print (text);
		if (text != null) 
		{
			//	loadingScreen.GetComponent<Image>().enabled = true;
			clientJoin();
		}
		
		//	ipAddressInput.text = "localhost";
	}

	public void host()
	{
		//if(text == null)
		{
			writer = new StreamWriter(Application.dataPath + "/IPConfig.txt", false);
			writer.WriteLine(Network.player.ipAddress);
			//StreamWriter writer = new StreamWriter("IPConfig.txt", false);
			//writer.WriteLine("Network.player.ipAddress");
			writer.Close();
		}
		manager.StopClient();
		manager.StartHost();
	}

	public void clientJoin()
	{
		loadingScreen = GameObject.Find("LoadingScreen");
		print (loadingScreen.name);
		loadingScreen.GetComponent<Image>().enabled = true;
		if (ipAddressInput.text != null)
		{
			manager.networkPort = int.Parse(portInput.text);
			manager.networkAddress = ipAddressInput.text;
			manager.StartClient();
		}
	}

	public void startServer()
	{
		manager.StopClient();
		int portNumber = int.Parse(portInput.text);
		manager.networkPort = portNumber;
		portNo.text = "" + portNumber;
		manager.StartServer();
	}

	public void stopServer()
	{
		manager.StopServer();
		NetworkServer.Reset();
		Destroy (menuServer);
		Destroy (manager.gameObject);
		Destroy (gameObject);
	}
}
