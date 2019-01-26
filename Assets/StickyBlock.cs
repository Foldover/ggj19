using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StickyBlock : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
      print(collision.gameObject.tag + " - " + gameObject.tag);
      if (collision.gameObject.tag == gameObject.tag)
      {
        print("collide!");
      }



    }
}
