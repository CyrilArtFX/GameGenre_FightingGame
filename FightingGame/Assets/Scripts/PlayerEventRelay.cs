using UnityEngine;

public class PlayerEventRelay : MonoBehaviour
{
    [SerializeField]
    Player player;


    public void Punch()
    {
        player.Punch();
    }

    public void Stomp()
    {
        player.Stomp();
    }

    public void ResetAttacks()
    {
        player.ResetAttacks();
    }
}
