using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 20;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    public float offset;
    private GameObject player;
    private EdgeCollider2D edgeCollider;
    private float damage;
    Transform m_transform;
    private void Awake() {
        m_transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update() {
        
        GetComponent<LineRenderer>().enabled = false;
        // If not selected
        if (!GetComponent<PickUp>().selected) {  
            return;
        }

        // Item should not have any collision, only the line rendering
        GetComponent<Collider2D>().enabled = false;
        // Put in front of where player is moving
        float vertical = (float) (player.GetComponent<Transform>().position.y + Input.GetAxis("Vertical")/2);
        float horizontal = (float) (player.GetComponent<Transform>().position.x + Input.GetAxis("Horizontal")/2);

        // Rotating the weapon
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // direction = destination (cursor) - origin (weapon)
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        
        // No input, default is to right for now
        // if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) {
        //     horizontal += (float) 5;
        // }
        Vector2 newPos = new Vector2(horizontal, vertical);
        GetComponent<Transform>().position = newPos;
        SetEdgeCollider(m_lineRenderer);

        // Laser!
        if (Input.GetKey(KeyCode.Mouse0)) {
            // Enable both the laser line and edge collisions
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<EdgeCollider2D>().enabled = true;
            Draw2DRay(laserFirePoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            GetComponent<AudioSource>().Play();
        }
        // Not firing
        else if (Input.GetMouseButtonUp(0)) {
            // Disable visuals and collision
            GetComponent<LineRenderer>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<AudioSource>().Stop();
        }
    }

    // Laser visuals
    void Draw2DRay(Vector2 startPos, Vector2 endPos) {
        float dist = Mathf.Sqrt(Mathf.Pow(endPos.x - startPos.x, 2) + Mathf.Pow(endPos.y - startPos.y, 2));
        // Short laser is strong
        if (dist <= defDistanceRay) {
            GetComponent<LineRenderer>().startColor = Color.cyan;
            GetComponent<LineRenderer>().endColor = Color.green;
            
            GetComponent<LineRenderer>().startWidth = 0.25f;
            GetComponent<LineRenderer>().endWidth = 1.0f;

            damage = 40f * player.GetComponent<PlayerInfo>().strength;
        } else {    // Longer range laser is weak
            GetComponent<LineRenderer>().startColor = Color.yellow;
            GetComponent<LineRenderer>().endColor = Color.red;

            GetComponent<LineRenderer>().startWidth = 0.125f;
            GetComponent<LineRenderer>().endWidth = 0.375f;

            damage = 20f * player.GetComponent<PlayerInfo>().strength;
        }
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    // Setting the points for the edge collider
    // since using world space, convert coordinates to local space for points
    void SetEdgeCollider(LineRenderer lineRenderer) {
        List<Vector2> edges = new List<Vector2>();
        for (int point = 0; point < lineRenderer.positionCount; point++) {
            edges.Add(transform.InverseTransformPoint(lineRenderer.GetPosition(point)));
        }
        edgeCollider.SetPoints(edges);
    }
    
    // Deal damage overtime
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            // If not selected
            if (!GetComponent<PickUp>().selected) {  
                return;
            }
            // Access healthmanager which is in the child object of enemy
            other.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage * Time.smoothDeltaTime); // smoothDeltaTime to account for fps
        }
    }
}