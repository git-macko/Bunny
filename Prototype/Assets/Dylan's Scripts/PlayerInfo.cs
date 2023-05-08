using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float health = 3f;
    [System.NonSerialized] public float maxHealth = 3f;
    public float speed = 5f;
    public float strength = 1f;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        // Upon death
        if (health <= 0) {
            Debug.Log("You're dead!");
        }
    }

    public void SetSpeed(float speedAdjustment) {
        speed += speedAdjustment;
    }

    public void SetStrength(float strengthAdjustment) {
        strength += strengthAdjustment;
    }

    public void SetHealth(float healthAdjustment) {
        // Avoid overflowing health
        if (health + healthAdjustment > maxHealth) {
            return;
        }
        // Overkill just brings health to 0
        if (health + healthAdjustment < 0 && healthAdjustment > health) {
            health = 0;
            return;
        }
        
        health += healthAdjustment;
    }

    public void SetMaxHearts(float num) {
        maxHealth += num;
    }
}
