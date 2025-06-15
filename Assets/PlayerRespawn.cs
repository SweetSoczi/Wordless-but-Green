using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public ScreenFader screenFader;

    public void Respawn()
    {
        
        StartCoroutine(screenFader.FadeOutIn(() =>
        {
            transform.position = respawnPoint.position;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }));
    }
}
