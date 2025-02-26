﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

/// <summary>
/// This class handles the soldier that shoots the balloons.
/// A laser is cast from the LaserStart object to where the mouse position on the screen is when the left mouse button is pressed.
/// Use a RayCast to check if the laser hits any balloons.
/// </summary>
public class Soldier : MonoBehaviour {

	[Tooltip("GameObject at the position where the laser starts")]
	public GameObject laserStart;
	[Tooltip("Crosshair GameObject")]
	public GameObject crosshair;
	[Tooltip("LineRenderer of the laser graphics")]
	public LineRenderer laserLineRenderer;
	[Tooltip("The layer of the balloons, for easy raycasting only agains balloons")]
	public LayerMask balloonLayerMask;

	void Update () {
		laserLineRenderer.enabled = false;
		// use GetMouseWorldPosition() and UpdateCrosshair() to make the chrosshair move with the mouse
		Vector3 mouseWorldPos = GetMouseWorldPosition();

		UpdateCrosshair(mouseWorldPos);

		// var origin = laserStart.transform.position;
		// var target = crosshair.transform.position;

		// var direction = target - origin;

		// var distancex = target.x - origin.x;
		// var distancey = target.y - origin.y;

		// var distanceOverall = Math.Sqrt((distancex * distancex) + (distancey*distancey));

		Vector3 dir3D = crosshair.transform.position - laserStart.transform.position;
		
		// transform.up = dir3D;
		//added this line to make player direction work, other math below didn't work for some reason
		
		Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);

		float length = dir2D.magnitude;

		dir2D.Normalize();

		Debug.Log($"Balloon Layer Mask value: {balloonLayerMask.value}");
		RaycastHit2D hit = Physics2D.Raycast(laserStart.transform.position, dir2D, length, balloonLayerMask);

		if (Input.GetMouseButton(0)) {
			laserLineRenderer.enabled = true;
			// Draw the raycast in the Scene view (red if no hit, green if hit)
			Debug.DrawRay(laserStart.transform.position, dir2D * length, Color.red, 1.0f);
			Debug.Log($"Raycast hit something: {hit.collider != null}");
			Debug.Log($"Direction: {dir2D}, Length: {length}");
			
			if (hit.collider != null && hit.collider.CompareTag("Balloon")) {
				Debug.Log("Hit a balloon!");
				hit.collider.GetComponent<Balloon>().Pop();
			}
		}

		Vector3 correctDirection = crosshair.transform.position - laserStart.transform.position;
        correctDirection.Normalize();
        
        float ydir = correctDirection.y;
        float xdir = correctDirection.x;

        float correctAngle = Mathf.Atan2(ydir, xdir) * Mathf.Rad2Deg; //finds angle in rads and converts to degrees

        correctAngle = correctAngle - 90;

        transform.rotation = Quaternion.AngleAxis(correctAngle,  Vector3.forward); //the axis we want is the world's global z-axis, this equals to Vector3.forward, or new Vector3(0,0,1)

        // Debug.Log($"LaserStart Z: {laserStart.transform.position.z}, Balloon Z: {FindObjectOfType<Balloon>()?.transform.position.z}");
	}

    /// <summary>
    /// Grabs the world position of the mouse with z = 0.
    /// </summary>
    /// <returns>World position of mouse as Vector3</returns>
    Vector3 GetMouseWorldPosition() {
        // this gets the current mouse position (in screen coordinates) and transforms it into world coordinates
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // the camera is on z = -10, so all screen coordinates are on z = -10. To be on the same plane as the game, we need to set z to 0
        mouseWorldPos.z = 0;

        return mouseWorldPos;
    }

	/// <summary>
	/// Updates the crosshair position and the line renderer from the laser to point from laserStart to the crosshair
	/// </summary>
	void UpdateCrosshair(Vector3 newCrosshairPosition){
		crosshair.transform.position = newCrosshairPosition;
		laserLineRenderer.SetPosition (0, laserStart.transform.position);
		laserLineRenderer.SetPosition (1, crosshair.transform.position);
	}
}
