using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightsType
{
	Scout, Candle
}
// i did this class if we wanna to expand with different lights types with different ranges and can spawn any level from the code
public abstract class Light : MonoBehaviour
{
	[SerializeField]
	private LayerMask obstaclesMask;
	[SerializeField]
	float viewDistance;
	[SerializeField]
	float lightFOV;
	[SerializeField]
	float startAngle;
	private Mesh mesh;
	private Vector3 origin;
	int rayCount = 50;
	float angle = 0f;
	Vector3[] vertices;
	Vector2[] uv;
	int[] triangles;

	private void Awake()
	{
		mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		var rot = transform.parent.eulerAngles;
		rot.z = startAngle;
		transform.parent.eulerAngles = rot;
		Draw ();
		gameObject.AddComponent<MeshCollider> ();
	}
	
	public void Draw()
	{
		mesh.Clear ();
		var angleIncrease = lightFOV / rayCount;
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
			var rayCastHit = Physics2D.Raycast (transform.position, GetVectorFromAngle (angle), viewDistance, obstaclesMask);
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

	//private void LateUpdate()
	//{
	//	Draw ();
	//	mesh.vertices = vertices;
	//	mesh.uv = uv;
	//	mesh.triangles = triangles;
	//}

	private Vector3 GetVectorFromAngle(float angle)
	{
		float angleToRad = Mathf.Deg2Rad * angle;
		return new Vector3 (Mathf.Cos (angleToRad), Mathf.Sin (angleToRad));
	}

	protected virtual void Init(int fov, int distance, LayerMask obstaclesMask)
	{
		lightFOV = fov;
		viewDistance = distance;
		this.obstaclesMask = obstaclesMask;
		mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		Draw ();
		gameObject.AddComponent<MeshCollider> ();
	}

}
public class ScoutLight : Light
{
     
}
