using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Tag : byte
    {
        SubmarineMine = 0,
        ForbiddenFruit = 1
    }

    [SerializeField] private Tag tag = Tag.SubmarineMine;

    public Tag ItemTag => tag;
}
