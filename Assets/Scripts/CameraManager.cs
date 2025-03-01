using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject _playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
