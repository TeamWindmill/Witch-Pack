using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class PartyTokenHandler : MonoBehaviour
{
    [SerializeField] private float _pathMaskInterval;
    [SerializeField] private Transform _partyToken;
    [SerializeField] private PathType _pathType;
    [SerializeField] private Transform _maskPrefab;
    [SerializeField] private Transform _maskSpawnPoint;
    
    private List<Transform> _pathMasks = new();
    private float _pathMaskTimer;
    private TweenerCore<Vector3, Path, PathOptions> _tokenAnimation;
    
    public void AnimateToken(Transform pathParent, float duration)
    {
        Vector3[] pathArray = new Vector3[pathParent.childCount];
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }

        _partyToken.position = pathArray[0];
        _tokenAnimation = _partyToken.DOPath(pathArray, duration, PathType.CatmullRom);
        _tokenAnimation.onComplete += OnFinishTokenAnimation;
        _tokenAnimation.onUpdate += SpawnPathMasks;
        _pathMaskTimer = 0;
    }

    
    private void SpawnPathMasks()
    {
        if (_pathMaskTimer > _pathMaskInterval)
        {
            var mask = Instantiate(_maskPrefab, _maskSpawnPoint.position, Quaternion.identity,transform);
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
        
        if(_pathMasks.Count <= 0) return;
        foreach (var mask in _pathMasks)
        {
            Destroy(mask.gameObject);
        }
        _pathMasks.Clear();
    }
}
