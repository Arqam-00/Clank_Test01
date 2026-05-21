using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float healTime = 0.5f;
    private PlayerMovement PS;
    private bool healing = false;
    private float health_mp = 0f;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration = 1.5f;
    [SerializeField] private int numberOfFlashes = 10;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    //[Header("Death Sound")]
    //[SerializeField] private AudioClip deathSound;
    //[SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        PS = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (healing)
        {
            health_mp += Time.deltaTime;
            if (health_mp >= healTime)
            {
                health_mp = 0;
                anim.SetTrigger("Healed");
                heal(1);
                healing = false;
                PS.UnFreeze();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F) && PS.isGrounded())
        {
            anim.SetBool("Healing", true);
            healing = true;
            PS.Freeze();
        }
        if(Input.GetKeyUp(KeyCode.F)) {
        
            anim.SetBool("Healing", false);
            health_mp = 0;
            PS.UnFreeze();
            anim.SetTrigger("Healed");
        }
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            //SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                //SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    //Respawn
    public void Respawn()
    {
        AddHealth(maxHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());
        dead = false;

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private void heal(float hp = 1f)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += hp;
            Debug.Log("Healed");
        }
    }
    private void healfull()
    {
        currentHealth = maxHealth;
    }
    private void increasemaxHealth(float maxHealth_)
    {
        maxHealth = maxHealth_;
        currentHealth = maxHealth;
    }
}