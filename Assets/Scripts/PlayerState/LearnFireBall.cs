using UnityEngine;

public class LearnFireBall : MonoBehaviour
{
    private PlayerState PS;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        PS = collision.GetComponent<PlayerState>();
        PS.Enable_fireBall();
        anim.SetTrigger("Learned");
    }
    private void DestroyFire()
    {
        gameObject.SetActive(false);
    }
}
