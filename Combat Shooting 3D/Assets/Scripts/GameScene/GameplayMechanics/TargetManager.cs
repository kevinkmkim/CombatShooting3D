using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public event TargetHitDelegate OnAnyTargetsHit;

    public delegate void TargetHitDelegate(int targetDistance, int targetNum);

    [SerializeField]
    private Target[] targetEvents;

    private void OnEnable()
    {
        foreach (Target targetEvent in targetEvents)
        {
            targetEvent.OnTargetHit += HandleTargetHit;
        }
    }
    
    private void OnDisable()
    {
        foreach (Target targetEvent in targetEvents)
        {
            targetEvent.OnTargetHit -= HandleTargetHit;
        }
    }

    private void HandleTargetHit(int targetDistance, int targetNum)
    {
        Debug.Log("HandleTargetHit TARGET MANAGER");
        OnAnyTargetsHit?.Invoke(targetDistance, targetNum);
    }
}
