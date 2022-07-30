using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class BeatController
    {
        private readonly List<IBeatReaction> _reactors = new List<IBeatReaction>();

        public void RegisterActor(IBeatReaction actor)
        {
            _reactors.Add(actor);

            Debug.Log($"Beat controller: actor registered - {actor}");
        }

        public void UnregisterActor(IBeatReaction actor)
        {
            if(!_reactors.Contains(actor)) { return; }

            _reactors.Remove(actor);
            Debug.Log($"Beat controller: actor unregistered - {actor}");
        }

        public void InstantiateBeat(EBeatEffectStrength strength)
        {
            if(strength == EBeatEffectStrength.None) { return; }
            
            if(App.ProjectContext.GameStateManager.CurrentGameState != Game.EGameState.Paused)
            {
                foreach (var actor in _reactors)
                {
                    actor.OnBeat(strength);
                }
            }

            if(BeatEffectController.Instance != null)
                BeatEffectController.Instance.InstantiateBeat(strength);
        }
    }
}