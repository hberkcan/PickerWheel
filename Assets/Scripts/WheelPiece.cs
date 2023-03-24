using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelPiece
{
    public ItemDataObject Data;

    public bool Countable;

    [ShowIf("Countable")]
    [Tooltip("Reward amount")]
    public int Amount;

    [Tooltip("Probability in %")]
    [Range(0f, 100f)]
    public float Chance = 100f;

    [HideInInspector] public int Index;
    [HideInInspector] public double Weight = 0f;

    public void OnDrop()
    {
        Data.OnDrop(Amount);
    }

    public void Setup(Piece piece)
    {
        if (Countable)
            piece.Setup(Data.icon, Amount);
        else
            piece.Setup(Data.icon);
    }
}
