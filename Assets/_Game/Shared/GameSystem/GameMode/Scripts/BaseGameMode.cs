using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public enum EGameModeState : ushort
{
    Starting = 1,
    Started,
    Ending,
    Ended,
}

namespace LOK1game.Game
{

    [Serializable]
    public abstract class BaseGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeState State { get; protected set; }
        public List<GameObject> GameModeSpawnedObjects { get; private set; }

        public EGameModeId Id => _id;
        public GameObject UiPrefab => _uiPrefab;
        public GameObject CameraPrefab => _cameraPrefab;
        public Actor PlayerPrefab => _playerPrefab;
        public PlayerController PlayerController => _playerController;

        [SerializeField] private EGameModeId _id = EGameModeId.None;
        [SerializeField] private GameObject _uiPrefab;
        [SerializeField] private GameObject _cameraPrefab;
        [SerializeField] private Actor _playerPrefab;
        [SerializeField] private PlayerController _playerController;

        private bool _isGameModeObjectListInitialized;


        public abstract IEnumerator OnEnd();
        public abstract IEnumerator OnStart();

        protected T SpawnGameModeObject<T>(T gameObject, string prefix = "", string postfix = "") where T : Object
        {
            return SpawnGameModeObject<T>(gameObject, gameObject.name, prefix, postfix);
        }

        protected T SpawnGameModeObject<T>(T gameObject, string objectName, string prefix = "", string postfix = "") where T: Object
        {
            var newGameObject = Instantiate(gameObject);

            newGameObject.name = $"{prefix}{objectName}{postfix}";
            
            RegisterGameModeObject(newGameObject);

            return newGameObject;
        }

        protected Vector3 GetRandomSpawnPointPosition() 
        {
            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>();

            if (spawnPoints.Length < 1)
                return Vector3.zero;
            
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            return spawnPoint.transform.position;
        }
        
        protected T RegisterGameModeObject<T>(T gameObject) where T: Object
        {
            if (!_isGameModeObjectListInitialized)
            {
                GameModeSpawnedObjects = new List<GameObject>();

                _isGameModeObjectListInitialized = true;
            }

            GameModeSpawnedObjects.Add(gameObject as GameObject);
            DontDestroyOnLoad(gameObject as GameObject);

            return gameObject;
        }

        protected IEnumerator DestroyAllGameModeObjects()
        {
            foreach (var obj in GameModeSpawnedObjects)
            {
                Destroy(obj as GameObject);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}