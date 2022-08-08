using UnityEngine;
using UnityEditor;

namespace LOK1game.Editor
{
    public class DebugText : MonoBehaviour
    {
        public string Text;
        public Vector3 PositionOffset;
        
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position + PositionOffset, Text);
        }

#endif
    }
}