using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private Transform _handle;

    [SerializeField] private Material _highlightMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Renderer _handleRenderer;

    [SerializeField] private float _rotationStep = 36f; //TEMP

    private bool _handleIsActive = false;
    private MultimeterMode _currentMode = MultimeterMode.Neutral;

    void Update()
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
        int newModeIndex = (int)_currentMode + direction;

        if (newModeIndex < 0)
            newModeIndex = 4;
        if (newModeIndex > 4)
            newModeIndex = 0;

        _currentMode = (MultimeterMode)newModeIndex;

        RotateHandle();
    }

    void RotateHandle()
    {
        if (_handle != null)
        {
            float targetRotation = (int)_currentMode * _rotationStep;
            _handle.localEulerAngles = new Vector3(0, 0, targetRotation);
        }
    }

    void HighlightHandle(bool highlight)
    {
        if (_handleRenderer != null && _highlightMaterial != null && _defaultMaterial != null)
            _handleRenderer.material = highlight ? _highlightMaterial : _defaultMaterial;
    }
}
