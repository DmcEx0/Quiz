using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewColorsConfig", menuName = "Config/Colors")]
public class ColorFillConfig : ScriptableObject
{
    [SerializeField]
    private List<Color> _colorFrames;

    public IReadOnlyCollection<Color> ColorFrames => _colorFrames;
}