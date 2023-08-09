using UnityEngine;

[CreateAssetMenu(menuName = "Items/Target")]
public class Target : ScriptableObject
{
    public int index;
    public int distance;
    public Vector3 position;
}
