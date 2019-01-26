using System.Linq;
using UnityEngine;

public class Hook : MonoBehaviour
{
	private Rigidbody2D pickedUpItem;
	private Collider2D pickUpCollider;

	// Start is called before the first frame update
	private void Start()
	{
		pickUpCollider = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (pickedUpItem == null)
			{
				TryPickup();
			}
			else
			{
				TryDrop();
			}
		}
	}

	private void TryPickup()
	{
		var results = new Collider2D[5];
		var collisions = Physics2D.OverlapCollider(pickUpCollider, new ContactFilter2D(), results);

		// To not grab floor etc
		var nonStaticColliders = results.Take(collisions).Where(x =>
			x.attachedRigidbody != null
			&& !x.attachedRigidbody.isKinematic
		);

		if (nonStaticColliders.Any())
		{
			pickedUpItem = nonStaticColliders.First().attachedRigidbody;
			pickedUpItem.isKinematic = true;
            var differanceToChopper = pickedUpItem.transform.position - transform.position;
            pickedUpItem.transform.position = transform.position;
            pickedUpItem.transform.SetParent(GetComponent<Transform>());
            
            //var joints = pickedUpItem.GetComponents<Joint2D>().ToList();
            //while(joints.Any()) {
            //    joints = new System.Collections.Generic.List<Joint2D>();
            //    foreach (var joint in joints) {
            //        var connectedBodyPosition = new Vector3(joint.connectedBody.position.x, joint.connectedBody.position.y, 0);
            //        joint.connectedBody.position = connectedBodyPosition + differanceToChopper;
            //        joints.AddRange(joint.connectedBody.GetComponents<Joint2D>());
            //    }
            //}

			//Success Pickup sound
			AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.hookPickup, transform.position, _Fmod.Params.variation, 1f);
		}
		else
		{
			//Failed pickup sound
			AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.hookPickup, transform.position, _Fmod.Params.variation, 0f);
		}
	}

	private void TryDrop()
	{
		pickedUpItem.isKinematic = false;
		pickedUpItem.transform.parent = null;
		pickedUpItem.gameObject.AddComponent<DropEffect>();
		pickedUpItem.GetComponent<Rigidbody2D>().gravityScale = 8f;
		pickedUpItem = null;

		//Drop sound
		AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.hookDrop, transform.position, _Fmod.Params.variation, 1f);
	}
}