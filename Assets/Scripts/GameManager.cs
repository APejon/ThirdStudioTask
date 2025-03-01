using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    private ScoreManager _scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scoreManager = _canvas.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void coinCollected()
    {
        _scoreManager.incrementScore();
    }
}
