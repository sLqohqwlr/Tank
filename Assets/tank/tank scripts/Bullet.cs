using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 20;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Пуля врезалась в объект: " + hitInfo.gameObject.name);

        // Ищем скрипт Health на самом объекте ИЛИ на его главном родителе (например, Tank)
        Health targetHealth = hitInfo.GetComponentInParent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Скрипт Health не найден ни на объекте, ни на его родителях!");
        }

        Destroy(gameObject);
    }
}