using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform playerTransform;                // Oyuncunun pozisyonunu almak i�in referans
    public float followDistance = 10f;     // Takip mesafesi (d��man gemisi bu mesafeye geldi�inde durur)
    public float shootDistance = 30f;       // Ate� etme mesafesi (d��man gemisi bu mesafeye geldi�inde ate� eder)
    public float moveSpeed = 5f;            // D��man gemisinin hareket h�z�
    public float fireRate = 2f;             // Ate� etme aral��� (saniye)
    public GameObject cannonballPrefab;     // Ate� edilecek mermi prefab�i
    public Transform cannonSpawnPoint;      // Top mermisinin spawn pozisyonu
    private Health health; // D��man�n Health bile�eni
    [SerializeField]
    private int damageAmount = 1;
    [SerializeField]
    private float cannonSpeed = 20f;
    private float fireCooldown = 0f;

    private void Start()
    {
        // Health bile�enini al
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
                Debug.LogError("Player bulunamad�! Sahnenizde 'Player' tag'l� bir GameObject oldu�undan emin olun.");
            }
        }
    }

    void Update()
    {
        // Oyuncunun pozisyonu ile mesafeyi kontrol et
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        // E�er oyuncu takip mesafesindeyse onu takip et
        if (distanceToPlayer > shootDistance)
        {
            FollowPlayer(direction);
            RotateToPlayer(direction);
        }
        // E�er oyuncu ate� etme mesafesindeyse ate� et
        if (distanceToPlayer <= shootDistance)
        {
            TryShoot();
            RotateToPlayer(direction);
        }
    }

    void FollowPlayer(Vector3 direction)
    {
        
        // Oyuncuya do�ru hareket et
        
        transform.position += direction * moveSpeed * Time.deltaTime;

        
    }
    void RotateToPlayer(Vector3 direction)
    {
        // Oyuncuya do�ru y�nel
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
    }

    void TryShoot()
    {
        // Ate� etme cooldown��n� kontrol et
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;  // Cooldown s�f�rlan�r
        }
    }

    void Shoot()
    {
        // Top mermisini olu�tur ve ileri do�ru ate�le
        GameObject cannonball = Instantiate(cannonballPrefab, cannonSpawnPoint.position, cannonSpawnPoint.rotation);

        // Cannonball script'ine hasar de�erini aktar
        CannonBall cannonballScript = cannonball.GetComponent<CannonBall>();
        cannonballScript.isEnemyCannonball = true; // Bu mermiyi d��man f�rlatt�

        // Seviyeye g�re hasar de�eri belirle
        cannonballScript.damage = damageAmount;
        //if (health.maxHealth == 2)
        //    cannonballScript.damage = 2; // 2. seviye d��man
        //else if (health.maxHealth == 4)
        //    cannonballScript.damage = 4; // 3. seviye d��man
        //else
        //    cannonballScript.damage = 1; // 1. seviye d��man

        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        rb.velocity = cannonSpawnPoint.forward * cannonSpeed; // Mermiyi ileri do�ru f�rlatma h�z�
    }
}
