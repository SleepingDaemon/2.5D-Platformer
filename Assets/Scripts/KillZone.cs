using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;

            player.transform.position = respawnPoint.position;

            StartCoroutine(ControllerEnableRoutine(controller));
        }
    }

    IEnumerator ControllerEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);

        if (controller != null)
            controller.enabled = true;
    }
}
