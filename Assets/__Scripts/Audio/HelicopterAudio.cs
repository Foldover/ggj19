using UnityEngine;

namespace Audio
{
	public class HelicopterAudio : MonoBehaviour
	{
		private Rigidbody2D rigidbod2D;
		private float previousFmodEventParameterValue = float.MinValue;
		[SerializeField] private float hysteresis;

		private FMODUnity.StudioEventEmitter helicopterSound;

		private void Awake()
		{
			rigidbod2D = GetComponent<Rigidbody2D>();
		}

		private void OnEnable()
		{
			if (!helicopterSound)
			{
				helicopterSound = AudioManager.Instance.InitializeAudioOnObject(gameObject, "event:/helicopter");
			}

			AudioManager.Instance.PlayEmitterOnce(helicopterSound);
		}

		private void OnDisable()
		{
			helicopterSound?.Stop();
		}

		private void Update()
		{
			var velocityMagnitude = rigidbod2D.velocity.magnitude;
			var fmodEventParameterValue = VelocityToFMODEventParameterTransform(velocityMagnitude);

			Debug.Log(fmodEventParameterValue);

			if (ShouldUpdateParameterValue(fmodEventParameterValue))
			{
				helicopterSound.SetParameter("velocity", fmodEventParameterValue); //TODO: get velocity from the rigidbody pls.
			}
		}

		private float VelocityToFMODEventParameterTransform(float velocityMagnitude)
		{
			return velocityMagnitude;
		}

		private bool ShouldUpdateParameterValue(float newFmodEventParameterValue)
		{
			var minusHysteresis = previousFmodEventParameterValue - hysteresis;
			var plusHysteresis = previousFmodEventParameterValue + hysteresis;
			return newFmodEventParameterValue < minusHysteresis || newFmodEventParameterValue > plusHysteresis;
		}
	}
}