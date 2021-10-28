using UnityEngine;

public class MoodColor : MonoBehaviour
{
    [SerializeField]
    private Color _color = default;
    private Color _previousColor = default;

    private MoodSync _moodSync;

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();
    }

    private void Update()
    {
        if (_color != _previousColor)
        {
            _moodSync.SetColor(_color);
            _previousColor = _color;
        }
    }
}
