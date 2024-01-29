using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float cameraShakeTime = 1f;
    [SerializeField] float shakeMagnitued = 0.5f;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }
    public void Play()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake(){
        float elapsedTime = 0;
        while(elapsedTime < cameraShakeTime){
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitued;   
            elapsedTime += Time.deltaTime; 
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
        
    }
}
