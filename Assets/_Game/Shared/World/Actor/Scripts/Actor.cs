using UnityEngine;

namespace LOK1game
{
    public abstract class Actor : MonoBehaviour
    {
        protected virtual void SubscribeToEvents()
        {

        }

        protected virtual void UnsubscribeFromEvents()
        {

        }

        protected ProjectContext GetProjectContext()
        {
            return App.ProjectContext;
        }
    }
}