using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;
    public HealthBar healthBar;
    private Rigidbody2D rb;

    public GameObject deathScreen;
    public Animator animator;
    public float deathAnimationDuration = 4f;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");

        Debug.Log("Player died");

        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        GetComponent<PlayerMovement>().enabled = false;

        StartCoroutine(ShowDeathScreenAfterDelay());
    }

    private IEnumerator ShowDeathScreenAfterDelay()
    {
        yield return new WaitForSeconds(deathAnimationDuration);

        deathScreen.SetActive(true);
        Time.timeScale = 0f;  
    }
}
