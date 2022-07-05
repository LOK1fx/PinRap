using System.Collections.Generic;
using UnityEngine;
using System;

namespace LOK1game
{
    public class MusicTimeline : MonoBehaviour
    {
        public bool IsPlaying { get; private set; }

        [SerializeField] private List<MusicNode> _musicNodes = new List<MusicNode>();
        [SerializeField] private float _secondError = 0.15f;

        private float _currentSecond = 0;
        private int _position;

        private void Update()
        {
            if (!IsPlaying) { return; }

            if (_position < _musicNodes.Count)
                TryBeatNextNode(OnBeatNode);
            else
                EndPlayback();

            _currentSecond += Time.deltaTime;
        }

        private void OnBeatNode(MusicNode node)
        {
            Debug.Log("On node!!!");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClientApp.ClientContext.BeatController.InstantiateBeat(node.Data.Strength);

                node.Played = true;

                _position++;
            }
        }

        [ContextMenu("StartPlayback")]
        public void StartPlayback(float second = 0)
        {
            IsPlaying = true;

            _currentSecond = second;
        }

        [ContextMenu("StopPlayback")]
        public void StopPlayback()
        {
            IsPlaying = false;

            foreach (var node in _musicNodes)
            {
                node.Played = false;
            }

            _currentSecond = 0;
            _position = 0;
        }

        public void EndPlayback()
        {
            IsPlaying = false;
        }

        public bool TryBeatNextNode(Action<MusicNode> callback)
        {
            if (IsNodeInRange(_musicNodes[_position]))
            {
                callback?.Invoke(_musicNodes[_position]);

                return true;
            }

            return false;
        }

        private bool IsNodeInRange(MusicNode node)
        {
            if (node.StartSecond >= _currentSecond && node.StartSecond <= _currentSecond + _secondError && !node.Played)
                return true;
            else
                return false;
        }
    }

    [System.Serializable]
    public class MusicNode
    {
        public float StartSecond;
        public MusicNodeData Data;
        public bool Played;
    }
}