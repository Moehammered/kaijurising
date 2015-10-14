using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StopServer : MonoBehaviour {

	public NetworkManager manager;

	public void stopServer()
	{
		manager.StopServer();
		NetworkServer.Reset();
		Destroy (manager.gameObject);
		Destroy (gameObject);
	}
}
