﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use this script to rotate the balloons around the center
// Use a Quaternion to rotate the transform in the Update() function
// Adjust the rotation speed using rotationSpeed
// Make the rotation framerate indipendent using Time.deltaTime!
public class BalloonCenter : MonoBehaviour {

	[Tooltip("how fast the balloons are rotating")]
	public float rotationSpeed = 0;

	void Update(){
		float angle = rotationSpeed * Time.deltaTime;

		transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
