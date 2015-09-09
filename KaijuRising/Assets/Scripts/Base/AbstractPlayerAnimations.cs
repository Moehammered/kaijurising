using UnityEngine;
using System.Collections;

public abstract class AbstractPlayerAnimations : BaseAnimation {

	public string walking;
	public string[] attacks;
	public string specialAttack;
	
	public abstract void playerAttack();
	public abstract void playerWalking(bool isWalking);
	public abstract void playerSpecialAttack();
}
