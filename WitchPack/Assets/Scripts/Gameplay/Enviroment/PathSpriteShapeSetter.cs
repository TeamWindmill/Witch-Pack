using UnityEngine;

namespace Gameplay.Enviroment
{
    public class PathSpriteShapeSetter : MonoBehaviour
    {
        [SerializeField] float _pathAngle;
        [SerializeField] float _pathScale;
    
        private void OnValidate()
        {
            gameObject.transform.eulerAngles = new Vector3 (_pathAngle, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
            gameObject.transform.localScale = new Vector3 (_pathScale, _pathScale, _pathScale);
        }
    }
}
