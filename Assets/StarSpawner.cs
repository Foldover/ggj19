using UnityEngine;

public class StarSpawner : MonoBehaviour
{
	public GameObject[] particlePrefabs;

	// Start is called before the first frame update
	private void Start()
	{
		var particleToSpawn = particlePrefabs[UnityEngine.Random.Range(0, particlePrefabs.Length - 1)];
		Instantiate(particleToSpawn, transform);
	}
}