using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private NodeObject[] _nodeObjects;
    [SerializeField] private bool _unLockAll;

    [Header("Camera Control")] [SerializeField]
    private Vector2 _cameraLockedPos;

    [SerializeField] private int _cameraLockedZoom;

    [SerializeField] private bool[] _nodeLockState;
    private bool[] _nodeCompletedState = new bool[3];

    private void Awake()
    {
        for (int i = 0; i < _nodeObjects.Length; i++)
        {
            _nodeObjects[i].Init(_nodeCompletedState[i], _nodeLockState[i]);
        }
    }

    private void Start()
    {
        GameManager.Instance.CameraHandler.LockCamera(_cameraLockedPos, _cameraLockedZoom);
    }

    public void UnlockLevels(bool state)
    {
        if(!state) return;
        foreach (var nodeObject in _nodeObjects)
        {
            nodeObject.Unlock();
        }
    }
}