using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PickerWheelView : View
{
    [SerializeField] private Wheel wheel;
    [SerializeField] private Button spinButton;
    [SerializeField] private Image spinButtonImage;
    [SerializeField] private Sprite spinButtonSprite;
    [SerializeField] private Sprite spinButtonEmptySprite;

    [SerializeField] private IntVariable zoneCount;

    private void OnEnable()
    {
        spinButtonImage.sprite = spinButtonSprite;
        spinButton.interactable = true;

        wheel.transform.DOScale(Vector3.one, 0.2f);
        wheel.Setup(GameManager.Instance.RandomWheel(wheel), WheelType.Bronze);
    }

    public override void Init()
    {
        spinButton.onClick.AddListener(wheel.Spin);

        wheel.OnSpinStart(() =>
        {
            spinButtonImage.sprite = spinButtonEmptySprite;
            spinButton.interactable = false;
        });

        wheel.OnSpinEnd(() =>
        {
            StartCoroutine(NextWheel());
        });
    }

    public IEnumerator NextWheel()
    {
        zoneCount.Value++;
        wheel.transform.DOScale(Vector3.one * 0.1f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        spinButtonImage.sprite = spinButtonSprite;
        spinButton.interactable = true;
        wheel.transform.DOScale(Vector3.one, 0.2f);

        if(zoneCount.Value % 5 == 0)
        {
            wheel.Setup(GameManager.Instance.RandomWheel(false), WheelType.Silver);
        }
        else if(zoneCount.Value % 30 == 0) 
        {
            wheel.Setup(GameManager.Instance.RandomWheel(false), WheelType.Gold);
        }
        else
        {
            wheel.Setup(GameManager.Instance.RandomWheel(wheel), WheelType.Bronze);
        }
    }
}
