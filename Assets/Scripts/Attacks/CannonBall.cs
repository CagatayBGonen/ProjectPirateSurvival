using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage = 1;  // Varsayýlan hasar
    public bool isEnemyCannonball; // Bu mermi düþman tarafýndan mý atýldý?

    public float gravityFactor = 0.5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Yerçekimini azaltýlmýþ þekilde uygula
        Vector3 reducedGravity = Physics.gravity * gravityFactor;
        rb.AddForce(reducedGravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Çarpýlan objenin Health script'ini kontrol et
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            if (isEnemyCannonball)
            {
                // Düþman mermisi, oyuncuya hasar verir
                if (health.isPlayer)
                {
                    health.TakeDamage(damage); // Düþmanýn verdiði hasarý uygula
                    Debug.Log(health.currentHealth);
                }
            }
            else
            {
                // Oyuncu mermisi, düþmana hasar verir
                if (!health.isPlayer)
                {
                    health.TakeDamage(1); // Oyuncu mermisi her zaman 1 hasar verir
                    Debug.Log(health.currentHealth);
                }
            }
        }

        // Mermi çarptýktan sonra yok edilmesi
        Destroy(gameObject);
    }
}
