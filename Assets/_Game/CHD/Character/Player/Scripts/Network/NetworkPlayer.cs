using UnityEngine;

namespace LOK1game.Networking
{
    public class NetworkPlayer : MonoBehaviour
    {
        private void FixedUpdate()
        {
            ClientSend.PlayerMovement(transform.position, transform.rotation);
        }
    }
}