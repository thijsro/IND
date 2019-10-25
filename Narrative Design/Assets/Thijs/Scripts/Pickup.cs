using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
	public bool isFinal = false;

	[SerializeField] int objectType;
	public bool canPlace = true;
	public bool isPlaced = false;

	public bool PlaceObject(Pickup currentObject, Transform PlaceObject)
	{
		if(currentObject.objectType == PlaceObject.GetComponent<Pickup>().objectType)
		{
			canPlace = true;
			Debug.Log("canplace is true");
		}
		else
		{
			canPlace = false;
		}
		return canPlace;
		
	}
	

	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
