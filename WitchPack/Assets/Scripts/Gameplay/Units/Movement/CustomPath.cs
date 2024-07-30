using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Units.Movement
{
    public class CustomPath : MonoBehaviour
    {
        //enemies will spawn according to which path they are going to follow, 
        //when a shaman enters the enemies detection range rng to see if it aggros it
        //when the shaman target cannot be detected anymore return to the current waypoint on the assigned path 

        //assign wayoints from inspecotor -> curves and turns need to be expressed with more wayponits 
        [SerializeField, TabGroup("Way Points")] private List<Transform> waypoints = new List<Transform>();

        public List<Transform> Waypoints { get => waypoints; }




    }
}
