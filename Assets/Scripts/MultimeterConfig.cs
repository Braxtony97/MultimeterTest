using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModePosition
{
    public MultimeterMode Mode;
    public float RotationAngle;
}

[CreateAssetMenu(fileName = "MultimeterConfig", menuName = "Multimeter/Config")]
public class MultimeterConfig : ScriptableObject
{
    public List<ModePosition> modePositions = new List<ModePosition>();
}
