using UnityEngine;
using UnityEngine.UI;

public class ScoreGUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private HouseComparer houseComparer;

    private void Awake()
    {
        scoreText.text = $"Similarity: {0}%";
    }

    private void Update()
    {
        scoreText.text = $"Similarity: {houseComparer.score}%";
    }
}
