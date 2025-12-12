using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    private Camera arCamera;
    private bool isInDeleteMode = false;

    void Start()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        if (xrOrigin != null)
            arCamera = xrOrigin.GetComponentInChildren<Camera>();
        else
            arCamera = Camera.main;
    }

    void OnEnable()
    {
        EnterDeleteMode();
    }
    
    void OnDisable()
    {
        ExitDeleteMode();
    }

    void Update()
    {
        if (!isInDeleteMode) return;
        
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
                    Destroy(hitFurniture);
                    Debug.Log("Deleted furniture");
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
                        Destroy(hitFurniture);
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

    public void EnterDeleteMode()
    {
        isInDeleteMode = true;
        Debug.Log("Delete Mode Active - Click furniture to delete");
    }
    
    public void ExitDeleteMode()
    {
        isInDeleteMode = false;
    }
}