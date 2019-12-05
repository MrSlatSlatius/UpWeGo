using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public void Move(Vector3 motion)
    {
        transform.position += motion * Time.fixedDeltaTime;
    }
}
