using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool TargetSpotted { get; private set; }

    public Vector2 DirectionToTarget { get; private set; }

    [SerializeField]
    private float _targetSpottedDistance;

    private Transform _target;

    private void Awake()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 chaserToTargetVector = _target.position - transform.position;
        DirectionToTarget = chaserToTargetVector.normalized;

        if (chaserToTargetVector.magnitude <= _targetSpottedDistance)
        {
            TargetSpotted = true;
        }
        else
        {
            TargetSpotted = false;
        }
    }
}
