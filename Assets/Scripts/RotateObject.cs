using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    private GameObject objectToRotate;
    public Slider slider;
    private float previousValue;
    private float startValue = 0.5f;
    private bool isInRotateMode = false;
    private Camera arCamera;

    void Start()
    {
        GameObject xrOrigin = GameObject.Find("XR Origin");
        if (xrOrigin != null)
            arCamera = xrOrigin.GetComponentInChildren<Camera>();
        else
            arCamera = Camera.main;

        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderChanged);
            previousValue = slider.value;
        }
    }

    void OnEnable()
    {
        EnterRotateMode();
    }
    
    void OnDisable()
    {
        ExitRotateMode();
    }

    public void OnSliderChanged (float value)
    {
        if (objectToRotate == null || !isInRotateMode) return;
        
        float delta = value - previousValue;
        objectToRotate.transform.Rotate(Vector3.up * delta * 360);
        previousValue = value;
    }

    void Update()
    {
        if (!isInRotateMode) return;
        
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
        objectToRotate = furniture;
        Recolour rec = objectToRotate.GetComponent<Recolour>();
        if (rec != null) rec.SetSelected();
        Debug.Log("Selected for rotation: " + objectToRotate.name);
    }

    public void Deselect()
    {
        if (objectToRotate != null)
        {
            Recolour rec = objectToRotate.GetComponent<Recolour>();
            if (rec != null) rec.SetOriginalMaterial();
            objectToRotate = null;
            if (slider != null) slider.value = startValue;
        }
    }

    public void EnterRotateMode()
    {
        isInRotateMode = true;
        Debug.Log("Rotate Mode Active - Click furniture, then use slider");
    }
    
    public void ExitRotateMode()
    {
        isInRotateMode = false;
        Deselect();
        Debug.Log("Rotate Mode Exited");
    }
}