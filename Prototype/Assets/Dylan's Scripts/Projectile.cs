using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float distance;
    public LayerMask whatIsSolid;
    private PlayerInfo player;

    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        damage = 10f;
        Invoke("DestroyProjectile", 4);
    }

    // Update is called once per frame
    void Update()
    {
        // Collision
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) {
                Debug.Log("enemy hit!");
                hitInfo.collider.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage * player.strength);   // * player strength stat
            }
            DestroyProjectile();
        }
        
        // Keep moving projectile forward direction
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
