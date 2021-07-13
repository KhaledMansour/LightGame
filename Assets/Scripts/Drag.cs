using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
	private Vector3 mOffset;
	private float zAxis;
	[SerializeField]
	private float xBoundary;
	[SerializeField]
	private float yBoundary;
	[SerializeField]
	Slider slider;

	private void Start()
	{
		if (slider)
		{
			slider.onValueChanged.AddListener (delegate { OnSliderValueChanged (); });
		}
	}

	private void OnSliderValueChanged()
	{
		Invoke ("chane", 0.3f);
	}
	void chane()
	{
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, slider.value * 90);
	}
	void OnMouseDown()
	{
		zAxis = Camera.main.WorldToScreenPoint (gameObject.transform.position).z;
		mOffset = gameObject.transform.position - GetMouseAsWorldPoint ();
	}

	private Vector3 GetMouseAsWorldPoint()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = zAxis;
		return Camera.main.ScreenToWorldPoint (mousePoint);
	}
	void OnMouseDrag()
	{
		var targetPos = GetMouseAsWorldPoint () + mOffset;
		targetPos = new Vector3 (Mathf.Clamp (targetPos.x, -xBoundary, xBoundary), Mathf.Clamp (targetPos.y, -yBoundary, yBoundary), targetPos.z);
		transform.position = targetPos ;
		EnvironmentGrid.instance.LightDragingStatus (true);
		//EnvironmentGrid.instance.spawnedGridItems.ForEach (x => x.OnLightDragged ());
	}

	private void OnMouseUp()
	{
		EnvironmentGrid.instance.LightDragingStatus (false);
		//EnvironmentGrid.instance.spawnedGridItems.ForEach (x => x.OnLightFinishDragged ());
	}
}
