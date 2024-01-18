using System;
using System.Collections;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;


public class CameraHandler : MonoBehaviour
{
    #region Consts

    private const float ORTHOGRAPHIC_DETECT_RANGE = 0.2f;
    private const float CAMERA_MOVEMENT_DETECT_RANGE = 0.5f;

    private const float FULL_HD_PIXELS_X = 1920;
    private const float FULL_HD_PIXELS_Y = 1080;
    private const int LOCKED_CAMERA_ZOOM = 9;

    #endregion

    #region SerialzedFields

    [SerializeField, Tooltip("attach a camera setting config file to determine all of the camera variables")]
    private CameraSettings _cameraSettings;

    [Header("ON/OFF")] [SerializeField, Tooltip("toggle camera movement and zoom")]
    private bool _enableCameraMovement = true;

    [SerializeField, Tooltip("toggle mouse edge scroll camera movement")]
    private bool _enableEdgeScroll = true;

    [SerializeField, Tooltip("toggle mouse Pan scroll camera movement")]
    private bool _enablePanScroll = true;

    [SerializeField] private bool _testing = false;

    [Header("Game Objects")] [TabGroup("Cameras"), SerializeField]
    private Camera _mainCamera;

    [TabGroup("Cameras"), SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [TabGroup("Cameras"), SerializeField] private Transform _cameraFollowObject;
    [TabGroup("Cameras"), SerializeField] private CinemachineBrain _cinemachineBrain;

    [TabGroup("Post Process"), SerializeField]
    private Volume _postProcessVolume;

    public Volume PostProcessVolume => _postProcessVolume;

    #endregion

    #region Fields

    private readonly Vector3 _lockedCameraPosition = new(0, -3, -80);
    private Vector2 _cameraBorders;
    private float _cameraMaxZoom;
    private Vector2 _cameraStartPosition;
    private float _cameraStartZoom;
    private CinemachineTransposer _cinemachineTransposer;

    private float _currentAspectRatioX;
    private float _currentAspectRatioY;

    private bool _dragPanMoveActive = false;
    private Vector2 _dragPanSpeed;
    private Vector2 _lastMousePosition;
    private float _targetOrthographicSize;
    private float _zoomPadding;
    private Vector3 _inputDir;

    public Camera MainCamera => _mainCamera;

    #endregion

    #region Init

    private void Awake()
    {
        //only 1 camera in the scene
        if (Camera.allCameras.Length > 1)
            Destroy(gameObject);
    }

    private void Start()
    {
        //check if a camera settings config file is attached
        if (_cameraSettings is null)
        {
            string cameraSettingNullLog =
                ColorLogHelper.SetColorToString("Camera Settings", ColorLogHelper.CAMERA_HANDLER);
            throw new Exception($"{cameraSettingNullLog} is null"); //stop program?
        }

        //caching
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _cinemachineBrain = _mainCamera.GetComponent<CinemachineBrain>();

        _cinemachineTransposer.m_XDamping = _cameraSettings.XDamping;
        _cinemachineTransposer.m_YDamping = _cameraSettings.YDamping;
        _cameraMaxZoom = _cameraSettings.ZoomMaxClamp;
        _dragPanSpeed.x = _cameraSettings.CameraDragPanSpeed * _mainCamera.aspect;
        _dragPanSpeed.y = _cameraSettings.CameraDragPanSpeed;
        LockCamera(_lockedCameraPosition, LOCKED_CAMERA_ZOOM);
#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Confined;
#endif
    }

    #endregion

    #region Update

    private void Update()
    {
        //here we control the camera movement and zoom

        if (_enableCameraMovement) //option to disable camera movement and zoom
        {
            _inputDir = HandleInput();
            HandleZoom();

            HandleCameraMove(_inputDir);
        }
    }

    private Vector3 HandleInput()
    {
        var inputDir = new Vector3(0, 0, 0); //resetting the input direction
        //WASD Detection
        if (Input.GetKey(KeyCode.W)) {inputDir.y = +1f;}
        else if (Input.GetKey(KeyCode.S)) inputDir.y = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        else if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;
#if !UNITY_EDITOR
            //Edge Scrolling Detection
            if (_enableEdgeScroll)
            {
                if (Input.mousePosition.x < (Screen.width * _cameraSettings.EdgeScrollDetectSizeX))
                    inputDir.x = -1f;
                if (Input.mousePosition.y < (Screen.height * _cameraSettings.EdgeScrollDetectSizeY))
                    inputDir.y = -1f;
                if (Input.mousePosition.x > Screen.width - (Screen.width * _cameraSettings.EdgeScrollDetectSizeX))
                    inputDir.x = +1f;
                if (Input.mousePosition.y > Screen.height - (Screen.height * _cameraSettings.EdgeScrollDetectSizeY))
                    inputDir.y = +1f;
            }
#endif
        if (!_enablePanScroll) return inputDir;

        int mouseButtonId = _cameraSettings.RightClickToPanCamera ? 1 : 2;

        if (Input.GetMouseButtonDown(mouseButtonId))
        {
            _dragPanMoveActive = true;
            _lastMousePosition = GameManager.Instance.CameraHandler._mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(mouseButtonId))
        {
            _dragPanMoveActive = false;
        }

        if (!_dragPanMoveActive) return inputDir.normalized;

        var mouseMovementDelta = _lastMousePosition - (Vector2)GameManager.Instance.CameraHandler._mainCamera.ScreenToViewportPoint(Input.mousePosition);

        _lastMousePosition = GameManager.Instance.CameraHandler._mainCamera.ScreenToViewportPoint(Input.mousePosition);

        _dragPanSpeed.x = _cameraSettings.CameraDragPanSpeed * _mainCamera.aspect;
        _dragPanSpeed.y = _cameraSettings.CameraDragPanSpeed;

        var tempLogDragPanSpeed = _dragPanSpeed;
        _dragPanSpeed *= mouseMovementDelta.magnitude;
        if (_testing) Debug.Log($"drag speed: {tempLogDragPanSpeed}, Magnitude: {_dragPanSpeed}");
        return mouseMovementDelta.normalized;
    }

    private void HandleZoom()
    {
        //Mouse Scroll Zoom 
        if (Input.mouseScrollDelta.y > 0) //zoom in
        {
            _targetOrthographicSize -= _cameraSettings.ZoomChangeValue;
            
        }

        if (Input.mouseScrollDelta.y < 0) //zoom out
        {
            _targetOrthographicSize += _cameraSettings.ZoomChangeValue;
        }

        CameraZoomClamp();
    }

    private void HandleCameraMove(Vector3 inputDir)
    {
        var cameraTransform = transform;
        var cameraPosition = _cameraFollowObject.position;

        //setting the input direction to correspond with camera direction
        var moveDir = cameraTransform.up * inputDir.y + cameraTransform.right * inputDir.x;

        //determine the camera speed according to the camera zoom
        float currentZoomNormalizedValue =
            (_cinemachineVirtualCamera.m_Lens.OrthographicSize - _cameraSettings.ZoomMinClamp) /
            (_zoomPadding - _cameraSettings.ZoomMinClamp);
        float zoomSpeedChangeValue = currentZoomNormalizedValue * _cameraSettings.CameraSpeedZoomChangeValue;
        float fixedCameraSpeed = _cameraSettings.MoveSpeedMinimum + zoomSpeedChangeValue;
        if (_dragPanMoveActive)
        {
            moveDir.x *= _dragPanSpeed.x;
            moveDir.y *= _dragPanSpeed.y;
        }

        //moving the camera
        cameraPosition += moveDir * (fixedCameraSpeed * Time.deltaTime);
        cameraPosition = CameraMoveClamp(cameraPosition);
        _cameraFollowObject.position = cameraPosition;
    }

    private Vector3 CameraMoveClamp(Vector3 cameraPosition)
    {
        var orthographicSize = _mainCamera.orthographicSize;
        var cameraHeight = orthographicSize * 2;
        var cameraWidth = _mainCamera.aspect * cameraHeight;

        //clamping the camera to the borders of the map
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, (-_cameraBorders.x / 2 + cameraWidth / 2), (_cameraBorders.x / 2 - cameraWidth / 2));

        cameraPosition.y = Mathf.Clamp(cameraPosition.y, (-_cameraBorders.y / 2 + cameraHeight / 2), (_cameraBorders.y / 2 - cameraHeight / 2));

        return cameraPosition;
    }

    private void CameraZoomClamp()
    {
        //clamping the camera zoom
        _targetOrthographicSize =
            Mathf.Clamp(_targetOrthographicSize, _cameraSettings.ZoomMinClamp, _zoomPadding);
        //camera zoom 
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.OrthographicSize, _targetOrthographicSize,
            Time.deltaTime * _cameraSettings.ZoomSpeed);
    }

    #endregion

    #region PublicMethods

    public void SetCameraLevelSettings(CameraLevelSettings cameraLevelSettings)
    {
        _cameraBorders = cameraLevelSettings.CameraBorders;
        _cameraMaxZoom = cameraLevelSettings.CameraMaxZoom;
        if (cameraLevelSettings.OverrideCameraStartPos)
        {
            _cameraStartPosition = cameraLevelSettings.CameraStartPos;
            _cameraStartZoom = cameraLevelSettings.CameraStartZoom;
        }
    }

    [Button("Reset Camera")]
    public void ResetCamera()
    {
        //calculating the current aspect ratio according to screen resolution
        if (FULL_HD_PIXELS_X / FULL_HD_PIXELS_Y == _mainCamera.aspect)
        {
            _dragPanSpeed.x = _cameraSettings.CameraDragPanSpeed * _mainCamera.aspect;
            _dragPanSpeed.y = _cameraSettings.CameraDragPanSpeed;
            _zoomPadding = _cameraMaxZoom;
            if (_zoomPadding > _cameraSettings.ZoomMaxClamp) _zoomPadding = _cameraSettings.ZoomMaxClamp;
        }
        else
        {
            _currentAspectRatioX = FULL_HD_PIXELS_X / _mainCamera.pixelWidth;
            _currentAspectRatioY = FULL_HD_PIXELS_Y / _mainCamera.pixelHeight;

            //calculating the current padding for movement borders and zoom
            _dragPanSpeed.x = _cameraSettings.CameraDragPanSpeed * _mainCamera.aspect;
            _dragPanSpeed.y = _cameraSettings.CameraDragPanSpeed;
            _zoomPadding = _cameraMaxZoom * _currentAspectRatioX;
            if (_zoomPadding > _cameraSettings.ZoomMaxClamp) _zoomPadding = _cameraSettings.ZoomMaxClamp;
        }

        //resetting the camera position and zoom
        ToggleCameraLock(true);
        if (_cameraStartZoom > _cameraSettings.ZoomMinClamp && _cameraStartZoom < _zoomPadding)
        {
            _targetOrthographicSize = _cameraStartZoom;
            _mainCamera.orthographicSize = _cameraStartZoom;
        }
        else
        {
            _targetOrthographicSize = _cameraSettings.ZoomDefaultStartValue;
            _mainCamera.orthographicSize = _cameraSettings.ZoomDefaultStartValue;
        }

        _cameraFollowObject.position = new Vector3(_cameraStartPosition.x, _cameraStartPosition.y, -80);
        _mainCamera.transform.position = new Vector3(_cameraStartPosition.x, _cameraStartPosition.y, -80);
        ToggleCameraLock(false);
    }

    private void ToggleCameraLock(bool state)
    {
        _enableCameraMovement = !state;
        _cinemachineBrain.enabled = !state;
    }

    public void LockCamera(Vector2 lockedCameraPos, int lockedCameraZoom)
    {
        _mainCamera.GetComponent<CinemachineBrain>().enabled = false;
        _mainCamera.transform.position = new Vector3(lockedCameraPos.x, lockedCameraPos.y, -80);
        _mainCamera.orthographicSize = lockedCameraZoom;
        _enableCameraMovement = false;
    }

    public void SetCameraPosition(Vector2 eventPosition, bool lockCameraMove = false)
    {
        //move the camera to Event Position
        var newPos = new Vector3(eventPosition.x, eventPosition.y, -80);
        _cameraFollowObject.position = newPos;
        ChangeDamping(_cameraSettings.EventTransitionDampingX,_cameraSettings.EventTransitionDampingY);
        StartCoroutine(!lockCameraMove ? ChangeDampingUntilCameraFinishedFollowUp(_cameraSettings.EventTransitionDampingX, _cameraSettings.EventTransitionDampingY) 
            : LockCameraAfterFinishedFollowUp());
    }

    public void SetCameraZoom(int zoom)
    {
        //move the camera to Event Position
        _targetOrthographicSize = zoom;
    }

    #endregion

    #region PrivateMethods

    private bool CameraFinishedFollowUp()
    {
        var mainCameraPos = _mainCamera.transform.position;
        var cameraFollowObjectPos = _cameraFollowObject.position;
        bool cameraFinishedFollowUpX = mainCameraPos.x <= (cameraFollowObjectPos.x + CAMERA_MOVEMENT_DETECT_RANGE) &&
                                       mainCameraPos.x >= (cameraFollowObjectPos.x - CAMERA_MOVEMENT_DETECT_RANGE);
        bool cameraFinishedFollowUpY = mainCameraPos.y <= (cameraFollowObjectPos.y + CAMERA_MOVEMENT_DETECT_RANGE) &&
                                       mainCameraPos.y >= (cameraFollowObjectPos.y - CAMERA_MOVEMENT_DETECT_RANGE);
        bool cameraFinishedFollowUp = cameraFinishedFollowUpX && cameraFinishedFollowUpY;
        return cameraFinishedFollowUp;
    }

    

    private void ChangeDamping(float xDamping, float yDamping)
    {
        _cinemachineTransposer.m_XDamping = xDamping;
        _cinemachineTransposer.m_YDamping = yDamping;
    }
    
    private IEnumerator ChangeDampingUntilCameraFinishedFollowUp(float xDamping, float yDamping)
    {
        ChangeDamping(xDamping, yDamping);
        yield return new WaitUntil(() => CameraFinishedFollowUp() || _inputDir != Vector3.zero);
        ChangeDamping(_cameraSettings.XDamping, _cameraSettings.XDamping);
        yield return null;
    }
    private IEnumerator LockCameraAfterFinishedFollowUp()
    {
        yield return new WaitUntil(() => CameraFinishedFollowUp() || _inputDir != Vector3.zero);
        ToggleCameraLock(true);
        yield return null;
    }
    #endregion
}