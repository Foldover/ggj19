using UnityEngine;

public class DropEffect : MonoBehaviour
{
	public float ShakeDurationSeconds = 0.1f;
	public float ShakeAmount = 0.1f;
	public float DecreaseFactor = 1.0f;

	private Transform _camTransform;
	private float _shakeTimeLeft;
	private Collider2D _collider;

	private Vector3 _originalPos;

	private void Awake()
	{
		if (_camTransform == null)
		{
			_camTransform = Camera.main.GetComponent<Transform>();
		}
	}

	private void Start()
	{
		_originalPos = _camTransform.localPosition;
	}

	// when the cube hits the floor
	private void OnCollisionEnter2D(Collision2D col)
	{
		//TODO doubleck if really floor
		//if(col...)
		_shakeTimeLeft = ShakeDurationSeconds;

		if (col.gameObject.GetComponent<DropEffect>())
		{
			AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.blockCollision, transform.position, _Fmod.Params.variation, 0f);
		}
		else
		{
			AudioManager.Instance.PlayOneShot3D(_Fmod.Events.Misc.blockCollision, transform.position, _Fmod.Params.variation, 1f);
		}
	}

	private void Update()
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