using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        // Can barýný kameraya döndür
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Yanlýþ dönerse ayarlayýn
    }
}

