using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterAnimationsSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip _shootClip;
        [SerializeField] private AudioClip _bodyFallClip;
        
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void ShootAudio()
        {
            _source.PlayOneShot(_shootClip);
        }

        public void BodyAudio()
        {
            _source.PlayOneShot(_bodyFallClip);
        }
    }
}