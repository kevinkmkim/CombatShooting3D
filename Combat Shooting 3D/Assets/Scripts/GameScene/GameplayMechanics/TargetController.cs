using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform spawnTransform;

    public void Initialize(Stage stage)
    {
        Instantiate(targetPrefab);
        foreach (Target target in stage.targets)
        {
            GameObject newTargetChild = Instantiate(targetPrefab, transform);
            newTargetChild.transform.localPosition = target.position;

            TargetItem newTargetChildScript = newTargetChild.GetComponent<TargetItem>();
            newTargetChildScript.Initialize(target.index, target.distance);
        }
    }

}
