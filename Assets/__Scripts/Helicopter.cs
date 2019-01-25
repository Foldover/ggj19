using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float Speed = 0.5f; 

    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey ("d")) {
            body.AddForce(Vector3.right * Speed);
        } 
        if (Input.GetKey ("a")) {
            body.AddForce(Vector3.left * Speed);
        } 
        if (Input.GetKey ("w")) {
            body.AddForce(Vector3.up * Speed);
        } 
        if (Input.GetKey ("s")) {
            body.AddForce(Vector3.down * Speed);
        }   
    }
}
