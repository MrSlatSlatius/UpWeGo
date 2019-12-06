using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Tag : byte
    {
        Potion = 0,
    }

    [SerializeField] private Tag tag = Tag.Potion;

    public Tag ItemTag => tag;
}
