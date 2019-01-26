using UnityEngine;

public class Helicopter : MonoBehaviour
{
	public float speed = 0.5f;
	private Rigidbody2D body;

	private void Start()
	{
		body = gameObject.GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (Input.GetKey("d"))
		{
			body.AddForce(Vector3.right * speed);
		}
		if (Input.GetKey("a"))
		{
			body.AddForce(Vector3.left * speed);
		}
		if (Input.GetKey("w"))
		{
			body.AddForce(Vector3.up * speed);
		}
		if (Input.GetKey("s"))
		{
			body.AddForce(Vector3.down * speed);
		}
	}
}