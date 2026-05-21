using TMPro;
using UnityEngine;

public class OpenableDoor : MonoBehaviour
{
    private bool isopening = false;
    private bool isclosing = false;
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - openingspeed, transform.position.z), openingspeed);
        }
        if (org_val > transform.position.y + 20)
        {
            isopening = false;
            isclosing = true;
            openingspeed = 0.01f;
        }
        if (isclosing)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, org_val, transform.position.z), 0.075f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireBall")
        {
            isclosing = false;
            if (!isopening)
            {
                isopening = true;
                return;
            }
            openingspeed += 0.02f;
        }
    }
}