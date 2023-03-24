using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<ItemDataObject> items;
    [SerializeField] private ItemDataObject bomb;
    [SerializeField] private IntVariable numberOfPieces;
    [SerializeField] private IntVariable zoneCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public WheelPiece[] RandomWheel(bool bombIncluded = true)
    {
        List<ItemDataObject> list = items.OrderBy(x => Guid.NewGuid()).Take(numberOfPieces.Value).ToList();
        WheelPiece[] wheelPieces = new WheelPiece[numberOfPieces.Value];

        for (int i = 0; i < numberOfPieces.Value; i++)
        {
            WheelPiece wheelPiece = new()
            {
                Data = list[i],
                Countable = true,
                Amount = zoneCount.Value + UnityEngine.Random.Range(0, 10),
                Chance = 12.5f
            };

            wheelPieces[i] = wheelPiece;
        }

        if (!bombIncluded)
            return wheelPieces;

        int r = UnityEngine.Random.Range(0, numberOfPieces.Value);
        wheelPieces[r].Data = bomb;
        wheelPieces[r].Countable = false;

        return wheelPieces;
    }
}
