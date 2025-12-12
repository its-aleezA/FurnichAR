using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject ghost;
    public GameObject objectToPlace;
    
    public GameObject chair;
    public GameObject table;
    public GameObject pouf;
    public GameObject shelf;
    public GameObject sofa;

    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    private Camera arCamera;

    void Start()
    {
        aRRaycastManager = FindFirstObjectByType<ARRaycastManager>();
        arCamera = GetARCamera(); // Custom method to find AR camera
    }

    void Update()
    {
        if (arCamera == null) 
        {
            arCamera = GetARCamera(); // Try to find camera each frame
            return;
        }
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    // Method to find AR camera WITHOUT XROrigin
    private Camera GetARCamera()
    {
        // Method 1: Look for camera in XR Origin
        GameObject xrOrigin = GameObject.Find("XR Origin");
        if (xrOrigin != null)
        {
            Camera cam = xrOrigin.GetComponentInChildren<Camera>();
            if (cam != null) return cam;
        }
        
        // Method 2: Look for Main Camera with AR tag
        Camera mainCam = Camera.main;
        if (mainCam != null && (mainCam.name.Contains("AR") || mainCam.name.Contains("XR")))
            return mainCam;
        
        // Method 3: Any camera in scene
        Camera[] allCams = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        foreach (Camera cam in allCams)
        {
            if (cam.isActiveAndEnabled)
                return cam;
        }
        
        return null;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementIndicator == null) return;
        
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
    
    private void UpdatePlacementPose()
    {
        if (arCamera == null || aRRaycastManager == null) return;
        
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
        }
    }

    public void PlaceObject()
    {
        if (placementPoseIsValid && ghost != null && objectToPlace != null)
        {
            Recolour recolour = ghost.GetComponent<Recolour>();
            if (recolour != null)
                recolour.SetOriginalMaterial();
            
            ghost.transform.parent = null;
            ghost = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
            
            Recolour newRecolour = ghost.GetComponent<Recolour>();
            if (newRecolour != null)
                newRecolour.SetValid();
                
            ghost.transform.parent = placementIndicator.transform;
        }
    }
    
    private void UseObject(GameObject o)
    {
        objectToPlace = o;
        if (ghost != null)
            Destroy(ghost);
            
        // Only instantiate if we have a valid position
        if (placementPoseIsValid)
        {
            ghost = Instantiate(o, PlacementPose.position, PlacementPose.rotation);
            
            Recolour recolour = ghost.GetComponent<Recolour>();
            if (recolour != null)
                recolour.SetValid();
                
            ghost.transform.parent = placementIndicator.transform;
        }
        else
        {
            Debug.Log("Move camera to find a surface first!");
        }
    }
    
    public void DestroyGhost()
    {
        if (ghost != null)
        {
            Destroy(ghost);
            ghost = null;
        }
    }
    
    public void UseChair() { UseObject(chair); }
    public void UseTable() { UseObject(table); }
    public void UsePouf() { UseObject(pouf); }
    public void UseShelf() { UseObject(shelf); }
    public void UseSofa() { UseObject(sofa); }
}