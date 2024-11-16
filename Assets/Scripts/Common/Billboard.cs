using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        // Can bar�n� kameraya d�nd�r
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Yanl�� d�nerse ayarlay�n
    }
}

