using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.IO;

public class NetworkHUD : NetworkBehaviour {

	public NetworkManager manager;
	public InputField ipAddressInput;
	public InputField portInput;
	public GameObject menuServer;
	public Text portNo;
	
	protected FileInfo theSourceFile = null;
	protected StreamReader reader = null;
	protected string text = ""; // assigned to allow first line to be read below

	// Runtime variable
//	bool showServer = false;
	
	void Awake()
	{
		//DontDestroyOnLoad(transform.gameObject);
		portInput.text = "7777";
		theSourceFile = new FileInfo ("IPConfig.txt");
		reader = theSourceFile.OpenText();
		if (text != null) {
			text = reader.ReadLine();
			//Console.WriteLine(text);
			ipAddressInput.text = text;
			clientJoin();
		}
		
	//	ipAddressInput.text = "localhost";

	}

	public void host()
	{
		manager.StopClient();
		manager.StartHost();
	}

	public void clientJoin()
	{
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
