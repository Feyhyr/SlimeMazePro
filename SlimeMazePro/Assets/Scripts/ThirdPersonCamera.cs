using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    public float SmoothFactor = 1f;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = target.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }
}
