using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockSpawner : MonoBehaviour
{
    public static GameObject[] AvailableBlocks;


    void Start()
    {
         AvailableBlocks = Resources.LoadAll<GameObject>("House Building Blocks");
         print("Loaded " + AvailableBlocks.Length + " building blocks");
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "House Building Block")
        {
          GameObject buildingBlockPrefab = AvailableBlocks[Random.Range(0, AvailableBlocks.Length)];
          Instantiate(buildingBlockPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
