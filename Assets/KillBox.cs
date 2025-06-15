using UnityEngine;

public class KillBox : MonoBehaviour
{
    private bool isRespawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning) return;

        if (other.CompareTag("Player"))
        {
            isRespawning = true;

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
            }

            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.Respawn();
            }

            StartCoroutine(ResetRespawnLock());
        }
    }

    private System.Collections.IEnumerator ResetRespawnLock()
    {
        yield return new WaitForSeconds(0.5f);
        isRespawning = false;
    }
}
