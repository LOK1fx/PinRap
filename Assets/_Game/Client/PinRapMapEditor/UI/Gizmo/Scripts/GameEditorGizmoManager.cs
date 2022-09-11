using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class GameEditorGizmoManager : Singleton<GameEditorGizmoManager>
    {
        [SerializeField] private GameEditorGizmo _gizmoPrefab;

        private List<GameEditorObject> _editorObjects = new List<GameEditorObject>();
        private List<GameEditorGizmo> _spawnedGizmos = new List<GameEditorGizmo>();

        private Camera _playerCamera;

        private void Start()
        {
            _playerCamera = Camera.main;
        }

        public void Register(GameEditorObject editorObject)
        {
            var gizmo = Instantiate(_gizmoPrefab, transform);
            gizmo.Setup(editorObject, _playerCamera);

            _editorObjects.Add(editorObject);
            _spawnedGizmos.Add(gizmo);
        }
    }
}