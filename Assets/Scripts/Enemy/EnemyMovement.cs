using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour
{
	public delegate void Stopped();
	public event Stopped OnStopped;

	public delegate void Started();
	public event Started OnStarted;

	public bool move = true;
	public float movementSpeed = 3f; // Units per second
	public float movementModifier = 1f;

	// If the value is an integer, the enemy is moving on the segment
	// If the value is a float of x.5, the enemy is moving between segments
	// floor(segmentIndex) is the previous segment, ceil(segmentIndex) is the nextSegment
	public float segmentIndex = 0;
	public Track track;

	public bool IsAtEndNode {
		get { return isAtTargetNode; }
		private set { isAtTargetNode = value; }
	}

	public bool IsStopped {
		get { return isStopped; }
		private set { isStopped = value; }
	}

	public float TrackProgress {
		get { return (track.LengthToSegment(segmentIndex) + distOnSegment) / track.Length; }
	}

	private bool isAtTargetNode = false;
	private bool isStopped = false;
	public float distOnSegment = 0f;
	
	void NextSegment() {
		segmentIndex += 0.5f;
		distOnSegment = 0;
		isAtTargetNode = false;
	}

	void MoveToNextNode() {
		var (startPos, targetPos) = track.GetSegmentBounds(segmentIndex);
		float segLength = track.GetSegmentLength(segmentIndex);

		float effectiveSpeed = movementSpeed * movementModifier;
		distOnSegment += effectiveSpeed * Time.deltaTime;
		float t = distOnSegment / segLength;

		transform.position = Vector3.Lerp(startPos, targetPos, t);

		if (t >= 1) {
			isAtTargetNode = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (move) {
			if (isAtTargetNode) {
				NextSegment();
			}

			if (Mathf.CeilToInt(segmentIndex) < track.GetSegmentCount()) {
				if (isStopped) {
					isStopped = false;
					OnStarted();
				}

				MoveToNextNode();
			} else if (!isStopped) {
				isStopped = true;
				OnStopped();
			}
		}
	}
}
