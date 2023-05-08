using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool[] isFull;       // If slot in hotbar is taken up
    private GameObject[] slots;  // The hotbar array

    // Start is called before the first frame update
    void Start()
    {
        // Get the appropriate sizes and put the appropriate slot objects into the slots array
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Slot");
        slots = new GameObject[gos.Length];
        isFull = new bool[gos.Length];

        for (int i = 0; i < gos.Length; i++)
        {
            isFull[i] = false;  // False is default value
            slots[i] = gos[i];
            slots[i].GetComponent<SlotBehavior>().i = i;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject button, GameObject item) {
        for (int i = 0; i < slots.Length; i++)
        {
            // If slot is empty, add item
            if (!isFull[i]) {
                isFull[i] = true;                                                                   // Slot is full now
                GameObject b = Instantiate(button, slots[i].transform, false);                      // Hotbar UI image
                slots[i].GetComponent<SlotBehavior>().item = item;                                  // Set hotbar item
                
        
                break;
            }
        }
    }

    public GameObject[] getSlots() { return slots; }
    public bool[] getFull() { return isFull; }
}
