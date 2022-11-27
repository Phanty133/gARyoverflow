using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
	public bool showNodes = false;

	public GameObject startNode;
	public GameObject endNode;
	public float Length {
		get { return length; }
		private set { length = value; } 
	}

	MeshRenderer startNodeRenderer;
	MeshRenderer endNodeRenderer;
	float length;

	public Vector3 getStartPos() {
		return startNode.transform.position;
	}

	public Vector3 getEndPos() {
		return endNode.transform.position;
	}

	// Start is called before the first frame update
	void Start()
	{
		startNodeRenderer = startNode.GetComponent<MeshRenderer>();
		endNodeRenderer = endNode.GetComponent<MeshRenderer>();

		length = Vector3.Distance(startNode.transform.position, endNode.transform.position);
	}

	// Update is called once per frame
	void Update()
	{
		if (startNodeRenderer.enabled != showNodes || endNodeRenderer.enabled != showNodes) {
			startNodeRenderer.enabled = showNodes;
			endNodeRenderer.enabled = showNodes;
		}
	}
}
