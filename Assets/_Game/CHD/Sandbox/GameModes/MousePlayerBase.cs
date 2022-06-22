using LOK1game.Player;
using UnityEngine;

namespace LOK1game.Sandbox
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerMovement))]
    public sealed class MousePlayerBase : Actor
    {
        private Rigidbody _rigidbody;
        private PlayerMovement _movement;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            _movement.SetAxisInput(input);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _movement.Jump();
            }
        }
    }
}