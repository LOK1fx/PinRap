using System;
using UnityEngine;

namespace LOK1game
{
    public class GameEditorObject : MonoBehaviour
    {
        public event Action Updated;

        public bool Hooked { get; private set; }
        public string Name => _name;

        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;

        private void Start()
        {
            GameEditorGizmoManager.Instance.Register(this);
        }

        public void SetName(string name)
        {
            _name = name;

            Updated?.Invoke();
        }

        public bool TryHook()
        {
            if (Hooked)
                return false;

            Hooked = true;

            Updated?.Invoke();

            return true;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }
    }
}