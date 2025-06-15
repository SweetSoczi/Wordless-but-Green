using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private bool isHurting = false;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;
    private bool canAttack = true;
    private bool isPreparingAttack = false;

    [Header("Animator")]
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead || !canAttack || isHurting || isPreparingAttack)
            return;

        if (Time.time >= nextAttackTime)
        {
            Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
            if (player != null)
            {
                StartCoroutine(DelayedAttack(player));
            }
        }
    }

    private IEnumerator DelayedAttack(Collider2D player)
    {
        isPreparingAttack = true;

        yield return new WaitForSeconds(0.5f); 

        if (!isDead && canAttack && !isHurting)
        {
            Collider2D stillInRange = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);
            if (stillInRange != null)
            {
                animator.SetTrigger("Attack");

                Player playerComponent = stillInRange.GetComponent<Player>();
                if (playerComponent != null)
                {
                    playerComponent.TakeDamage(attackDamage);
                }

                nextAttackTime = Time.time + attackCooldown;
            }
        }

        isPreparingAttack = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        canAttack = false;
        isHurting = true;

        Invoke(nameof(EndHurt), 0.8f); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void EndHurt()
    {
        canAttack = true;
        isHurting = false;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");

        Debug.Log(gameObject.name + " has died.");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
