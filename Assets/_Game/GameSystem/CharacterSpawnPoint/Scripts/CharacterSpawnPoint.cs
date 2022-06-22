using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    public class CharacterSpawnPoint : MonoBehaviour
    {
        private List<Actor> _spawnedAtPoint = new List<Actor>();

        public Actor SpawnActor(Actor actor)
        {
            var newActor = Instantiate(actor, transform.position, transform.rotation);

            _spawnedAtPoint.Add(newActor);

            return newActor;
        }
    }
}