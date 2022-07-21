using System;
using UnityEngine;
using System.Collections.Generic;

namespace LOK1game.UI
{
    [Obsolete]
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class Hitmarker : MonoBehaviour
    {
        private const string TRIGGER_ON_HIT = "Hit";
        private const string TRIGGER_ON_CRIT_HIT = "CritHit";

        [SerializeField] private float _maxRotationAngleOnHit = 5f;
        [SerializeField] private List<AudioClip> _clips = new List<AudioClip>();

        private Animator _animator;
        private AudioSource _source;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
        }
    }
}