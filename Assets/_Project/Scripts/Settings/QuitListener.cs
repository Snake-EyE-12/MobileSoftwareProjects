using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitListener : MonoBehaviour
{
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame) Quit();
    }
#endif


}

