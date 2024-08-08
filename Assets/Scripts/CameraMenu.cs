using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour
{
    void Start()
    {
        UnlockCameraAndShowCursor();
    }

    void Update()
    {
        UnlockCameraAndShowCursor();
    }

    void UnlockCameraAndShowCursor()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 
    }
}