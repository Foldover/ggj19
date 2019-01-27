using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
	public class GameTimer : MonoBehaviour
	{
		private float startTime;
		[SerializeField] private int maxTime;
		[SerializeField] private Text text;
		private Color initialColor;
		private HouseComparer houseComparer;

		public GameObject mainTimerCanvas;
		public GameObject endUiPrefab;
		public GameObject endStarSpawners;

		private bool hasEnded = false;

		private void Awake()
		{
			ResetTimer();
			initialColor = text.color;
		}

		private void Start()
		{
			houseComparer = FindObjectOfType<HouseComparer>();
		}

		private void Update()
		{
			var now = Time.time;
			var timeLeft = maxTime - (now - startTime);
			if (timeLeft < 0 || houseComparer.score == 100)
			{
				OnGameEnd();
			}
			else
			{
				text.text = string.Format("{0}:{1}", Minutes(timeLeft), Seconds(timeLeft));
			}
			text.color = Color.Lerp(initialColor, Color.red, (1 - timeLeft / maxTime));
		}

		private void ResetTimer()
		{
			startTime = Time.time;
		}

		private string Minutes(float time)
		{
			var minutes = Mathf.FloorToInt(time / 60.0f);
			if (minutes < 10)
			{
				return "0" + minutes.ToString();
			}
			else
			{
				return minutes.ToString();
			}
		}

		private string Seconds(float time)
		{
			var seconds = (int)(time % 60.0f);
			if (seconds < 10)
			{
				return "0" + seconds.ToString();
			}
			else
			{
				return seconds.ToString();
			}
		}

		private void OnGameEnd()
		{
			if (hasEnded)
			{
				return;
			}
			hasEnded = true;

			AudioManager.Instance.PlayOneShot("event:/endReaction");
			Instantiate(endUiPrefab, mainTimerCanvas.transform);
			Instantiate(endStarSpawners);
			Invoke("ReloadScene", 3f);
		}

		private void ReloadScene()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}