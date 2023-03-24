using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Wheel Type Container", menuName = "Wheel Type Container")]
public class WheelTypeContainer : ScriptableObject
{
    public Sprite BronzeWheel;
    public Sprite BronzeIndicator;

    public Sprite SilverWheel;
    public Sprite SilverIndicator;

    public Sprite GoldWheel;
    public Sprite GoldIndicator;

    public WheelTypeData GetWheelType(WheelType wheelType)
    {
        WheelTypeData wheelTypeData = new WheelTypeData();

        if(wheelType == WheelType.Bronze)
        {
            wheelTypeData.Wheel = BronzeWheel;
            wheelTypeData.Indicator = BronzeIndicator;
        }
        else if(wheelType == WheelType.Silver)
        {
            wheelTypeData.Wheel = SilverWheel;
            wheelTypeData.Indicator = SilverIndicator;
        }
        else if(wheelType == WheelType.Gold)
        {
            wheelTypeData.Wheel = GoldWheel;
            wheelTypeData.Indicator = GoldIndicator;
        }

        return wheelTypeData;
    }
}

[System.Serializable]
public class WheelTypeData
{
    public Sprite Wheel;
    public Sprite Indicator;
}
