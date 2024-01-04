using System.Collections.Generic;
using UnityEngine;


public class PartyUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform _heroContainer;
    [SerializeField] private ShamanUIHandler _shamanUIHanlder;
    
    
    public void Init(List<Shaman> party)
    {
    
        foreach (var shaman in party)
        {
            var shamanUI = Instantiate(_shamanUIHanlder, _heroContainer);
            shamanUI.SetShamanData(shaman);
        }
        
    }
}