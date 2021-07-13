using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public abstract class Light
//{
//	public int lightFOV { get;private set; }
//	public int viewDistance { get;private set; }
//	public Light(int fov, int distance)
//	{
//		lightFOV = fov;
//		viewDistance = viewDistance;
//	}
//}

//public class ScoutLight : Light
//{
//	public ScoutLight(int fov, int distance):base(fov, distance)
//	{

//	}
//}
public enum LightsType
{
	Scout, Candle
}

//public class Level
//{
//	public lis
//}

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	[SerializeField]
	Material defaultMaterial;
	[SerializeField]
	SpriteRenderer levelBG;
	private bool isLevelCompleted;
	[SerializeField]
	List<Animator> levelCharacterAnimation;
    void Start()
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
}
