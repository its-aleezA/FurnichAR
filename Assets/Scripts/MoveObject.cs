using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Camera arCamera;
    private GameObject objectToMove;
    private bool isInMoveMode = false;

    void Start()
    {
        if (arCamera == null)
        {
            GameObject xrOrigin = GameObject.Find("XR Origin");
            if (xrOrigin != null)
                arCamera = xrOrigin.GetComponentInChildren<Camera>();
            else
                arCamera = Camera.main;
        }
    }

    void OnEnable()
    {
        // Auto-enter move mode when this component is enabled
        EnterMoveMode();
    }
    
    void OnDisable()
    {
        // Auto-exit when disabled
        ExitMoveMode();
    }

    void Update()
    {
        if (!isInMoveMode) return;
        
        // MOUSE version
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                GameObject hitFurniture = FindFurnitureParent(hitObject.transform);
                if (hitFurniture != null)
                {
                    SelectObject(hitFurniture);
                }
            }
        }
        
        // TOUCH version
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    GameObject hitFurniture = FindFurnitureParent(hitObject.transform);
                    if (hitFurniture != null)
                    {
                        SelectObject(hitFurniture);
                    }
                }
            }
        }
    }

    private GameObject FindFurnitureParent(Transform hitTransform)
    {
        Transform current = hitTransform;
        for (int i = 0; i < 5; i++)
        {
            if (current == null) return null;
            if (current.GetComponent<Recolour>() != null || 
                current.CompareTag("Furniture"))
            {
                return current.gameObject;
            }
            current = current.parent;
        }
        return null;
    }

    private void SelectObject(GameObject furniture)
    {
        objectToMove = furniture;
        Recolour rec = objectToMove.GetComponent<Recolour>();
        if (rec != null) rec.SetSelected();
        objectToMove.transform.parent = arCamera.transform;
        Debug.Log("Selected for moving: " + objectToMove.name);
    }

    public void Deselect()
    {
        if (objectToMove != null)
        {
            Recolour rec = objectToMove.GetComponent<Recolour>();
            if (rec != null) rec.SetOriginalMaterial();
            objectToMove.transform.parent = null;
            objectToMove = null;
        }
    }

    public void EnterMoveMode()
    {
        isInMoveMode = true;
        Debug.Log("Move Mode Active - Click furniture to move");
    }
    
    public void ExitMoveMode()
    {
        isInMoveMode = false;
        Deselect();
        Debug.Log("Move Mode Exited");
    }
}