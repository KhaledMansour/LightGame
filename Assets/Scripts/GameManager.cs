using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// if we will gonna made many levels generated from the code
//[System.Serializable]
//public class Level
//{
//	public List<LightProps> LightProps;
//}

//[System.Serializable]
//public class LightProps
//{
//	public LightsType lightType;
//	public float Fov;
//	public float ViewDistance;
//	public float startLightAngle;
//}

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField]
	private Material defaultMaterial;
	[SerializeField]
	private SpriteRenderer levelBG;
	private bool isLevelCompleted;
	[SerializeField]
	private List<Animator> levelCharacterAnimation;
    private void Start()
    {
		if (instance)
		{
			Destroy (instance);
		} else
		{
			instance = this;
		}
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnCompleteLevel ();
		}
	}

	public void OnCompleteLevel()
	{
		if (isLevelCompleted)
		{
			return;
		}
		isLevelCompleted = true;
		levelBG.material = defaultMaterial;
		var color = levelBG.color;
		color.a = 0.5f;
		levelBG.color = color;
	 	StartCoroutine( CompleteLevelAnimation ());
	}

	private IEnumerator CompleteLevelAnimation()
	{
		var color = levelBG.color;
		while (levelBG.color.a < 1)
		{
			yield return null;
			color.a += Time.deltaTime;
			levelBG.color = color;
		}
		foreach (var item in levelCharacterAnimation)
		{
			item.enabled = true;
			item.GetComponent<SpriteRenderer> ().material = defaultMaterial;
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene ("Level1");
	}
}
