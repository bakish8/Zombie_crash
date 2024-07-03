using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeLifeBar : MonoBehaviour
{
    public Slider lifeBar;
    public float maxLife = 100f;
    public float currentLife;
    public float detectionRadius = 5f;
    public LayerMask zombieLayer;
    public float damagePerSecondPerZombie = 10f;

    private List<GameObject> zombiesInRange = new List<GameObject>();

    void Start()
    {
        currentLife = maxLife;
        lifeBar.maxValue = maxLife;
        lifeBar.value = currentLife;
    }

    void Update()
    {
        DetectZombies();
        UpdateLifeBar();
    }

    private void DetectZombies()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, zombieLayer);
        zombiesInRange.Clear();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Zombie"))
            {
                zombiesInRange.Add(hitCollider.gameObject);
            }
        }
    }

    private void UpdateLifeBar()
    {
        // Decrease life based on the number of zombies in range
        float damage = zombiesInRange.Count * damagePerSecondPerZombie * Time.deltaTime;
        currentLife -= damage;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        lifeBar.value = currentLife;

        if (currentLife <= 0)
        {
            Debug.Log("Home destroyed!");
            // Implement additional logic for when the home is destroyed
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
