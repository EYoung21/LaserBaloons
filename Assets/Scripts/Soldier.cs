using System.Collections;
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
		Vector2 dir2D = new Vector2(dir3D.x, dir3D.y);

		float length = dir2D.magnitude;

		dir2D.Normalize();

		RaycastHit2D hit = Physics2D.Raycast(laserStart.transform.position, dir2D, length, balloonLayerMask);

		if (hit.collider.CompareTag("Balloon")) {
			hit.collider.GetComponent<Balloon>().Pop();
		}
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
