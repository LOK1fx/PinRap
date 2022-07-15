using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class BeatRecorder : MonoBehaviour, IBeatReaction
    {
        [SerializeField] private GameObject _recordInidcator;
        [SerializeField] private MusicTimeline _timeline;

        private readonly List<RecordedNode> _recordedNodes = new List<RecordedNode>();
        
        private bool _isRecording;

        private void Awake()
        {
            _timeline.OnMusicStart += StartRecord;
            _timeline.OnMusicEnd += StopRecord;
            
            ClientApp.ClientContext.BeatController.RegisterActor(this);
        }
        
        public void OnBeat(EBeatEffectStrength strength)
        {
            var node = new RecordedNode()
            {
                Second = _timeline.GetCurrentSecond(),
                Strength = strength
            };
            
            _recordedNodes.Add(node);
        }

        private void StartRecord()
        {
            _isRecording = true;
    
            UpdateRecordIndicator();
        }

        private void StopRecord()
        {
            _isRecording = false;
            
            UpdateRecordIndicator();
        }

        private void UpdateRecordIndicator()
        {
            _recordInidcator.SetActive(_isRecording);
        }

        private void OnDestroy()
        {
            _timeline.OnMusicStart -= StartRecord;
            _timeline.OnMusicEnd -= StopRecord;
            
            ClientApp.ClientContext.BeatController.UnregisterActor(this);
        }
    }

    public struct RecordedNode
    {
        public float Second;
        public EBeatEffectStrength Strength;
    }
}