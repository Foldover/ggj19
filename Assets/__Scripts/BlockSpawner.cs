using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockSpawner : MonoBehaviour
{
    public static GameObject[] AvailableBlocks;


    public static string[] Colors = {
      "Red",
      "Blue",
      "Green"
    };


    void Start()
    {
         AvailableBlocks = Resources.LoadAll<GameObject>("House Building Blocks");
         print("Loaded " + AvailableBlocks.Length + " building blocks");
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "House Building Block")
        {
          print("Stick!");
          GameObject buildingBlockPrefab = AvailableBlocks[Random.Range(0, AvailableBlocks.Length)];
          GameObject block = Instantiate(buildingBlockPrefab, this.transform.position, Quaternion.identity);
          StickyBlock sticky = block.AddComponent<StickyBlock>();
          sticky.color = Colors[Random.Range(0, Colors.Length)];
        }
    }
}
