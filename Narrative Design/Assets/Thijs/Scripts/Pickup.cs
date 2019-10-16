using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public void OnPickup(GameObject newParent)
	{
		GetComponent<Collider>().enabled = false;
		transform.parent = newParent.transform;
		transform.position = newParent.transform.position;
	}
}
