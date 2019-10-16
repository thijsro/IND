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
	[SerializeField] private float hitDistance = 10f;
    [SerializeField] private GameObject playerParent;
    public Pickup currentObject;


	Transform _selection;

    

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
			Pickup pickedUpObject = hit.collider.gameObject.GetComponent<Pickup>();
            if(selection.CompareTag(selectableTag))
			{
				SetHighlightMaterial(selection);

				if (Input.GetButton("Fire1"))
				{
					if (currentObject == null)
					{
						currentObject = pickedUpObject;
						PickUp();
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
		print("picked up object");
    }
}
