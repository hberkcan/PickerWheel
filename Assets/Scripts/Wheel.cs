using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;
using MyMessagingSystem;

public enum WheelType { Bronze, Silver, Gold}
public class Wheel : MonoBehaviour
{
    [Space]
    [SerializeField] private Transform PickerWheelTransform;
    [SerializeField] private Transform wheelCircle;
    [SerializeField] private Piece wheelPiecePrefab;
    [SerializeField] private Transform wheelPiecesParent;
    [SerializeField] private Image wheelCircleImage;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private WheelTypeContainer wheelTypeContainer;

    [Space]
    [Header("Picker wheel settings :")]
    [SerializeField] [Range(1, 20)] private int spinDuration = 8;
    [SerializeField] [Range(0.5f, 1.5f)] private float oscillateDuration = 1;
    [SerializeField] [Range(.2f, 2f)] private float wheelSize = 1f;

    [Space]
    [Header("Picker wheel pieces :")]
    public WheelPiece[] WheelPieces;
    private Piece[] pieces;

    // Events
    private Action onSpinStartEvent;
    private Action onSpinEndEvent;

    private bool _isSpinning = false;

    public bool IsSpinning { get { return _isSpinning; } }


    [SerializeField] private IntVariable numberOfPieces;

    private float pieceAngle;
    private float halfPieceAngle;
    private float halfPieceAngleWithPaddings;


    private double accumulatedWeight;

    private System.Random rand = new System.Random();

    private List<int> nonZeroChancesIndices = new List<int>();

    private void Awake()
    {
        pieceAngle = 360 / WheelPieces.Length;
        halfPieceAngle = pieceAngle / 2f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 2f);
        pieces = new Piece[numberOfPieces.Value];
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < WheelPieces.Length; i++)
            DrawPiece(i);
    }

    private void DrawPiece(int index)
    {
        Piece piece = InstantiatePiece();
        pieces[index] = piece;

        piece.transform.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
    }

    public void Setup(WheelPiece[] _wheelPieces, WheelType type)
    {
        WheelPieces = _wheelPieces;

        for(int i = 0; i < WheelPieces.Length; i++)
        {
            WheelPieces[i].Setup(pieces[i]);
            CalculateWeightsAndIndices(i);
        }

        if (nonZeroChancesIndices.Count == 0)
            Debug.LogError("You can't set all pieces chance to zero");

        WheelTypeData wheelTypeData = wheelTypeContainer.GetWheelType(type);
        wheelCircleImage.sprite = wheelTypeData.Wheel;
        indicatorImage.sprite = wheelTypeData.Indicator;
    }

    private Piece InstantiatePiece()
    {
        return Instantiate(wheelPiecePrefab, wheelPiecesParent);
    }


    public void Spin()
    {
        if (!_isSpinning)
        {
            _isSpinning = true;
            onSpinStartEvent?.Invoke();
            MessagingSystem.Instance.Dispatch(new SpinStartMessage());

            int index = GetRandomPieceIndex();
            WheelPiece wheelPiece = WheelPieces[index];

            if (wheelPiece.Chance == 0 && nonZeroChancesIndices.Count != 0)
            {
                index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
                wheelPiece = WheelPieces[index];
            }

            float angle = -(pieceAngle * index);

            float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;

            Vector3 targetRotation = Vector3.back * (leftOffset + 2 * 360 * spinDuration);

            var spinSequence = DOTween.Sequence()
            .Append(wheelCircle.DORotate(targetRotation, spinDuration, RotateMode.Fast)
                .SetEase(Ease.OutCubic))
            .Append(wheelCircle.DORotate(Vector3.back * angle, oscillateDuration, RotateMode.Fast)
                .SetEase(Ease.OutBack))
            .OnComplete(() =>
            {
                _isSpinning = false;
                onSpinEndEvent?.Invoke();
                //MessagingSystem.Instance.Dispatch(new SpinEndMessage(wheelPiece));
                wheelPiece.OnDrop();
            });
        }
    }

    public void OnSpinStart(Action action)
    {
        onSpinStartEvent += action;
    }

    public void OnSpinEnd(Action action)
    {
        onSpinEndEvent += action;
    }


    private int GetRandomPieceIndex()
    {
        double r = rand.NextDouble() * accumulatedWeight;

        for (int i = 0; i < WheelPieces.Length; i++)
            if (WheelPieces[i].Weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeightsAndIndices(int index)
    {
        WheelPiece piece = WheelPieces[index];

        //add weights:
        accumulatedWeight += piece.Chance;
        piece.Weight = accumulatedWeight;

        //add index :
        piece.Index = index;

        //save non zero chance indices:
        if (piece.Chance > 0)
            nonZeroChancesIndices.Add(index);
    }

    private void OnValidate()
    {
        if (PickerWheelTransform != null)
            PickerWheelTransform.localScale = new Vector3(wheelSize, wheelSize, 1f);

        if (WheelPieces.Length != numberOfPieces.Value)
            Debug.LogError("[ PickerWheelwheel ]  pieces length must equal to" + numberOfPieces);
    }
}
