using System.Linq;
using UnityEngine;

public class Hook : MonoBehaviour {
    public Rigidbody2D pickedUpItem;
    public Collider2D pickUpCollider;

    // Start is called before the first frame update
    private void Start() {
        pickUpCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (pickedUpItem == null) {
                var results = new Collider2D[5];
                var collisions = Physics2D.OverlapCollider(pickUpCollider, new ContactFilter2D(), results);

                // To not grab floor etc
                var nonStaticColliders = results.Take(collisions).Where(x =>
                    x.attachedRigidbody != null
                    && !x.attachedRigidbody.isKinematic
                );

                if (nonStaticColliders.Any()) {
                    pickedUpItem = nonStaticColliders.First().attachedRigidbody;
                    pickedUpItem.isKinematic = true;
                    pickedUpItem.transform.SetParent(GetComponent<Transform>());
                    pickedUpItem.transform.localPosition = new Vector3();
                }
            } else {
                pickedUpItem.isKinematic = false;
                pickedUpItem.transform.parent = null;
                pickedUpItem.gameObject.AddComponent<DropEffect>();
                pickedUpItem.GetComponent<Rigidbody2D>().gravityScale = 8f;
                pickedUpItem = null;
            }
        }
    }


}
