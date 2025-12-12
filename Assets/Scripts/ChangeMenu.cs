using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    public GameObject catalogueControls;
    public GameObject moveControls;
    public GameObject rotateControls;
    public GameObject currentlyDisplayed;
    public GameObject deleteControls;
    public bool isSectionDisplayedToggle = true;
    
    private ARTapToPlaceObject arPlacer;
    private MoveObject moveScript;
    private RotateObject rotateScript;
    private RemoveObject deleteScript;
    
    void Start()
    {
        arPlacer = FindFirstObjectByType<ARTapToPlaceObject>();
        moveScript = FindFirstObjectByType<MoveObject>();
        rotateScript = FindFirstObjectByType<RotateObject>();
        deleteScript = FindFirstObjectByType<RemoveObject>();
        
        // Initialize with catalogue
        if (catalogueControls != null)
        {
            catalogueControls.SetActive(true);
            currentlyDisplayed = catalogueControls;
        }
        
        // Exit all modes on start
        ExitAllModes();
    }
    
    // MAIN BUTTON FUNCTIONS (for ButtonChooseFurniture/Move/Rotate/Delete)
    public void SetCatalogue()
    {
        ToggleMenu(catalogueControls);
        ExitAllModes();
    }
    
    public void SetMove()
    {
        ToggleMenu(moveControls);
        ExitAllModes();
        EnterMoveMode(); // Enter move mode when this UI opens
    }
    
    public void SetRotate()
    {
        ToggleMenu(rotateControls);
        ExitAllModes();
        EnterRotateMode(); // Enter rotate mode
    }
    
    public void SetDelete()
    {
        ToggleMenu(deleteControls);
        ExitAllModes();
        EnterDeleteMode(); // Enter delete mode
    }
    
    // MODE SWITCHING FUNCTIONS (for buttons INSIDE the panels)
    public void EnterMoveMode()
    {
        if (moveScript != null)
        {
            moveScript.EnterMoveMode();
            Debug.Log("Move Mode Activated");
        }
    }
    
    public void ExitMoveMode()
    {
        if (moveScript != null) moveScript.ExitMoveMode();
    }
    
    public void EnterRotateMode()
    {
        if (rotateScript != null)
        {
            rotateScript.EnterRotateMode();
            Debug.Log("Rotate Mode Activated");
        }
    }
    
    public void ExitRotateMode()
    {
        if (rotateScript != null) rotateScript.ExitRotateMode();
    }
    
    public void EnterDeleteMode()
    {
        if (deleteScript != null)
        {
            deleteScript.EnterDeleteMode();
            Debug.Log("Delete Mode Activated - Click furniture to delete");
        }
    }
    
    public void ExitDeleteMode()
    {
        if (deleteScript != null) deleteScript.ExitDeleteMode();
    }
    
    private void ExitAllModes()
    {
        ExitMoveMode();
        ExitRotateMode();
        ExitDeleteMode();
    }
    
    // In ToggleMenu method, add:
    private void ToggleMenu(GameObject menu)
    {
        if (menu == null) return;
        
        // Switch away from catalogue - destroy ghost
        if (currentlyDisplayed == catalogueControls && arPlacer != null)
        {
            arPlacer.DestroyGhost();
        }
        
        // DISABLE current mode script
        DisableCurrentMode();
        
        if (currentlyDisplayed != menu)
        {
            if (currentlyDisplayed != null)
                currentlyDisplayed.SetActive(false);
                
            menu.SetActive(true);
            currentlyDisplayed = menu;
            isSectionDisplayedToggle = true;
            
            // ENABLE new mode script based on which UI opened
            EnableModeForMenu(menu);
        }
        else
        {
            currentlyDisplayed.SetActive(!isSectionDisplayedToggle);
            isSectionDisplayedToggle = !isSectionDisplayedToggle;
            
            if (!currentlyDisplayed.activeSelf)
            {
                DisableCurrentMode();
            }
            else
            {
                EnableModeForMenu(menu);
            }
        }
    }

    private void EnableModeForMenu(GameObject menu)
    {
        MoveObject moveScript = FindFirstObjectByType<MoveObject>();
        RotateObject rotateScript = FindFirstObjectByType<RotateObject>();
        RemoveObject deleteScript = FindFirstObjectByType<RemoveObject>();
        
        if (menu == moveControls && moveScript != null)
            moveScript.enabled = true;
        else if (menu == rotateControls && rotateScript != null)
            rotateScript.enabled = true;
        else if (menu == deleteControls && deleteScript != null)
            deleteScript.enabled = true;
    }

    private void DisableCurrentMode()
    {
        MoveObject moveScript = FindFirstObjectByType<MoveObject>();
        RotateObject rotateScript = FindFirstObjectByType<RotateObject>();
        RemoveObject deleteScript = FindFirstObjectByType<RemoveObject>();
        
        if (moveScript != null) moveScript.enabled = false;
        if (rotateScript != null) rotateScript.enabled = false;
        if (deleteScript != null) deleteScript.enabled = false;
    }
}