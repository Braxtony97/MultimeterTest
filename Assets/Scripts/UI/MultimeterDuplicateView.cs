using Base;
using System;
using TMPro;
using UnityEngine;
using static Base.SignalsProvider;

public class MultimeterDuplicateView : MonoBehaviour
{
    [SerializeField] private TMP_Text _voltageDC;
    [SerializeField] private TMP_Text _currentDC;
    [SerializeField] private TMP_Text _voltageAC;
    [SerializeField] private TMP_Text _resistance;

    private MultimeterMode _currentMode;
    private string _zeroValue = "0.00";

    private void Start()
    {
        EventBus.Instance.Subscribe<DisplayChangeSignal>(OnDisplayChanged);
        EventBus.Instance.Subscribe<ModeChangeSignal>(OnModeChanged);

        ResetAllDisplays();
    }

    private void OnModeChanged(ModeChangeSignal signal)
    {
        _currentMode = signal.CurrentMode;

        ResetAllDisplays();
    }

    private void OnDisplayChanged(DisplayChangeSignal signal)
    {
        ResetAllDisplays();

        string convertedSignal = signal.Value.ToString();

        switch (_currentMode)
        {
            case MultimeterMode.VoltageDC:
                _voltageDC.text = convertedSignal;
                break;
            case MultimeterMode.Current:
                _currentDC.text = convertedSignal;
                break;
            case MultimeterMode.VoltageAC:
                _voltageAC.text = convertedSignal;
                break;
            case MultimeterMode.Resistance:
                _resistance.text = convertedSignal;
                break;
            case MultimeterMode.Neutral:
                break;
        }
    }

    private void ResetAllDisplays()
    {
        _voltageDC.text = _zeroValue;
        _currentDC.text = _zeroValue;
        _voltageAC.text = _zeroValue;
        _resistance.text = _zeroValue;
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe<DisplayChangeSignal>(OnDisplayChanged);
        EventBus.Instance.Unsubscribe<ModeChangeSignal>(OnModeChanged);
    }
}
