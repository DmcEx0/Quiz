using UnityEngine;
using UnityEngine.UI;

public class HomeButtonView : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    public Button Button => _button;
}