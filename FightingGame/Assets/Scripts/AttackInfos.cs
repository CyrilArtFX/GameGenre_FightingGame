using UnityEngine;

public class AttackInfos : MonoBehaviour
{
    public float damages;
    public float maxStunTime;
    [Tooltip("The percentage by which the stun time is decreased each hit in a combo")] public float stunTimeDiminution;
    public Vector3 firstHitProjection;
    public Vector3 hitProjectionAccumulation;
    public bool bypassProjectionDiminution;
}
