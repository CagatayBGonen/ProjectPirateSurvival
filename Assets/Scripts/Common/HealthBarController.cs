using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider; // Slider bile�eni
    public Health health;       // �lgili geminin Health script'i
    public Vector3 offset = new Vector3(0, 3, 0); // Geminin �st�nde g�r�ns�n

    private void Start()
    {
        if (health == null)
        {
            health = GetComponentInParent<Health>();
        }
    }

    private void Update()
    {
        if (health != null)
        {
            // Slider de�erini Health'e g�re ayarla
            healthSlider.value = (float)health.currentHealth / health.maxHealth;

            // Sa�l�k bar�n� geminin �st�nde tut
            transform.position = health.transform.position + offset;
        }
    }
}
