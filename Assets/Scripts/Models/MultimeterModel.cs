using Base;
using System;
using static Base.SignalsProvider;

public enum MultimeterMode
{
    Neutral,    
    Resistance, 
    Current,    
    VoltageDC,  
    VoltageAC   
}

public class MultimeterModel
{
    public MultimeterMode CurrentMode => _currentMode;

    private MultimeterMode _currentMode = MultimeterMode.Neutral;

    private float _inputResistance = 1000f;
    private float _inputPower= 400f;
    private float _currentValue;

    public void SetMode(MultimeterMode mode)
    {
        _currentMode = mode;
        _currentValue = GetValue();
        EventBus.Instance.Publish<ModeChangeSignal>(new ModeChangeSignal(_currentMode));
        EventBus.Instance.Publish<DisplayChangeSignal>(new DisplayChangeSignal(_currentValue));
    }

    private float GetValue()
    {
        switch (_currentMode)
        {
            case MultimeterMode.Neutral: 
                return 0f;
            case MultimeterMode.Resistance: 
                return _inputResistance;
            case MultimeterMode.Current: 
                return GetI();
            case MultimeterMode.VoltageDC: 
                return GetA();
            case MultimeterMode.VoltageAC: 
                return 0.01f;
            default: 
                return 0f;
        }
    }

    private float GetI()
    {
        double current = Math.Sqrt(_inputPower/_inputResistance);
        float I = (float)Math.Round(current, 2);
        return I;
    }

    private float GetA()
    {
        double voltageDC = Math.Sqrt(_inputPower * _inputResistance);
        float A = (float)Math.Round(voltageDC, 2);
        return A;
    }
}
