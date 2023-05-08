using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    //camera variables
    private Camera mainCam;
    private Vector3 mousePos;
    
    //Bullet variables
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float FireCooldown;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Aim Rotation using Camera
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        //Bullets
        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > FireCooldown)
            {
                canFire = true;
                timer = 0;
            }
        }
        if(Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            // Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
