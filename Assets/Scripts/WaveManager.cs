using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int enemyLevel1Count; // 1. Seviye düþman sayýsý
        public int enemyLevel2Count; // 2. Seviye düþman sayýsý
        public int enemyLevel3Count; // 3. Seviye düþman sayýsý
    }

    public List<Wave> waves; // Tüm dalgalarýn bilgisi
    public float timeBetweenWaves = 5f; // Dalgalar arasýnda bekleme süresi
    public GameObject enemyLevel1Prefab;
    public GameObject enemyLevel2Prefab;
    public GameObject enemyLevel3Prefab;
    //public Transform[] spawnPoints; // Düþmanlarýn spawn olacaðý noktalar
    public float spawnInterval = 1f; // Düþmanlarýn spawn olma aralýðý (7. dalga için de kullanýlýr)

    private int currentWaveIndex = 0; // Hangi dalgada olduðumuzu takip eder
    private bool isFinalWave = false; // Son dalgada mýyýz?
    private int enemiesRemaining = 0; // Aktif düþman sayýsý

    private void Start()
    {
       StartNextWave();
    }

    private void Update()
    {
        // Son dalga deðilse ve düþman kalmadýysa bir sonraki dalgayý baþlat
        if (!isFinalWave && enemiesRemaining <= 0)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        Debug.Log($"Þu anki dalga: {currentWaveIndex}");
        if (currentWaveIndex < waves.Count)
        {
            Wave wave = waves[currentWaveIndex];

            Debug.Log($"Wave {currentWaveIndex + 1}: {wave.waveName} baþlýyor!");
            //yield return new WaitForSeconds(timeBetweenWaves);

            // Düþmanlarý spawn et
            StartCoroutine(SpawnEnemies(wave));
            
        }
        else
        {
            // Son dalga (7. dalga)
            isFinalWave = true;
            Debug.Log("Final wave baþladý!");
            StartCoroutine(SpawnEnemiesContinuously());
        }
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        // Düþmanlarý sýrayla spawn et
        enemiesRemaining = wave.enemyLevel1Count + wave.enemyLevel2Count + wave.enemyLevel3Count;
        Debug.Log("Kalan Düþman Sayýsý" + enemiesRemaining);

        // 1. Seviye düþmanlar
        for (int i = 0; i < wave.enemyLevel1Count; i++)
        {
            SpawnEnemy(enemyLevel1Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }

        // 2. Seviye düþmanlar
        for (int i = 0; i < wave.enemyLevel2Count; i++)
        {
            SpawnEnemy(enemyLevel2Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }

        // 3. Seviye düþmanlar
        for (int i = 0; i < wave.enemyLevel3Count; i++)
        {
            SpawnEnemy(enemyLevel3Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }
        currentWaveIndex++;
    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        while (isFinalWave)
        {
            // 7. Dalga için sürekli düþman spawn et
            SpawnEnemy(enemyLevel1Prefab);
            yield return new WaitForSeconds(spawnInterval);

            SpawnEnemy(enemyLevel2Prefab);
            yield return new WaitForSeconds(spawnInterval);

            SpawnEnemy(enemyLevel3Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Oyuncu pozisyonunu alýn
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPosition = player.transform.position;

        // Spawn pozisyonunu hesaplayýn
        Vector3 spawnPosition = GetSpawnPosition(playerPosition);

        // Düþmaný oluþtur
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    private Vector3 GetSpawnPosition(Vector3 playerPosition)
    {
        float spawnDistance = Random.Range(20f, 50f); // Oyuncudan uzaklýk (20 - 50 birim)
        float spawnAngle = Random.Range(0f, 360f); // Rastgele bir açý

        // Spawn pozisyonunu polar koordinatlara göre hesaplayýn
        Vector3 offset = new Vector3(
            Mathf.Cos(spawnAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(spawnAngle * Mathf.Deg2Rad)
        ) * spawnDistance;

        Vector3 spawnPosition = playerPosition + offset;

        // Yüzeyde kalmasý için y eksenini sabitle
        spawnPosition.y = 0;

        return spawnPosition;
    }

    public void OnEnemyKilled()
    {
        // Düþman öldüðünde kalan düþman sayýsýný azalt
        enemiesRemaining--;
    }
}
