using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItem : MonoBehaviour
{
    private int index;
    private int distance;

    public void Initialize(int index, int distance)
    {
        this.index = index;
        this.distance = distance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Index: {index}");
        Debug.Log($"Distance: {distance}");
    }
}
