using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomAddPlayerMessage : MessageBase {

	/*
	 * By default, UNet uses classes from "MessageBase" to send messages to the server via the function client.Send();
	 * UNet uses this to spawn a player.
	 * These MessageBase classes can hold basic variables such as ints, strings, floats which are carried thru the Send() function.
	 * Once the server receives the message, it can access the class's variables.
	 * I am using this same idea to spawn a kaiju that the player chooses, overriding UNet's default implementation.
	 */ 

	public string chosenKaiju = "";
}