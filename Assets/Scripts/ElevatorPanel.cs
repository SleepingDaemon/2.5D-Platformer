using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [Tooltip("Enter a value required to activate the elevator panel")]
    [SerializeField] private int _requiredCoins;
    [SerializeField] private Color _activeLight;
    [SerializeField] private Color _inActiveLight;
    [SerializeField] private MeshRenderer _meshRend;
    private bool _canSwitch = false;

    private Elevator _elevator;
    private Player player;

    private void Awake()
    {
        if(_meshRend == null)
            _meshRend = GameObject.Find("Call_Button").GetComponent<MeshRenderer>();

        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        if (_elevator == null)
            print("Elevator is null");

        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
            print("Player is null");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canSwitch && player.GetCoins() >= _requiredCoins)
        {
            _meshRend.material.color = _activeLight;
            _elevator.CallElevator(true);
        }

        if (!_canSwitch)
        {
            _meshRend.material.color = _inActiveLight;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSwitch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canSwitch = false;
            _elevator.CallElevator(false);
        }
    }
}
