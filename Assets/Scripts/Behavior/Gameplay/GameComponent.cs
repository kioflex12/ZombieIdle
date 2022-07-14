using UnityEngine;
using Utils;

namespace Behavior.Gameplay
{
    public abstract class GameComponent : MonoBehaviour
    {
        protected virtual void Init()
        {
        }

        protected virtual void DeInit()
        {
        }

        protected virtual void CheckDescription()
        {
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            GameComponentUtils.CheckAttributes(this);
            CheckDescription();
            Init();
        }

        protected void OnValidate()
        {
            if ( (gameObject.hideFlags & HideFlags.DontSaveInEditor) != 0 ) {
                return;
            }
            GameComponentUtils.CheckAttributes(this);
            CheckDescription();
        }

        private void OnDestroy()
        {
            DeInit();
        }
    }
}

