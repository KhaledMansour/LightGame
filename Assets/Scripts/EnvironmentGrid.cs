using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnvironmentGrid : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	float xBoundary;
	[SerializeField]
	float YBoundary;
	[SerializeField]
	EnvironmentGridItem gridItemPrefab;
	[SerializeField]
	List<EnvironmentGridItem> observersGridItems;
	private Vector3 gridItemScale;
	public static EnvironmentGrid instance;
	private bool isLightDrag;
	private const int levelLightsCount = 5;
	private int DraggedLightsCount = 0;

	void Start()
    {
		if (instance)
		{
			Destroy (this);
		}
		else
		{
			instance = this;
		}
		gridItemScale = gridItemPrefab.transform.localScale;
		observersGridItems = new List<EnvironmentGridItem> ();
		SpawnGridItems ();
	}

	private void SpawnGridItems()
	{
		for (float y = -YBoundary + gridItemScale.y; y <= YBoundary - gridItemScale.y; y += gridItemScale.y * 2)
		{
			for (float x = -xBoundary + gridItemScale.x; x <= xBoundary - gridItemScale.x; x += gridItemScale.x * 2)
			{
				var item = Instantiate (gridItemPrefab, transform);
				item.transform.position = new Vector3 (x, y, 0);
				observersGridItems.Add (item);
			}
		}
	}

	public void Update()
	{
		if (DraggedLightsCount >= levelLightsCount && !isLightDrag)
		{
			Debug.LogError ("win state = " + CheckRoomReceiveLights ());
			if (CheckRoomReceiveLights ())
			{
				GameManager.instance.OnCompleteLevel ();
			}
		}
	}

	public bool CheckRoomReceiveLights()
	{
		foreach (var item in observersGridItems)
		{
			if (!item.IsItemReceiveLight())
			{
				return false;
			}
		}
		return true;
	}

	public void LightDragingStatus(bool isDrag)
	{
		isLightDrag = isDrag;
		observersGridItems.ForEach (x => x.NotifyDragStatus (isDrag));
	
	}

	public void OnSelectLight()
	{
		DraggedLightsCount++;
	}

}
