using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(Player.Player))]
    public class PlayerArmsBobbing : Actor, IPawn
    {
        public Controller Controller { get; private set; }
        
        private Player.Player _player;

        [Header("Arms bobing")]
        [SerializeField] private float _armsOffsetOnActions = 0.05f;
        [SerializeField] private float _armsOffsetOnStartMovement = 0.05f;
        [SerializeField] private ArmsOffset _armsOffset;

        private void Start()
        {
            _player = GetComponent<Player.Player>();

            SubscribeToEvents();

            GetComponent<PlayerInputManager>().PawnInputs.Add(this);
        }

        private void OnEnable()
        {
            if(_player != null)
            {
                SubscribeToEvents();
            }
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                UnsubscribeFromEvents();
            }
        }

        public void OnInput(object sender)
        {
            if (_player.State.OnGround)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    AddArmsMovementOffset(Vector3.back);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    AddArmsMovementOffset(Vector3.right);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    AddArmsMovementOffset(Vector3.forward);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    AddArmsMovementOffset(Vector3.left);
                }
            }
        }

        private void AddArmsMovementOffset(Vector3 direction)
        {
            _armsOffset.AddTemp(direction * _armsOffsetOnStartMovement);
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnStartMovement);
        }

        private void OnJump()
        {
            _armsOffset.AddTemp(Vector3.up * _armsOffsetOnActions);
        }

        private void OnLand()
        {
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnActions);
        }

        private void OnStartCrouch()
        {
            _armsOffset.AddTemp(Vector3.down * _armsOffsetOnActions);
        }

        private void OnStopCrouch()
        {
            _armsOffset.AddTemp(Vector3.up * _armsOffsetOnActions);
        }

        protected override void SubscribeToEvents()
        {
            var playerMovement = _player.Movement;

            playerMovement.OnJump += OnJump;
            playerMovement.OnLand += OnLand;
            playerMovement.OnStartCrouch += OnStartCrouch;
            playerMovement.OnStopCrouch += OnStopCrouch;
        }

        protected override void UnsubscribeFromEvents()
        {
            var playerMovement = _player.Movement;

            playerMovement.OnJump -= OnJump;
            playerMovement.OnLand -= OnLand;
            playerMovement.OnStartCrouch -= OnStartCrouch;
            playerMovement.OnStopCrouch -= OnStopCrouch;
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
}