using UnityEngine;

public class PlayerEventRelay : MonoBehaviour
{
    [SerializeField]
    Player player;


    public void Punch()
    {
        player.Punch();
    }

    public void EndPunch()
    {
        player.EndPunch();
    }
}
