using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
 
[RequireComponent(typeof(Rigidbody))]
public abstract class AbstractMover : NetworkBehaviour{

	public float speed;
	protected new Rigidbody rigidbody; // Rigidbody of the mover.
	protected Vector3 direction;

	protected virtual void Awake()
	{
		getRigidbody();
	}

	private void getRigidbody()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	public virtual void move(Vector3 direction, float speed)
	{
		rigidbody.MovePosition (transform.position + direction * speed * Time.deltaTime);
	}
}