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

        movement.Active = !Input.GetKey(KeyCode.X);
        if (!movement.Active && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
        {
            //movement.Active = true;
            StartCoroutine(GainControl());
            GameObject go = Instantiate(needlePrefab, transform);
            NeedleController nc = go.GetComponent<NeedleController>();

            nc.Direction = Vector3.up * Mathf.Abs(Input.GetAxis("Vertical")) +
                Vector3.right * Input.GetAxis("Horizontal");
            nc.IgnorePlatform = movement.OnPlatform;
            nc.Player = movement;
            timer = 1f;
        }
    }

    private IEnumerator GainControl()
    {
        yield return new WaitForSeconds(0.5f);

        movement.Active = true;
    }
}
