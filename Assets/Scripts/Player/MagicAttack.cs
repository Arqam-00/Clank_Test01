using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float cost = 0.3f;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
    private Mana playermp;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playermp = GetComponent<Mana>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            MagicAttack_();
        if (playermp.current_mana < 10)
        {
            playermp.AddMana(Time.deltaTime);
        }
        cooldownTimer += Time.deltaTime;
    }

    private void MagicAttack_()
    {
        if (playermp.current_mana < cost) return;
        anim.SetTrigger("MagicAttack");
        cooldownTimer = 0;
        playermp.DecreaseMana(cost);
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}