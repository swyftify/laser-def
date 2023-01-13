using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    float shakeDuration = 0.5f;

    [SerializeField]
    float shakeMagnitude = 0.2f;

    Vector3 initialCameraPosition;

    void Start()
    {
        initialCameraPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while (shakeDuration > 0)
        {
            transform.position =
                initialCameraPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            yield return new WaitForEndOfFrame();
            shakeDuration -= Time.deltaTime;
        }
        shakeDuration = 1f;
        transform.position = initialCameraPosition;
    }
}
