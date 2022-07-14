using Behavior.Gameplay;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Logger;

namespace Behavior.Common
{
    public abstract class BaseGameComponent<T> : GameComponent
    {
        protected bool IsInit { get; private set; }


        protected abstract void InitInternal(T starter);

        public void Init(T starter) {
            if ( IsInit ) {
                return;
            }

            InitInternal(starter);

            IsInit = true;
        }


#if UNITY_EDITOR
        static BaseGameComponent() {
            EditorSceneManager.sceneSaving += ValidateScene;
        }

        static void ValidateScene(Scene scene, string path) {
            var components = Resources.FindObjectsOfTypeAll<BaseGameComponent<T>>();
            foreach ( var comp in components ) {
                if ( !comp.gameObject.activeInHierarchy && !string.IsNullOrEmpty(comp.gameObject.scene.name) ) {
                    Log.TraceWarningFormat(LogTag.Behaviour, comp,
                        "Game component {0} on game object {1} is inactive and won't be initialized on scene start",
                        comp.GetType().Name, comp.name);
                }
            }
        }
#endif
    }
}
