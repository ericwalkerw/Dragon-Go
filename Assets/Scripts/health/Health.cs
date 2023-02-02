using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;

    public float currentHealth { get; private set; }

    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFamesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private bool dead;
    private bool invulnerble;

    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerble) 
        {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {              
                foreach (Behaviour comp in components)
                {
                    comp.enabled = false;
                }
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(dieSound);
            }          
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour comp in components)
        {
            comp.enabled = true;
        }
    }

    private IEnumerator Invunerability()
    {
        invulnerble = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFamesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFamesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerble = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
