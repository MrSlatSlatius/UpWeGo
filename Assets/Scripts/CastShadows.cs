using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CastShadows : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }
}
