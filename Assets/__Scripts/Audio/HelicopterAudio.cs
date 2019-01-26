using System;
using System.Linq.Expressions;
using FMOD;
using UnityEngine;

namespace Audio
{
    public class HelicopterAudio : MonoBehaviour
    {
        private Rigidbody2D rigidbody2D;
        private float previousFmodEventParameterValue = float.MinValue;
        [SerializeField] private float hysteresis;
        
        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var velocityMagnitude = rigidbody2D.velocity.magnitude;
            var fmodEventParameterValue = velocityToFMODEventParameterTransform(velocityMagnitude);
            
            if (shouldUpdateParameterValue(fmodEventParameterValue))
            {
                //TODO: Send parameter value to fmod event or something   
            }
        }

        private float velocityToFMODEventParameterTransform(float velocityMagnitude)
        {
            throw new NotImplementedException();
        }

        private bool shouldUpdateParameterValue(float newFmodEventParameterValue)
        {
            var minusHysteresis = previousFmodEventParameterValue - hysteresis;
            var plusHysteresis = previousFmodEventParameterValue + hysteresis;
            return newFmodEventParameterValue < minusHysteresis || newFmodEventParameterValue > plusHysteresis;
        }
    }
}