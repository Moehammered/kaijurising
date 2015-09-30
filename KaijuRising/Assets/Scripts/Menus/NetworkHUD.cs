using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHUD : MonoBehaviour {

	public NetworkManager manager;
	public InputField ipAddressInput;
	public InputField portInput;

	// Runtime variable
//	bool showServer = false;
	
	void Awake()
	{
//		manager = GetComponent<CustomNetworkManager>();
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
}
