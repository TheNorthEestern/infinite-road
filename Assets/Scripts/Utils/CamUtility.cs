using UnityEngine;
using System.Collections;

public class CamUtility {
	// Function to determine whether a renderer is within frustum of
	// a specified camera
	// Returns true if renderer is within frustum, else flase
	public static bool IsRendererInFrustum(Renderer Renderable, Camera Cam) {
		// Construct frustum planes from camera
		// Each plane represents one wall of frustum
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Cam);

		// Test whether renderable is within frustum planes
		return GeometryUtility.TestPlanesAABB(planes, Renderable.bounds);
	}
}
