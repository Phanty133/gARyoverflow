using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Track : MonoBehaviour
{
	public GameObject[] segmentObjs;

	public float Length {
		get { return length; }
		private set { length = value; }
	}

	public Vector3 StartPosition {
		get { return segments[0].startNode.transform.position; }
	}

	List<TrackSegment> segments;

	List<float> interSegmentDistances; // Index 0 - Distance between seg0 and seg1, Index 1 - distance between seg1 and seg2, etc.

	float length = 0;

	public Tuple<Vector3, Vector3> GetSegmentBounds(float segIndex) {
		if (segIndex % 1 == 0) {
			return new Tuple<Vector3, Vector3>(
				segments[(int)segIndex].getStartPos(),
				segments[(int)segIndex].getEndPos()
			);
		} else {
			return new Tuple<Vector3, Vector3>(
				segments[Mathf.FloorToInt(segIndex)].getEndPos(),
				segments[Mathf.CeilToInt(segIndex)].getStartPos()
			);
		}
	}

	public float GetSegmentLength(float segIndex) {
		if (segIndex % 1 == 0) {
			return segments[(int)segIndex].Length;
		} else {
			return interSegmentDistances[Mathf.FloorToInt(segIndex)];
		}
	}

	public int GetSegmentCount() {
		return segments.Count;
	}

	// Non-inclusive
	public float LengthToSegment(float segIndex) {
		float len = 0;

		for (float i = 0; i < segIndex; i += 0.5f) {
			len += GetSegmentLength(i);
		}
		
		return len;
	}

	// Start is called before the first frame update
	void Start()
	{
		segments = new List<TrackSegment>();

		foreach (GameObject obj in segmentObjs) {
			TrackSegment seg = obj.GetComponent<TrackSegment>();
			length += seg.Length;
			segments.Add(seg);
		}

		interSegmentDistances = new List<float>();

		for (int i = 0; i < segments.Count - 1; i++) {
			Vector3 node1 = segments[i].endNode.transform.position;
			Vector3 node2 = segments[i + 1].startNode.transform.position;
			float dist = Vector3.Distance(node1, node2);

			length += dist;
			interSegmentDistances.Add(dist);
		}
	}
}
