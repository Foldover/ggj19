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
			pickedUpItem.transform.SetParent(GetComponent<Transform>());
			pickedUpItem.transform.localPosition = new Vector3();

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
		AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.hookDrop, transform.position, _Fmod.Params.variation, 0f);
	}
}