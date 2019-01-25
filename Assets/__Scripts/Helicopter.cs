using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public float Speed =0.5; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey ("d")) {
            transform.position  = new Vector3(transform.position.x +Speed, transform.position.y, 0);
        } 
        if (Input.GetKey ("a")) {
            transform.position  = new Vector3(transform.position.x -Speed, transform.position.y, 0);
        } 
        if (Input.GetKey ("w")) {
            transform.position  = new Vector3(transform.position.x , transform.position.y+Speed,0); 
        } 
        if (Input.GetKey ("s")) {
            transform.position  = new Vector3(transform.position.x , transform.position.y-Speed, 0);
            
        } 
            
    }
}
