using UnityEngine;

namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public bool IsLocal { get; private set; }
        public Controller Controller { get; private set; }

        public EPlayerType PlayerType => _playerType;

        [SerializeField] private EPlayerType _playerType;

        public void SetLocal(bool local)
        {
            IsLocal = local;
        }

        public abstract void OnInput(object sender);
        

        public virtual void OnPocces(Controller sender)
        {
            Controller = sender;
        }

        public virtual void OnUnpocces()
        {
            Controller = null;
        }
    }
}