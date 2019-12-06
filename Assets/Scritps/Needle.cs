using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Needle : MonoBehaviour
{
    private PlayerMovement movement;
    [SerializeField] private GameObject needlePrefab = null;
    private float timer = 0f;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        timer = Mathf.Max(timer - Time.deltaTime, 0f);
        if (timer > 0)
            return;

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            return;

        if (movement.Active && Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            movement.Active = false;
        else if (!movement.Active && Input.GetKeyDown(KeyCode.X))
        {
            //movement.Active = true;
            StartCoroutine(GainControl());
            GameObject go = Instantiate(needlePrefab, transform);
            NeedleController nc = go.GetComponent<NeedleController>();

            nc.Direction = Vector3.up * Input.GetAxis("Vertical") +
                Vector3.right * Input.GetAxis("Horizontal");
            nc.IgnorePlatform = movement.OnPlatform;
            timer = 1f;
        }
    }

    private IEnumerator GainControl()
    {
        yield return new WaitForSeconds(0.5f);

        movement.Active = true;
    }
}
