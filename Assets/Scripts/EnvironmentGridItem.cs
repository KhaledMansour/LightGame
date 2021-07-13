using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGridItem : MonoBehaviour
{
	[SerializeField]
	private bool isDrag;
	[SerializeField]
	private bool isRecevingLight;

	private void OnTriggerStay(Collider other)
	{
		if (!isDrag)
		{
			isRecevingLight = true;
		}
	}

	public void NotifyDragStatus(bool isDrag)
	{
		this.isDrag = isDrag;
		if (isDrag)
		{
			isRecevingLight = false;
		}
	}

	public bool IsItemReceiveLight()
	{
		return isRecevingLight;
	}
}
