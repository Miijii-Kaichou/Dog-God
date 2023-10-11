using UnityEngine;

public sealed class CameraPanningController : MonoBehaviour
{
    [SerializeField, Header("Camera")]
    private Camera _targetCamera;

    [SerializeField, Header("Centre Point")]
    private Transform _centrePoint;

    [SerializeField, Header("Control Points")]
    private Transform[] _controlPoints;

    [Header("Centre Distance")]
    public float centreXDistance = 0.5f;
    public float centreYDistance = 0.5f;

    [SerializeField, Header("Smooth Damping")]
    private float _smoothDamping = 0.5f;

    public bool controlWithCursor;

    private Vector2 _velocity;
    private Vector3 _viewportVector;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseVector = new(Input.mousePosition.x, Input.mousePosition.y);
        _viewportVector = _targetCamera.ScreenToViewportPoint(mouseVector);

        int i = 0;

        float left = _controlPoints[i++].position.x;
        float top = _controlPoints[i++].position.y;
        float right = _controlPoints[i++].position.x;
        float down = _controlPoints[i].position.y;

        Rect bounds = new(left, top, right, down);
        Vector2 finalPosition = GetFinalCoordinates(bounds, _viewportVector);

        _targetCamera.transform.position = Vector2.SmoothDamp(_targetCamera.transform.position, finalPosition, ref _velocity, _smoothDamping);
    }

    public void SetViewportVector(Vector2 viewportVector)
    {
        _viewportVector = viewportVector;
    }

    Vector2 GetFinalCoordinates(Rect bounds, Vector2 viewport)
    {
        float thresholdValueX = centreXDistance / 2;
        float thresholdValueY = centreYDistance / 2;

        float finalPosX = Mathf.Lerp(bounds.x, bounds.width, Mathf.Clamp(viewport.x, thresholdValueX, 1 - thresholdValueX));
        float finalPosY = Mathf.Lerp(bounds.height, bounds.y, Mathf.Clamp(viewport.y, thresholdValueY, 1 - thresholdValueY));

        return new(finalPosX, finalPosY);
    }

    public Camera GetCamera() => _targetCamera;
}
