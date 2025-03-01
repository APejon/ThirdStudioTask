using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float  _movementSpeed;
    [SerializeField] float  _jumpForce;
    [SerializeField] float _dashPower;
    [SerializeField] float _dashDuration;
    [SerializeField] GameObject _camera;

    private Rigidbody _rb;
    private float _originalMovementSpeed;
    private float _moveX;
    private float _moveZ;
    private bool _jump;
    private bool _airJumps;
    private bool _canJump;
    private bool _dash;
    private Vector3 _targetVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _originalMovementSpeed = _movementSpeed;
        _canJump = true;
        _airJumps = true;
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveZ = Input.GetAxis("Vertical");
        if (_moveX != 0 || _moveZ != 0)
            transform.rotation = Quaternion.Euler(0f, _camera.transform.eulerAngles.y, 0f);
        _jump = Input.GetKeyDown(KeyCode.Space);
        if (_jump && (_canJump || _airJumps))
        {
            if (!_canJump)
                _airJumps = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        _dash = Input.GetKeyDown(KeyCode.LeftShift);
        if (_dash)
            StartCoroutine(dashCoroutine());
    }

    IEnumerator dashCoroutine()
    {
        _movementSpeed = 0;
        _rb.useGravity = false;
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = (forward * _moveZ + right * _moveX).normalized;

        _rb.linearVelocity = moveDirection * _dashPower;
        yield return new WaitForSeconds(_dashDuration);
        _movementSpeed = _originalMovementSpeed;
        _rb.useGravity = true;
        _rb.linearVelocity = new Vector3(0f, 0f, 0f);
    }

    private void MovePlayer(float _moveX, float _moveZ, bool jump)
    {
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDirection = (forward * _moveZ + right * _moveX).normalized;

        _targetVelocity = moveDirection * _movementSpeed;
        _targetVelocity.y = _rb.linearVelocity.y;

        _rb.MovePosition(_rb.position + _targetVelocity * Time.fixedDeltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _canJump = true;
            _airJumps = true;
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
