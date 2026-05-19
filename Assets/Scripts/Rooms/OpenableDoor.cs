using TMPro;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    [SerializeField] private bool isopening = false;
    [SerializeField] private float openingspeed = 0.01f;
    private float org_val;

    private void Start()
    {
        org_val = transform.position.y;
    }

    private void Update()
    {
        if (isopening)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - openingspeed, transform.position.y), openingspeed);
        }
        if (org_val > transform.position.y + 20)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireBall")
        {
            if (!isopening)
            {
                isopening = true;
                return;
            }
            openingspeed += 0.02f;
        }
    }
}