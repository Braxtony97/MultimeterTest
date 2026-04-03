using UnityEngine;

public enum MultimeterMode
{
    Neutral,    
    Resistance, 
    Current,    
    VoltageDC,  
    VoltageAC   
}

public class MultimeterModel : MonoBehaviour
{
    public MultimeterMode CurrentMode => _currentMode;

    private MultimeterMode _currentMode = MultimeterMode.Neutral;

    private float _inputResistance = 1000f;
    private float _inputPower= 400f;

    public void SetMode(MultimeterMode mode)
    {
        _currentMode = mode;
    }
}
