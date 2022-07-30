using UnityEngine;

namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public bool IsLocal { get; private set; }

        public EPlayerType PlayerType => _playerType;

        [SerializeField] private EPlayerType _playerType;

        private Controller _controller;

        public void SetLocal(bool local)
        {
            IsLocal = local;
        }

        public Controller Controller => _controller;
        public abstract void OnInput(object sender);
        

        public virtual void OnPocces(Controller sender)
        {
            _controller = sender;
        }

        public virtual void OnUnpocces()
        {
            _controller = null;
        }
    }
}