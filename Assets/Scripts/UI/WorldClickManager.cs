using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClickManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Clickable click = hit.collider.gameObject.GetComponent<Clickable>();
                if(click) click.Fire();
			}
		}
	}
}
