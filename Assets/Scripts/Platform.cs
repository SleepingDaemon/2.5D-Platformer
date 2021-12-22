using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _pointA, _pointB;
    [SerializeField] private float _speed = 3f;
    private bool _canSwitch = false;

    private void FixedUpdate()
    {
        if (!_canSwitch)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        }
        else if(_canSwitch)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        }

        if (transform.position == _pointA.position)
        {
            _canSwitch = false;
        }

        if (transform.position == _pointB.position)
        {
            _canSwitch = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
