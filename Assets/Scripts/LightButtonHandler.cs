using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightButtonHandler : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	GameObject lightObject;
	[SerializeField]
	Vector3 startPos;
	Button lightButton;

	//public void Init(GameObject lightObject, Vector3 startPos, Button lightButton)
	//{

	//}
    void Start()
    {
		lightButton = GetComponent<Button> ();
		lightButton.onClick.AddListener (OnLightSelected);
    }

	void OnLightSelected()
	{
		lightObject.SetActive (true);
		lightObject.transform.position = startPos;
		lightButton.interactable = false;
		EnvironmentGrid.instance.OnSelectLight ();
	}
}
