using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * angle, Space.World);
    }
}
