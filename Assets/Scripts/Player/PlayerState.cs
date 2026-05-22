using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // This script records player actions(Players abilities and effect on world)
    public bool can_cast { get; private set; }
    public bool can_dash { get; private set; }

    public Respawn RespawnPoint;
    void Start()
    {
        can_cast = false;
        can_dash = false;
    }
    public void Set_respawn(Respawn r)
    {
        RespawnPoint = r;
    }
    public void player_respawn()
    {
        RespawnPoint.respawn_();
    }
    public void Enable_fireBall()
    {
        can_cast = true;
    }
    public void Enable_dash()
    {
        can_dash = true;
    }
}
