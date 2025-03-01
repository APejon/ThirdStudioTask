using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject _gameManagerObject;
    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameManager.coinCollected();
            this.gameObject.SetActive(false);
        }
    }
}
