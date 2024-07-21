using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PartyTokenHandler : MonoBehaviour
{
    [SerializeField] private float _pathMaskInterval;
    [SerializeField] private float _pathMaskRadius;
    [SerializeField] private Transform _partyToken;
    [SerializeField] private Transform _maskPrefab;
    [SerializeField] private int _pathResolution;
    [SerializeField] private Transform _maskSpawnPoint;
    
    private List<Transform> _pathMasks = new();
    private float _pathMaskTimer;
    private TweenerCore<Vector3, Path, PathOptions> _tokenAnimation;

    /// <param name="pathPoints"> works according to DoPath points format</param>
    public void AnimateToken(Vector3 startPos, Vector3[] pathPoints, float duration)
    {
        _partyToken.position = startPos;
        _tokenAnimation = _partyToken.DOPath(pathPoints, duration, PathType.CubicBezier,PathMode.TopDown2D,_pathResolution,Color.blue);
        _tokenAnimation.onComplete += OnFinishTokenAnimation;
        _tokenAnimation.onUpdate += SpawnPathMasks;
        _pathMaskTimer = 0;
    }

    
    private void SpawnPathMasks()
    {
        if (_pathMaskTimer > _pathMaskInterval)
        {
            var mask = Instantiate(_maskPrefab, _maskSpawnPoint.position, Quaternion.identity,transform);
            mask.localScale = new Vector3(_pathMaskRadius, _pathMaskRadius, _pathMaskRadius);
            _pathMasks.Add(mask);
            _pathMaskTimer = 0;
        }
        else _pathMaskTimer += Time.deltaTime;
    }

    private void OnFinishTokenAnimation()
    {
        _tokenAnimation = null;
    }

    public void ResetToken()
    {
        if(_tokenAnimation != null) _tokenAnimation.Kill();
        if (GameManager.SaveData.CurrentNode != null)
        {
            _partyToken.position = GameManager.SaveData.CurrentNode.transform.position;
        }
        else
        {
            _partyToken.position = MapManager.Instance.NodeObjects[0].transform.position;
        }

        ClearPath();
    }

    public void ClearPath()
    {
        if(_pathMasks.Count <= 0) return;
        foreach (var mask in _pathMasks)
        {
            Destroy(mask.gameObject);
        }
        _pathMasks.Clear();
    }
}
