using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool TargetSpotted { get; private set; }

    public bool ScaryTargetSpotted { get; private set; }

    public Vector2 DirectionToTarget { get; private set; }
    public Vector2 DirectionToScaryTarget { get; private set; }

    [SerializeField]
    private float _targetSpottedDistance;

    private Transform _target;

    private Transform _chaser;
    private Transform _fleer;

    private void Awake()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _chaser = GameObject.FindWithTag("Chase").transform;
        _fleer = GameObject.FindWithTag("Flee").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (_chaser)
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
        if(_fleer)
        {
            Vector2 chaserToTargetVector = transform.position - _target.position;

            DirectionToScaryTarget = chaserToTargetVector.normalized;

            if (chaserToTargetVector.magnitude <= _targetSpottedDistance)
            {
                ScaryTargetSpotted = true;
            }
            else
            {
                ScaryTargetSpotted = false;
            }

        }
        
    }
}
