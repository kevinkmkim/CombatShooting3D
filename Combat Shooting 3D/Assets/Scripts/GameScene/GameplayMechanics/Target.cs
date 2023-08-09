using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    public int targetDistance;

    [SerializeField]
    private int targetNum;

    private Quaternion downRotation = Quaternion.AngleAxis(100, Vector3.left);

    private Quaternion upRotation;

    public event TargetHitDelegate OnTargetHit;

    public delegate void
        TargetHitDelegate(int targetDistance, int targetNum);

    void Start()
    {
        upRotation = transform.rotation;
        // shotDetected = false;
    }

    private void Up()
    {
        transform.rotation =
            Quaternion.Slerp(transform.rotation, upRotation, 0.05f);
    }

    private void Down()
    {
        transform.rotation =
            Quaternion.Slerp(transform.rotation, downRotation, 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Information");
        Debug.Log (targetDistance);
        Debug.Log (targetNum);
        if (targetDistance == GameManager.activeDistance)
        {
            // OnTargetHitInvoke (targetNum, targetDistance);
            OnTargetHit?.Invoke(targetDistance, targetNum);

            // GameManager.IncrementScore();
            GameManager.targetsHit.Add (targetNum);
            // shotDetected = true;
        }
    }
}
