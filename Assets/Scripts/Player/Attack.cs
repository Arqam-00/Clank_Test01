using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float AttackCooldown = 0.5f;
    [SerializeField] private GameObject Hitbox;
    [SerializeField] private float AttackDuration = 1.25f;

    [SerializeField] private AudioClip AttackSound;
    private Animator Anim;
    private PlayerMovement PlayerMovement;
    private float CooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();

        Hitbox.SetActive(false);
    }

    private void Update()
    {
        if (PlayerMovement.Freeze_)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) &&
            CooldownTimer > AttackCooldown &&
            PlayerMovement.canAttack())
        {
            SoundManager.instance.PlaySound(AttackSound);
            StartCoroutine(Attack_());
        }

        CooldownTimer += Time.deltaTime;
    }

    private IEnumerator Attack_()
    {
        if (PlayerMovement.isGrounded())
            Anim.SetTrigger("Attack");
        else
            Anim.SetTrigger("Jmp_Attack");
        CooldownTimer = 0;
        Hitbox.SetActive(true);
        yield return new WaitForSeconds(AttackDuration);
        Hitbox.SetActive(false);
    }
    public void Disable_Hitbox()
    {
        Hitbox.SetActive(false);
    }
}