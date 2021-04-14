using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    private float shakeDuration = 0f;            //Wie lange der Shake dauert
    public float shakeAmount = 0.7f;             //Die Stärke des Shakes
    public float decreaseFactor = 1f;            //Wie schnell der Shake wieder aufhört
    public float shakeSaveDurationValue = 0.0f;
    public static CameraShake instance;

    Vector3 originalPos;

    private void Awake()
    {
        instance = this;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }
    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void StartShake()
    {
        shakeDuration = shakeSaveDurationValue;
        originalPos = camTransform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0;
            camTransform.localPosition = originalPos;
        }
    }
}
