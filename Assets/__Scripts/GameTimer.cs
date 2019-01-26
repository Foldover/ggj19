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

        private void Awake()
        {
            resetTimer();
        }

        private void Update()
        {
            var now = Time.time;
            var timeLeft = maxTime - (now - startTime);
            text.text = string.Format("{0}:{1}", minutes(timeLeft), seconds(timeLeft));
            if (timeLeft < 0)
            {
                reloadScene();
            }
        }
        
        private void resetTimer()
        {
            startTime = Time.time;
        }

        private string minutes(float time)
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

        private string seconds(float time)
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

        private void reloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}