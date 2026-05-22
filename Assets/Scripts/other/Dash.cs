using UnityEngine;

public class Dash : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        collision.GetComponent<PlayerState>().Enable_dash();
        anim.SetTrigger("dash");
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    
}
