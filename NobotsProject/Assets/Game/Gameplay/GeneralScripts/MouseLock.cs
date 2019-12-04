using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    private bool locked;

    public void LockCursor()
    {
        if (!locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            locked = true;

        }
    }

    public void ShowCursor()
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            locked = false;
        }

    }
}
