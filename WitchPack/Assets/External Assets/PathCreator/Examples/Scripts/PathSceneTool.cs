using External_Assets.PathCreator.Core.Runtime.Objects;
using UnityEngine;

namespace External_Assets.PathCreator.Examples.Scripts
{
    [ExecuteInEditMode]
    public abstract class PathSceneTool : MonoBehaviour
    {
        public event System.Action onDestroyed;
        public Core.Runtime.Objects.PathCreator pathCreator;
        public bool autoUpdate = true;

        protected VertexPath path {
            get {
                return pathCreator.path;
            }
        }

        public void TriggerUpdate() {
            PathUpdated();
        }


        protected virtual void OnDestroy() {
            if (onDestroyed != null) {
                onDestroyed();
            }
        }

        protected abstract void PathUpdated();
    }
}
