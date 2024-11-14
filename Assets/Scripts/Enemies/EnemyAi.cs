using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform player;                // Oyuncunun pozisyonunu almak için referans
    public float followDistance = 10f;     // Takip mesafesi (düþman gemisi bu mesafeye geldiðinde durur)
    public float shootDistance = 30f;       // Ateþ etme mesafesi (düþman gemisi bu mesafeye geldiðinde ateþ eder)
    public float moveSpeed = 5f;            // Düþman gemisinin hareket hýzý
    public float fireRate = 2f;             // Ateþ etme aralýðý (saniye)
    public GameObject cannonballPrefab;     // Ateþ edilecek mermi prefab’i
    public Transform cannonSpawnPoint;      // Top mermisinin spawn pozisyonu

    private float fireCooldown = 0f;

    void Update()
    {
        // Oyuncunun pozisyonu ile mesafeyi kontrol et
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 direction = (player.position - transform.position).normalized;
        // Eðer oyuncu takip mesafesindeyse onu takip et
        if (distanceToPlayer > shootDistance)
        {
            FollowPlayer(direction);
            RotateToPlayer(direction);
        }
        // Eðer oyuncu ateþ etme mesafesindeyse ateþ et
        if (distanceToPlayer <= shootDistance)
        {
            TryShoot();
            RotateToPlayer(direction);
        }
    }

    void FollowPlayer(Vector3 direction)
    {
        
        // Oyuncuya doðru hareket et
        
        transform.position += direction * moveSpeed * Time.deltaTime;

        
    }
    void RotateToPlayer(Vector3 direction)
    {
        // Oyuncuya doðru yönel
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
    }

    void TryShoot()
    {
        // Ateþ etme cooldown’ýný kontrol et
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;  // Cooldown sýfýrlanýr
        }
    }

    void Shoot()
    {
        // Top mermisini oluþtur ve ileri doðru ateþle
        GameObject cannonball = Instantiate(cannonballPrefab, cannonSpawnPoint.position, cannonSpawnPoint.rotation);
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.velocity = cannonSpawnPoint.forward * 20f; // Mermiyi ileri doðru fýrlatma hýzý
    }
}
