using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public event TargetHitDelegate OnAnyTargetsHit;

    public delegate void TargetHitDelegate(int targetDistance, int targetNum);

    [SerializeField]
    private Target[] someTargetEvents;

    void Start()
    {
        OnEnable();
    }

    void Update()
    {
    }

    private void OnEnable()
    {
        foreach (Target someTargetEvent in someTargetEvents)
        {
            someTargetEvent.OnTargetHit += HandleTargetHit;
        }
    }

    private void OnDisable()
    {
        foreach (Target someTargetEvent in someTargetEvents)
        {
            someTargetEvent.OnTargetHit -= HandleTargetHit;
        }
    }

    private void HandleTargetHit(int targetDistance, int targetNum)
    {
        Debug.Log("HandleTargetHit TARGET MANAGER");
        OnAnyTargetsHit?.Invoke(targetDistance, targetNum);
    }
}
