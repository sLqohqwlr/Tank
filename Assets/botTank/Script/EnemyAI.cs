using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Tank Node References")]
    public Transform turret;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // ����� ���� ��� ������� �������������� ������ � ����������!
    [Header("Target Settings")]
    public Transform playerTarget;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 90f;

    [Header("Combat Behavior Settings")]
    public float attackRange = 7f;
    public float stopRange = 4f;
    public float fireRate = 1.5f;

    private Rigidbody2D rb;
    private float nextFireTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���� ������ ���������� ������ �������, ������ ���������� ����� ��� �� �����
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.Find("Tank");
            if (playerObj != null)
            {
                playerTarget = playerObj.transform;
            }
            else
            {
                Debug.LogError("����������� ������: ������ � ������ 'Tank' �� ������ �� �����!");
            }
        }
    }

    void Update()
    {
        if (playerTarget == null) return;

        RotateTurretTowardsPlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
        if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        if (playerTarget == null || rb == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        Vector2 direction = (Vector2)playerTarget.position - rb.position;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float smoothedAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(smoothedAngle);

        if (distanceToPlayer > stopRange)
        {
            rb.linearVelocity = transform.up * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void RotateTurretTowardsPlayer()
    {
        if (turret == null) return;

        Vector2 direction = (Vector2)playerTarget.position - (Vector2)turret.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        float angle = Mathf.MoveTowardsAngle(turret.eulerAngles.z, targetAngle, 200f * Time.deltaTime);
        turret.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
