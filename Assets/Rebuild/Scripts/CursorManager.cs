using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Texture2D normalCursor, grabCursor;

    public static void LoadCursors()
    {
        normalCursor = Resources.Load<Texture2D>("HandCursor");
        grabCursor = Resources.Load<Texture2D>("GrabCursor");
        SetNormalCursor();
    }

    public static void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor,new Vector2(16,4),CursorMode.Auto);
    }

    public static void SetGrabCursor()
    {
        Cursor.SetCursor(grabCursor,new Vector2(16,4),CursorMode.Auto);
    }

}
