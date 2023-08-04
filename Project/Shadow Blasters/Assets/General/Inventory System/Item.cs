using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Estrutura genérica que representa Items no geral
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("UI")]
    public Sprite Sprite;
    public int StackSize;
}
