using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDroppedItem(GameObject item) {
        // Drop mechanic

        // Drop on top of player 
        Vector2 playerPos = new Vector2(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 10);
        item.GetComponent<Transform>().position = playerPos;
        // Collectible again
        item.GetComponent<Collider2D>().enabled = true;
        // Adjust visuals
        item.GetComponent<PickUp>().Invoke();
        Color c = item.GetComponent<SpriteRenderer>().color;
        c.a = 1;
        item.GetComponent<SpriteRenderer>().color = c;
    }
}
