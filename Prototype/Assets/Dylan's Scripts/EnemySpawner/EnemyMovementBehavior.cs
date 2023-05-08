using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehavior : MonoBehaviour
{
    private GameObject MyTarget = null;
    public float MySpeed = 20f;
    public float turnRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        MyTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PointAtPosition(MyTarget.transform.position, turnRate * Time.smoothDeltaTime);
        transform.position += MySpeed * Time.smoothDeltaTime * transform.up;
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    private void OnTriggerEnter2D(Collider2D collider)	
	{
        if (collider.gameObject.name.Equals("Circle")) 
        {
            Destroy(gameObject);
        }
    }
}
