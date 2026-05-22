using UnityEngine;

public class MaxHealth : MonoBehaviour
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
        collision.GetComponent<Health>().increasemaxHealth(1);
        anim.SetTrigger("increased");
    }
    private void DestroyFire()
    {
        gameObject.SetActive(false);
    }
}
