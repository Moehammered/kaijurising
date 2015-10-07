using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHUD : NetworkBehaviour {

	public NetworkManager manager;
	public InputField ipAddressInput;
	public InputField portInput;
	public GameObject menuServer;
	public Text portNo;

	// Runtime variable
//	bool showServer = false;
	
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		portInput.text = "7777";
		ipAddressInput.text = "localhost";

	}

	public void host()
	{
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
