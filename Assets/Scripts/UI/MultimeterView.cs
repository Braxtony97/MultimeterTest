using Base;
using TMPro;
using UnityEngine;
using static Base.SignalsProvider;

public class MultimeterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _displayText;

    private void Start()
    {
        EventBus.Instance.Subscribe<DisplayChangeSignal>(OnDisplayChanged);
    }

    private void OnDisplayChanged(DisplayChangeSignal signal)
    {
        _displayText.text = signal.Value.ToString();
    }
}
