using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacterBehaviour
{
    [SerializeField] private float baseSpecialRecharge;
    private int lifes;
    private float specialRecharge;
    private bool charged;
    private Timer timer;

    private ICharacterBehaviour characterBehaviour;

    private void Awake()
    {
        lifes = 3;
        characterBehaviour = GetComponent<ICharacterBehaviour>();
    }

    private void Update()
    {
        if (!timer.Counter(ref specialRecharge, baseSpecialRecharge))
            charged = true;
    }

    public void TakeDamage() => lifes -= 1;

    public void Special()
    {
        if (charged)
        {
            characterBehaviour.Special();
        }
    }
}
