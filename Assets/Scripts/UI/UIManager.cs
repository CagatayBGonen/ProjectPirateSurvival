using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text waveText; // Wave say�s�n� g�sterecek UI Text
    public Text killCountText; // �ld�r�len toplam d��man say�s�n� g�sterecek UI Text

    private int totalKills = 0; // Toplam �ld�r�len d��man say�s�
    private int currentWave = 0; // �u anki wave

    private void Start()
    {
        UpdateWaveText();
        UpdateKillCountText();
    }

    // Wave say�s�n� g�ncelle
    public void UpdateWaveText()
    {
        waveText.text = "Wave: " + currentWave.ToString();
    }

    // �ld�r�len d��man say�s�n� g�ncelle
    public void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + totalKills.ToString();
    }

    // �ld�r�len d��man say�s�n� artt�r
    public void IncrementKillCount()
    {
        totalKills++;
        UpdateKillCountText(); // UI'y� g�ncelle
    }

    // Wave say�s�n� artt�r
    public void IncrementWave()
    {
        currentWave++;
        UpdateWaveText(); // UI'y� g�ncelle
    }
}
