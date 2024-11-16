using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int enemyLevel1Count; // 1. Seviye d��man say�s�
        public int enemyLevel2Count; // 2. Seviye d��man say�s�
        public int enemyLevel3Count; // 3. Seviye d��man say�s�
    }

    public List<Wave> waves; // T�m dalgalar�n bilgisi
    public float timeBetweenWaves = 5f; // Dalgalar aras�nda bekleme s�resi
    public GameObject enemyLevel1Prefab;
    public GameObject enemyLevel2Prefab;
    public GameObject enemyLevel3Prefab;
    //public Transform[] spawnPoints; // D��manlar�n spawn olaca�� noktalar
    public float spawnInterval = 1f; // D��manlar�n spawn olma aral��� (7. dalga i�in de kullan�l�r)

    private int currentWaveIndex = 0; // Hangi dalgada oldu�umuzu takip eder
    private bool isFinalWave = false; // Son dalgada m�y�z?
    private int enemiesRemaining = 0; // Aktif d��man say�s�

    private void Start()
    {
       StartNextWave();
    }

    private void Update()
    {
        // Son dalga de�ilse ve d��man kalmad�ysa bir sonraki dalgay� ba�lat
        if (!isFinalWave && enemiesRemaining <= 0)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        Debug.Log($"�u anki dalga: {currentWaveIndex}");
        if (currentWaveIndex < waves.Count)
        {
            Wave wave = waves[currentWaveIndex];

            Debug.Log($"Wave {currentWaveIndex + 1}: {wave.waveName} ba�l�yor!");
            //yield return new WaitForSeconds(timeBetweenWaves);

            // D��manlar� spawn et
            StartCoroutine(SpawnEnemies(wave));
            
        }
        else
        {
            // Son dalga (7. dalga)
            isFinalWave = true;
            Debug.Log("Final wave ba�lad�!");
            StartCoroutine(SpawnEnemiesContinuously());
        }
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        // D��manlar� s�rayla spawn et
        enemiesRemaining = wave.enemyLevel1Count + wave.enemyLevel2Count + wave.enemyLevel3Count;
        Debug.Log("Kalan D��man Say�s�" + enemiesRemaining);

        // 1. Seviye d��manlar
        for (int i = 0; i < wave.enemyLevel1Count; i++)
        {
            SpawnEnemy(enemyLevel1Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }

        // 2. Seviye d��manlar
        for (int i = 0; i < wave.enemyLevel2Count; i++)
        {
            SpawnEnemy(enemyLevel2Prefab);
            yield return new WaitForSeconds(spawnInterval);
        }

        // 3. Seviye d��manlar
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
            // 7. Dalga i�in s�rekli d��man spawn et
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
        // Oyuncu pozisyonunu al�n
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPosition = player.transform.position;

        // Spawn pozisyonunu hesaplay�n
        Vector3 spawnPosition = GetSpawnPosition(playerPosition);

        // D��man� olu�tur
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    private Vector3 GetSpawnPosition(Vector3 playerPosition)
    {
        float spawnDistance = Random.Range(20f, 50f); // Oyuncudan uzakl�k (20 - 50 birim)
        float spawnAngle = Random.Range(0f, 360f); // Rastgele bir a��

        // Spawn pozisyonunu polar koordinatlara g�re hesaplay�n
        Vector3 offset = new Vector3(
            Mathf.Cos(spawnAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(spawnAngle * Mathf.Deg2Rad)
        ) * spawnDistance;

        Vector3 spawnPosition = playerPosition + offset;

        // Y�zeyde kalmas� i�in y eksenini sabitle
        spawnPosition.y = 0;

        return spawnPosition;
    }

    public void OnEnemyKilled()
    {
        // D��man �ld���nde kalan d��man say�s�n� azalt
        enemiesRemaining--;
    }
}
