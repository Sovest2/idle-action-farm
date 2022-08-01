using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutButton : MonoBehaviour
{
    public static Action CutPressed;
    
    public void Cut()
    {
        CutPressed?.Invoke();
    }
}
