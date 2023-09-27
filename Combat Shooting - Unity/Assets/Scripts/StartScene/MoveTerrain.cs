using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTerrain : MonoBehaviour
{
    private float speed = 1f;

    void Update()
    {
        transform
            .Translate(new Vector3(0, -3, -4) *
            speed *
            Time.deltaTime);
    }
}
