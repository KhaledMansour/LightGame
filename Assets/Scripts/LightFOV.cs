using UnityEngine;

public class LightFOV : MonoBehaviour
{
	// Start is called before the first frame update

	private Mesh mesh;
	[SerializeField]
	private LayerMask obstaclesMask;
	private Vector3 origin;
	private float startAngle;

	public bool enableDraw;
	[SerializeField]
	float viewDistance;
	[SerializeField]
	float fov;
	int rayCount = 50;
	float angle = 0f;
	[SerializeField]
	Vector3[] vertices;
	Vector2[] uv;
	[SerializeField]
	int[] triangles;
	void Start()
	{
		mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		Draw ();
		gameObject.AddComponent<MeshCollider> ();
	}

	public void Draw()
	{
		mesh.Clear ();
		angle = startAngle;
		var angleIncrease = fov / rayCount;
		vertices = new Vector3[rayCount + 2];
		uv = new Vector2[vertices.Length];
		triangles = new int[rayCount * 3];
		origin = Vector3.zero;
		vertices[0] = origin;
		int vertexIndex = 1;
		int triangleIndex = 0;
		for (int i = 0; i <= rayCount; i++)
		{
			Vector3 vertex;
			var rayCastHit = Physics2D.Raycast (transform.position , GetVectorFromAngle (angle), viewDistance, obstaclesMask);
			if (rayCastHit.collider)
			{
				vertex = ((Vector3)rayCastHit.point) - transform.position;
			}
			else
			{
				vertex = origin + GetVectorFromAngle (angle) * viewDistance;
			}
			
			vertices[vertexIndex] = vertex;
			Debug.DrawRay (transform.position, GetVectorFromAngle (angle) * viewDistance, Color.red);


			if (i > 0)
			{
				triangles[triangleIndex] = 0;
				triangles[triangleIndex + 1] = vertexIndex - 1;
				triangles[triangleIndex + 2] = vertexIndex;
				triangleIndex += 3;
			}
			vertexIndex++;
			angle -= angleIncrease;
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}

	private void LateUpdate()
	{
		if (enableDraw)
		{
			Draw ();
		}
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}

	private Vector3 GetVectorFromAngle(float angle)
	{
		float angleToRad = Mathf.Deg2Rad * angle;
		return new Vector3 (Mathf.Cos (angleToRad), Mathf.Sin (angleToRad));
	}
}
