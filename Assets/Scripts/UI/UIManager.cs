using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text waveText; // Wave sayýsýný gösterecek UI Text
    public Text killCountText; // Öldürülen toplam düþman sayýsýný gösterecek UI Text

    private int totalKills = 0; // Toplam öldürülen düþman sayýsý
    private int currentWave = 0; // Þu anki wave

    private void Start()
    {
        UpdateWaveText();
        UpdateKillCountText();
    }

    // Wave sayýsýný güncelle
    public void UpdateWaveText()
    {
        waveText.text = "Wave: " + currentWave.ToString();
    }

    // Öldürülen düþman sayýsýný güncelle
    public void UpdateKillCountText()
    {
        killCountText.text = "Kills: " + totalKills.ToString();
    }

    // Öldürülen düþman sayýsýný arttýr
    public void IncrementKillCount()
    {
        totalKills++;
        UpdateKillCountText(); // UI'yý güncelle
    }

    // Wave sayýsýný arttýr
    public void IncrementWave()
    {
        currentWave++;
        UpdateWaveText(); // UI'yý güncelle
    }
}
