using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class PlayerInputManager : MonoBehaviour, IPawn
    {
        public Controller Controller { get; private set; }
        
        [HideInInspector] public List<IPawn> PawnInputs = new List<IPawn>();
        [SerializeField] private List<MonoBehaviour> _actors = new List<MonoBehaviour>();

        private void Awake()
        {
            foreach (var actor in _actors)
            {
                if(actor.TryGetComponent<IPawn>(out var pawnInput))
                {
                    if(pawnInput is PlayerInputManager)
                    {
                        throw new System.Exception("PlayerInputManager can not call itself!");
                    }

                    PawnInputs.Add(pawnInput);

                    Debug.Log($"PlayerInput manager: {actor} added");
                }
                else
                {
                    Debug.LogError($"PlayerInput manager: {actor} doesn't have IPawnInput!");
                }
            }

            foreach (var pawn in PawnInputs)
            {
                Debug.Log(pawn);
            }
        }

        private void Update()
        {
            //Test temp solution
            OnInput(this);
        }
        

        public void OnInput(object sender)
        {
            var managerSender = new PlayerInputManagerSender(sender, this);

            foreach (var pawn in PawnInputs)
            {
                if (pawn is PlayerInputManager)
                {
                    Debug.LogWarning($"There are input manager: {pawn}");

                    continue;
                }

                pawn.OnInput(managerSender);
            }
        }

        public void OnPocces(Controller sender)
        {
            Controller = sender;
        }

        public void OnUnpocces()
        {
            Controller = null;
        }
    }

    public struct PlayerInputManagerSender
    {
        public object OriginSender;
        public object ActualSender;

        public PlayerInputManagerSender(object originSender, object actualSender)
        {
            OriginSender = originSender;
            ActualSender = actualSender;
        }
    }
}