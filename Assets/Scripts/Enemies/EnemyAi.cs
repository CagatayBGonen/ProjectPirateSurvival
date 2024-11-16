using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform playerTransform;                // Oyuncunun pozisyonunu almak için referans
    public float followDistance = 10f;     // Takip mesafesi (düþman gemisi bu mesafeye geldiðinde durur)
    public float shootDistance = 30f;       // Ateþ etme mesafesi (düþman gemisi bu mesafeye geldiðinde ateþ eder)
    public float moveSpeed = 5f;            // Düþman gemisinin hareket hýzý
    public float fireRate = 2f;             // Ateþ etme aralýðý (saniye)
    public GameObject cannonballPrefab;     // Ateþ edilecek mermi prefab’i
    public Transform cannonSpawnPoint;      // Top mermisinin spawn pozisyonu
    private Health health; // Düþmanýn Health bileþeni
    [SerializeField]
    private int damageAmount = 1;
    [SerializeField]
    private float cannonSpeed = 20f;
    private float fireCooldown = 0f;

    private void Start()
    {
        // Health bileþenini al
        health = GetComponent<Health>();
        // Oyuncuyu bul ve playerTransform'a ata
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (playerTransform != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player bulunamadý! Sahnenizde 'Player' tag'lý bir GameObject olduðundan emin olun.");
            }
        }
    }

    void Update()
    {
        // Oyuncunun pozisyonu ile mesafeyi kontrol et
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
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

        // Cannonball script'ine hasar deðerini aktar
        CannonBall cannonballScript = cannonball.GetComponent<CannonBall>();
        cannonballScript.isEnemyCannonball = true; // Bu mermiyi düþman fýrlattý

        // Seviyeye göre hasar deðeri belirle
        cannonballScript.damage = damageAmount;
        //if (health.maxHealth == 2)
        //    cannonballScript.damage = 2; // 2. seviye düþman
        //else if (health.maxHealth == 4)
        //    cannonballScript.damage = 4; // 3. seviye düþman
        //else
        //    cannonballScript.damage = 1; // 1. seviye düþman

        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.velocity = cannonSpawnPoint.forward * cannonSpeed; // Mermiyi ileri doðru fýrlatma hýzý
    }
}
