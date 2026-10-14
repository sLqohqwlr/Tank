using UnityEngine;
using UnityEngine.InputSystem; 

public class TurretRotation : MonoBehaviour
{
    [Header("Turret Settings")]
    public float rotationSpeed = 300f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;   
    public Transform firePoint;        

    void Update()
    {
        RotateTurret();

        
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void RotateTurret()
    {
        if (Mouse.current == null || Camera.main == null) return;

   
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
       
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float currentAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
           
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
