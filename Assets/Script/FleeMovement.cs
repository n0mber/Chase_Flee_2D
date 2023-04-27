using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeeD;

    private Rigidbody2D _rb;
    private PlayerAwareness _playerAwareness;
    private Vector2 _targetDirection;
    private Vector2 _startDirection;
    private Vector3 _startPosition;
    private bool _delay = true;
    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwareness = GetComponent<PlayerAwareness>();
        _startPosition = transform.position;
        _circleCollider = GetComponent<CircleCollider2D>();

    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.25f, 9.23f), Mathf.Clamp(transform.position.y, -4.45f, 4.5f), transform.position.z);       
    }

    private void FixedUpdate()
    {
        if (!_playerAwareness.ScaryTargetSpotted && transform.position != _startPosition)
        {
            if (_delay)
            {
                _rb.velocity = Vector2.zero;
                _targetDirection = Vector3.zero;
                float _delayTime = 0.5f;
                DoDelayAction(_delayTime);
            }
            else
            {
                ReturnToStart();
            }
        }
        else
        {
            _delay = true;
            UpdateTargetDirection();
        }
        RotateTowardsTarget();
        SetVelocity();

        if (_targetDirection == _startDirection.normalized && _startPosition != transform.position && !_delay)
        {
            _rb.MovePosition(_startDirection);
        }
    }

    private void ReturnToStart()
    {
        if (!_playerAwareness.ScaryTargetSpotted && transform.position != _startPosition)
        {
            float step = _speed * Time.deltaTime;
            _startDirection = Vector2.MoveTowards(transform.position, _startPosition, step);
            _targetDirection = _startDirection.normalized;
        }
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwareness.ScaryTargetSpotted)
        {
            _targetDirection = _playerAwareness.DirectionToScaryTarget;
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

        Quaternion awayRotation = Quaternion.LookRotation(-transform.position, _targetDirection);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, awayRotation, _rotationSpeeD * Time.deltaTime);

        _rb.SetRotation(rotation);

    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = transform.up * _speed;
        }
    }

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayReturn(delayTime));
    }
    IEnumerator DelayReturn(float delayTime)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(delayTime);

        Debug.Log("Ended Coroutine at timestamp : " + Time.time);
        _delay = false;
    }
}
