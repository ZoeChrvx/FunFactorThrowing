using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public GameObject explosion;

    public void TakeDamage(int damage)
    {
        health = health - damage;

        if(health <= 0)
        {
            explosion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
