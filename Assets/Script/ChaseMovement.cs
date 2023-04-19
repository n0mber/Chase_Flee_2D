using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeeD;

    private Rigidbody2D _rb;
    private PlayerAwareness _playerAwareness;
    private Vector2 _targetDirection;
    private Vector2 _startPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwareness = GetComponent<PlayerAwareness>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.25f, 9.23f), Mathf.Clamp(transform.position.y, -4.45f, 4.5f), transform.position.z);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if(_playerAwareness.TargetSpotted)
        {
            _targetDirection = _playerAwareness.DirectionToTarget;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeeD * Time.deltaTime);

        _rb.SetRotation(rotation);

    }

    private void SetVelocity()
    {
        if(_targetDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = transform.up * _speed;
        }
    }
}
