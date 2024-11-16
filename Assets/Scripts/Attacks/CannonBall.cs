using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;  // Varsay�lan hasar
    public bool isEnemyCannonball; // Bu mermi d��man taraf�ndan m� at�ld�?

    public float gravityFactor = 0.5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Yer�ekimini azalt�lm�� �ekilde uygula
        Vector3 reducedGravity = Physics.gravity * gravityFactor;
        rb.AddForce(reducedGravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �arp�lan objenin Health script'ini kontrol et
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            if (isEnemyCannonball)
            {
                // D��man mermisi, oyuncuya hasar verir
                if (health.isPlayer)
                {
                    health.TakeDamage(damage); // D��man�n verdi�i hasar� uygula
                    Debug.Log(health.currentHealth);
                }
            }
            else
            {
                // Oyuncu mermisi, d��mana hasar verir
                if (!health.isPlayer)
                {
                    health.TakeDamage(1); // Oyuncu mermisi her zaman 1 hasar verir
                    Debug.Log(health.currentHealth);
                }
            }
        }

        // Mermi �arpt�ktan sonra yok edilmesi
        Destroy(gameObject);
    }
}
