using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float  _movementSpeed;
    [SerializeField] float  _jumpForce;
    [SerializeField] GameObject _camera;

    private Rigidbody _rb;
    private float _moveX;
    private float _moveZ;
    private bool _jump;
    private bool _canJump;
    private Vector3 _targetVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveZ = Input.GetAxis("Vertical");
        if (_moveX != 0 || _moveZ != 0)
            transform.rotation = Quaternion.Euler(0f, _camera.transform.eulerAngles.y, 0f);
        if (!_jump)
            _jump = Input.GetKey(KeyCode.Space);
    }

    private void MovePlayer(float horizontal, float vertical, bool jump)
    {
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        _targetVelocity = moveDirection * _movementSpeed;
        _targetVelocity.y = _rb.linearVelocity.y;

        _rb.MovePosition(_rb.position + _targetVelocity * Time.fixedDeltaTime);

        if (jump && _canJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _jump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _canJump = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer(_moveX, _moveZ, _jump);
    }
}
