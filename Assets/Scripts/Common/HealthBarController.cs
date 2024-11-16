using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider; // Slider bileþeni
    public Health health;       // Ýlgili geminin Health script'i
    public Vector3 offset = new Vector3(0, 3, 0); // Geminin üstünde görünsün

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
            // Slider deðerini Health'e göre ayarla
            healthSlider.value = (float)health.currentHealth / health.maxHealth;

            // Saðlýk barýný geminin üstünde tut
            transform.position = health.transform.position + offset;
        }
    }
}
