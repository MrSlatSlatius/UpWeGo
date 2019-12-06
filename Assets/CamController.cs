using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private ProceduralMap map = null;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if (movement != null)
            map.Movements.Add(movement);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if (movement != null)
            map.Movements.Remove(movement);
    }
}
