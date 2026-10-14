using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Кому следовать")]
    public Transform target; 

    [Header("Настройки плавности")]
    public float smoothSpeed = 5f;

    [Header("Смещение")]
    public Vector3 offset = new Vector3(0f, 0f, -10f); 

    void LateUpdate()
    {
        if (target == null) return;

    
        Vector3 targetPosition = target.position + offset;

   
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);


        transform.position = smoothedPosition;
    }
}
