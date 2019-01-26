using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class DropEffect : MonoBehaviour {
    public float ShakeDurationSeconds = 0.1f;
    public float ShakeAmount = 0.1f;
    public float DecreaseFactor = 1.0f;

    private Transform _camTransform;
    private float _shakeTimeLeft;
    private Collider2D _collider;
    
    Vector3 _originalPos;
    
    void Awake() 
    {
        if (_camTransform == null) {
            _camTransform = Camera.main.GetComponent<Transform>();
        }
    }
    
    void Start()
    {
        _originalPos = _camTransform.localPosition;
    }

    // when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col) {
        //TODO doubleck if really floor
        //if(col...)
        _shakeTimeLeft = ShakeDurationSeconds;
    }

    void Update()
    {
        if (_shakeTimeLeft > 0)
        {
            _camTransform.localPosition = _originalPos + Random.insideUnitSphere * ShakeAmount;
            _shakeTimeLeft -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            _shakeTimeLeft = 0f;
            _camTransform.localPosition = _originalPos;
            //Destroy(this);
        }
    }
}
