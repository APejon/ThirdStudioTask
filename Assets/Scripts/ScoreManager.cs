using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    private int _score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
}
