using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseBehaviour : MonoBehaviour
{
    public Vector3 mousePosition;
    public Texture2D cursorTexture;
    public Camera mainCamera;


    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        mousePosition = GetMousePosition();

    }


    public Vector3 GetMousePosition()
    { // finds the loaction of the mouse on the screen
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return worldMousePosition;
    }



}
