using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanningController : MonoBehaviour
{
    [SerializeField, Header("Camera")]
    private Camera _targetCamera;

    [SerializeField, Header("Centre Point")]
    private Transform _centrePoint;

    [SerializeField, Header("Control Points")]
    private Transform[] _controlPoints;

    [Header("Centre Distance")]
    [SerializeField] private float _centreXDistance = 0.5f;
    [SerializeField] private float _centreYDistance = 0.5f;

    [SerializeField, Header("Smooth Damping")]
    private float _smoothDamping = 0.5f;

    private Vector2 _velocity;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseVector = new(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 viewportVector = _targetCamera.ScreenToViewportPoint(mouseVector);

        int i = 0;

        float left = _controlPoints[i++].position.x;
        float top = _controlPoints[i++].position.y;
        float right = _controlPoints[i++].position.x;
        float down = _controlPoints[i].position.y;

        Vector4 bounds = new(left, top, right, down);
        Vector2 finalPosition = GetFinalCoordinates(bounds, viewportVector);

        _targetCamera.transform.position = Vector2.SmoothDamp(_targetCamera.transform.position, finalPosition, ref _velocity, _smoothDamping);
    }

    private Vector2 GetFinalCoordinates(Vector4 bounds, Vector2 viewport)
    {
        float thresholdValueX = _centreXDistance / 2;
        float thresholdValueY = _centreYDistance / 2;

        float finalPosX = Mathf.Lerp(bounds.x, bounds.z, Mathf.Clamp(viewport.x, thresholdValueX, 1 - thresholdValueX));
        float finalPosY = Mathf.Lerp(bounds.w, bounds.y, Mathf.Clamp(viewport.y, thresholdValueY, 1 - thresholdValueY));

        return new(finalPosX, finalPosY);
    }
}
