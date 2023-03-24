using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Piece : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI text;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Setup(Sprite sprite, int amount)
    {
        image.sprite = sprite;
        text.text = $"x{amount}";
    }

    public void Setup(Sprite sprite)
    {
        image.sprite = sprite;
        text.text = "";
    }
}
