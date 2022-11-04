using UnityEngine;

public class EmployeEventRelay : MonoBehaviour
{
    [SerializeField]
    Employe employe;


    public void Punch()
    {
        employe.Punch();
    }

    public void Stomp()
    {
        employe.Stomp();
    }

    public void ResetAttacks()
    {
        employe.ResetAttacks();
    }

    public void ResetAttacksBeginning()
    {
        employe.ResetAttacksBeginning();
    }

    public void ActivateUlti()
    {
        employe.ActivateUlti();
    }

    public void Ulti()
    {
        employe.Ulti();
    }
}
