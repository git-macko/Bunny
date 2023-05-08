using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyCamScript : MonoBehaviour
{
    //Camera Shake
    public float CameraShakeDuration = 0.1f;
    public AnimationCurve CurveShake;

    //Follow Bunny
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate() 
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);

        if(Input.GetMouseButton(0))
        {
            StartCoroutine(Shaking());
        }
    }


    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < CameraShakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float shakeStrength = CurveShake.Evaluate(elapsedTime / CameraShakeDuration);
            transform.position = startPos + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }
        transform.position = startPos;
    }
}
