using System.Collections;
using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private Transform _handle;
    [SerializeField] private MultimeterConfig _config;

    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Renderer _handleRenderer;

    [SerializeField] private float _duration = 0.15f;

    private MultimeterMode _currentMode = MultimeterMode.Neutral;
    private bool _handleIsActive = false;
    private float _currentRotation = 0f;
    private MultimeterModel _multimeterModel;

    private void Start()
    {
        _multimeterModel = new MultimeterModel();
    }

    private void Update()
    {
        CheckMouseOver();

        if (_handleIsActive && Input.mouseScrollDelta.y != 0)
        {
            int direction = Input.mouseScrollDelta.y > 0 ? -1 : 1;
            ChangeMode(direction);
        }
    }

    private void CheckMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == _handle || hit.transform == _handle)
            {
                if (!_handleIsActive)
                {
                    _handleIsActive = true;
                    HighlightHandle(true);
                }
                return;
            }
        }

        if (_handleIsActive)
        {
            _handleIsActive = false;
            HighlightHandle(false);
        }
    }

    void ChangeMode(int direction)
    {
        if (_config == null || _config.modePositions.Count == 0) 
            return;

        int currentIndex = _config.modePositions.FindIndex(p => p.Mode == _currentMode);

        if (currentIndex == -1) 
            return;

        int newIndex = currentIndex + direction;

        if (newIndex < 0)
            newIndex = _config.modePositions.Count - 1;
        if (newIndex >= _config.modePositions.Count)
            newIndex = 0;

        _currentMode = _config.modePositions[newIndex].Mode;
        float targetRotation = GetRotationAngleForMode(_currentMode);

        StartCoroutine(RotateSmoothly(targetRotation));

        _multimeterModel.SetMode(_currentMode);
    }

    private IEnumerator RotateSmoothly(float targetAngle)
    {
        float startAngle = _currentRotation;
        float delta = Mathf.DeltaAngle(startAngle, targetAngle);
        float endAngle = startAngle + delta;

        float elapsed = 0f;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _duration;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            _handle.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        _currentRotation = endAngle;
        _handle.localEulerAngles = new Vector3(0, 0, endAngle);
    }

    float GetRotationAngleForMode(MultimeterMode mode)
    {
        var position = _config.modePositions.Find(p => p.Mode == mode);

        if (position != null)
            return position.RotationAngle;

        return 0f;
    }

    void HighlightHandle(bool highlight)
    {
        if (_handleRenderer != null && _highlightMaterial != null && _defaultMaterial != null)
            _handleRenderer.material = highlight ? _highlightMaterial : _defaultMaterial;
    }
}
