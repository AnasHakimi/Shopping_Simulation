using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        float angle = rotationSpeed * Time.deltaTime; 

        transform.Rotate(Vector3.up * angle, Space.World);
    }
}
