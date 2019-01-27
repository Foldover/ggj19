using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
	public static GameObject[] AvailableBlocks;

	public List<GameObject> spawnedBlocks;

	public static Color[] Colors = {
	  Color.red,
	  Color.blue,
	  Color.green
	};

	private void Start()
	{
		AvailableBlocks = Resources.LoadAll<GameObject>("House Building Blocks");
		print("Loaded " + AvailableBlocks.Length + " building blocks");
		Spawn();
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "House Building Block")
		{
			Spawn();
		}
	}

	private void Spawn()
	{
		GameObject buildingBlockPrefab = AvailableBlocks[Random.Range(0, AvailableBlocks.Length)];
		GameObject block = Instantiate(buildingBlockPrefab, this.transform.position, Quaternion.identity);
		StickyBlock sticky = block.AddComponent<StickyBlock>();
		sticky.color = Colors[Random.Range(0, Colors.Length)];
		block.GetComponent<Renderer>().material.SetColor("_Color", sticky.color);
		AddBlocksCheck(sticky.gameObject);
	}

	private void AddBlocksCheck(GameObject block)
	{
		spawnedBlocks.Add(block);
		Debug.Log("SpawnedBlocks: " + spawnedBlocks.Count);
		if (spawnedBlocks.Count > 100)
		{
			FindObjectOfType<DefaultNamespace.GameTimer>().OnGameEnd();
		}
	}
}