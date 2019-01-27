using UnityEngine;

public class Triangle : MonoBehaviour
{
	private Mesh mesh;
	private Vector3[] vertices;
	private int[] triangles;
	public Material material;

	// Start is called before the first frame update
	private void Start()
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		mesh = new Mesh();
		vertices = new[]
		{
			new Vector3(0, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(1, 0, 0),
		};

		mesh.vertices = vertices;
		GetComponent<MeshFilter>().mesh = mesh;
		triangles = new[] { 0, 1, 2 };
		mesh.triangles = triangles;

		gameObject.GetComponent<MeshRenderer>().material = material;
	}
}