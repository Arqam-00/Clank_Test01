using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask enemyLayer;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack_();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack_()
    {
        // Play attack animation
        if(playerMovement.isGrounded())
            anim.SetTrigger("Attack");
        else
            anim.SetTrigger("Jmp_Attack");
        cooldownTimer = 0;

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage each enemy
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit: " + enemy.name);
            // enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

}