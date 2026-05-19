using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
