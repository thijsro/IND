using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
	GameObject pickedUpObject;

	[SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] public LayerMask objectLayer;
	[SerializeField] public LayerMask placeLayer;
	[SerializeField] private float hitDistance = 10f;
    [SerializeField] private GameObject playerParent;
	[SerializeField] private GameObject placeParent;
    public Pickup currentObject;
	public Transform _selection;

	[SerializeField] Pickup[] pickups;
	[SerializeField] Pickup finalObject;
	

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
		{
			SetDefaultMaterial();
		}

		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, hitDistance, objectLayer))
        {
            var selection = hit.transform;
			Pickup pickedUpObject = hit.collider.gameObject.GetComponentInChildren<Pickup>();
            if(selection.CompareTag(selectableTag))
			{
				SetHighlightMaterial(selection);

				if (Input.GetButton("Fire1"))
				{
					if (currentObject == null)
					{
                        if(pickedUpObject.GetComponent<Pickup>().isFinal == true)
                        {
                            print("play sound");
							pickedUpObject.GetComponent<PlaySound>().UseSoundManager();
							currentObject.GetComponent<Pickup>().LoadNextScene();
						}
                        else
                        {
							currentObject = pickedUpObject;
							PickUp();
                        }
						pickedUpObject.GetComponent<PlaySound>().UseSoundManager();
					}
				}
			}
		}
        if (Physics.Raycast(ray, out hit, hitDistance, placeLayer))
        {
			var selection = hit.transform;
			if (selection.CompareTag(selectableTag))
			{
				SetHighlightMaterial(selection);

				if (Input.GetButton("Fire1"))
				{
					if (currentObject != null)
					{
						placeParent = selection.Find("Parent").gameObject;
						PutDown();
					}
				}
			}
        }
    }

	private void SetHighlightMaterial(Transform selection)
	{
		var selectionRenderer = selection.GetComponent<Renderer>();
		if (selectionRenderer != null)
		{
			selectionRenderer.material = highlightMaterial;
			selection.GetComponent<Outline>().enabled = true;
		}
		_selection = selection;
	}

	private void SetDefaultMaterial()
	{
		var selectionRenderer = _selection.GetComponent<Renderer>();
		selectionRenderer.material = defaultMaterial;
		_selection.GetComponent<Outline>().enabled = false;
		_selection = null;
	}

	private void PickUp()
    {
		currentObject.GetComponent<Collider>().enabled = false;
		currentObject.transform.parent = playerParent.transform;
		currentObject.transform.position = playerParent.transform.position;
        //currentObject.transform.localScale = new Vector3(1,1,1);
		print("picked up object");
    }

	private void PutDown()
	{
		currentObject.GetComponent<Pickup>().PlaceObject(currentObject, _selection);
		if(currentObject.GetComponent<Pickup>().canPlace == true)
		{
			//currentObject.GetComponent<Collider>().enabled = true;
			currentObject.transform.position = placeParent.transform.position;
			currentObject.transform.parent = placeParent.transform;
			currentObject.transform.localRotation = Quaternion.identity;
			currentObject.GetComponent<Pickup>().isPlaced = true;
			CheckIfDone();
			print("put down object");
			print("Is placed" + currentObject);
			currentObject = null;
		}
		else
		{
			print("cannot place here");
		}
	}

	private void CheckIfDone()
	{
		if(pickups[0].isPlaced == true && pickups[1].isPlaced == true && pickups[2].isPlaced == true && pickups[3].isPlaced == true)
		{
			finalObject.gameObject.SetActive(true);
		}
	}
}
