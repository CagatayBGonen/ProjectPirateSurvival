using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset = new Vector3 (0f, 6.5f, -3.36f);
   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset; 
    }
}
