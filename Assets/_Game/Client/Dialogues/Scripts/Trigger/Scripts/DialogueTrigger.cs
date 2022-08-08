using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    [RequireComponent(typeof(Collider))]
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private TextAsset _inkJsonAsset;

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            
        }
    }
}