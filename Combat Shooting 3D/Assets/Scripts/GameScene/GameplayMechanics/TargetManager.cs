using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public event TargetHitDelegate OnTargetHit;
    public delegate void TargetHitDelegate(int targetDistance, int targetNum);

    [SerializeField]
    private Target[] targets;

    private void OnEnable()
    {
        foreach (Target target in targets)
        {
            target.OnTargetHit += HandleTargetHit;
        }
    }

    private void OnDisable()
    {
        foreach (Target target in targets)
        {
            target.OnTargetHit -= HandleTargetHit;
        }
    }

    private void HandleTargetHit(int targetDistance, int targetNum)
    {
        Debug.Log("HandleTargetHit TARGET MANAGER");
        OnTargetHit?.Invoke(targetDistance, targetNum);
    }
}
