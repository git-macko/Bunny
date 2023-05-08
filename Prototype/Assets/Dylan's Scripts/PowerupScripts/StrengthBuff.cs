using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuff : MonoBehaviour
{
    [SerializeField] private float strengthIncreaseAmt = 1;
    [SerializeField] private float heartsIncreaseAmt = 3;
    [SerializeField] private float duration = 8;

    private void Update() 
    {

    }
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerInfo pi = other.gameObject.GetComponent<PlayerInfo>();
        if (pi) {
            StartCoroutine(PowerupSequence(pi));
        }
    }

    private IEnumerator PowerupSequence(PlayerInfo pi) {
        // Soft destroy
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        // Activate
        pi.SetStrength(strengthIncreaseAmt);                                                       // Strength
        Vector3 iScale = pi.gameObject.transform.localScale;                                       // Size
        pi.gameObject.transform.localScale = new Vector3(iScale.x*1.5f, iScale.y*1.5f, iScale.z*1.5f);      
        Vector4 iColor = pi.gameObject.GetComponent<SpriteRenderer>().color;                       // Color
        pi.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        float iMax = pi.gameObject.GetComponent<PlayerInfo>().maxHealth;                           // Hearts
        pi.gameObject.GetComponent<PlayerInfo>().SetMaxHearts(heartsIncreaseAmt);
        pi.gameObject.GetComponent<PlayerInfo>().SetHealth(3);


        // Delay 
        yield return new WaitForSeconds(duration);
    
        // Deactivate
        pi.SetStrength(-strengthIncreaseAmt);
        pi.gameObject.transform.localScale = iScale;
        pi.gameObject.GetComponent<SpriteRenderer>().color = iColor;
        pi.gameObject.GetComponent<PlayerInfo>().SetMaxHearts(-heartsIncreaseAmt);
        if (pi.gameObject.GetComponent<PlayerInfo>().health > pi.gameObject.GetComponent<PlayerInfo>().maxHealth) {
            pi.gameObject.GetComponent<PlayerInfo>().health = 3;                                                        // Reset to 3 if still have more hearts than initial
        }
        Destroy(gameObject);
    }
}
