using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField] private Color _activationColor;
    private MeshRenderer _rend;

    private void Awake()
    {
        _rend = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Moving Box"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Distance: " + distance);

            if(distance < 0.1f)
            {
                Rigidbody _rb = other.GetComponent<Rigidbody>();

                if(_rb != null)
                    _rb.isKinematic = true;

                if(_rend != null)
                    _rend.material.color = _activationColor;
            }
        }
    }
}
