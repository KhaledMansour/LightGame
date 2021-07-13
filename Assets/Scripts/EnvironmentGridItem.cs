using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGridItem : MonoBehaviour
{
	[SerializeField]
	bool isDrag;
	[SerializeField]
	bool isRecevingLight;

	//private void OnTriggerEnter(Collider other)
	//{
	//	Debug.LogError (other.gameObject.name);
	//}

	//private void OnTriggerExit(Collider other)
	//{
	//	isRecevingLight = false;
	//}

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
