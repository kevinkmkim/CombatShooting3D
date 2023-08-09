using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region Serialized Field
    // [SerializeField] private Target[] targets;
    [SerializeField] private GameObject targetPrefab;
    #endregion

    #region Properties
    private Transform[] targetPositions;
    #endregion

    private void OnEnable()
    {
        foreach (Transform targetPosition in targetPositions)
        {
            GameObject targetObject = Instantiate(targetPrefab, targetPosition);
            TargetController targetController = targetObject.GetComponent<TargetController>();

            targetController.Initialize();
        }
    }

    private void OnDisable()
    {
    }
}
