using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightButtonHandler : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	private GameObject lightObject;
	[SerializeField]
	private Vector3 startPos;
	private Button lightButton;

    private void Start()
    {
		lightButton = GetComponent<Button> ();
		lightButton.onClick.AddListener (OnLightSelected);
    }

	private void OnLightSelected()
	{
		lightObject.SetActive (true);
		lightObject.transform.position = startPos;
		lightButton.interactable = false;
		EnvironmentGrid.instance.OnSelectLight ();
	}
}
