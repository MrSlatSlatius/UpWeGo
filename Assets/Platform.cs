using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool isFixed = false;

    public Vector3 Velocity { get; set; } = Vector3.zero;
    public bool Fixed => isFixed;

    public void Move(Vector3 motion)
    {
        Velocity = motion;
    }

    public void FixedUpdate()
    {
        transform.position += Velocity * Time.fixedDeltaTime;
        Velocity = Vector3.Lerp(Velocity, Vector3.zero, Time.fixedDeltaTime * 3f);
    }
}
