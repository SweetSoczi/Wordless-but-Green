using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Obiekt do œledzenia (np. gracz)
    public Vector3 offset;       // Przesuniêcie kamery wzglêdem obiektu
    public float smoothSpeed = 0.125f; // P³ynnoœæ ruchu kamery

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
