using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorNormal;
    public Texture2D cursorClick;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
        // Set the cursor to the normal one on start
        Cursor.SetCursor(cursorNormal, hotspot, cursorMode);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button held
        {
            Cursor.SetCursor(cursorClick, hotspot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(cursorNormal, hotspot, cursorMode);
        }
    }
}
