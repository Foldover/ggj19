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
		if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
		{
			body.AddForce(Vector3.right * speed);
		}
		if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
		{
			body.AddForce(Vector3.left * speed);
		}
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
		{
			body.AddForce(Vector3.up * speed);
		}
		if ((Input.GetKey("s") && transform.position.y > -2.2f || Input.GetKey(KeyCode.DownArrow) && transform.position.y > -2.2f)) 
		{
			body.AddForce(Vector3.down * speed);
		}
	}
}