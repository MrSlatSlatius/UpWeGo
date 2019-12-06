using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -18)
            Destroy(gameObject);
    }
}
