using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public Transform mapTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public float RotationSpeed = 3.0f;
    void Start()
    {
        _cameraOffset = transform.position - mapTransform.position;
    }

    void LateUpdate()
    {
    }
}
