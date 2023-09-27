using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStairs : MonoBehaviour
{
    private float speed = 2.3f;

    void Update()
    {
        transform
            .Translate(new Vector3(0f, -1.259f, 1.78f) *
            speed *
            Time.deltaTime);
    }
}
