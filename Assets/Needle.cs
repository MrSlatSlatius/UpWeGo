using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Needle : MonoBehaviour
{
    private PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            movement.Active = false;
        else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            movement.Active = true;
    }
}
