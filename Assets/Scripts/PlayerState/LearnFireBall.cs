using UnityEngine;

public class LearnFireBall : MonoBehaviour
{
    private PlayerState PS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        PS = collision.GetComponent<PlayerState>();
        PS.Enable_fireBall();
    }
}
