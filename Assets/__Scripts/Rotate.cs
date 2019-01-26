using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float SpeedZ = 100;
    public float SpeedY = 0;

    void Update()
    {
      transform.Rotate((Vector3.up * SpeedY + Vector3.forward * SpeedZ) * Time.deltaTime);
    }
}
