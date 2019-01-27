using UnityEngine;

public class StickyBlock : MonoBehaviour
{
	public Color color;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		StickyBlock otherBlock = collision.gameObject.GetComponent<StickyBlock>();
		var ofSameColor = otherBlock && otherBlock.color == color;
		var bothDynamic = !collision.rigidbody.isKinematic && !collision.otherRigidbody.isKinematic;
		if (ofSameColor && bothDynamic)
		{
			FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
			joint.connectedBody = collision.rigidbody;
		}
	}
}