using UnityEngine;
using UnityEngine.InputSystem; // Добавляем новую библиотеку

public class TankMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float rotationSpeed = 120f;

    private Rigidbody2D rb;
    private float moveInput;
    private float rotationInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.angularDamping = 5f;
        }
    }

    void Update()
    {
     
        if (Keyboard.current != null)
        {
        
            moveInput = 0f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveInput = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput = -1f;

            rotationInput = 0f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) rotationInput = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) rotationInput = -1f;
        }
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        rb.linearVelocity = transform.up * moveInput * moveSpeed;
        float rotation = rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation - rotation);
    }
}
