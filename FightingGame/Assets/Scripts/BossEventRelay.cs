using UnityEngine;

public class BossEventRelay : MonoBehaviour
{
    [SerializeField]
    Boss boss;


    public void Punch()
    {
        boss.Punch();
    }

    public void Stomp()
    {
        boss.Stomp();
    }

    public void ResetAttacks()
    {
        boss.ResetAttacks();
    }

    public void ActivateUlti()
    {
        boss.ActivateUlti();
    }

    public void Ulti()
    {
        boss.Ulti();
    }
}
