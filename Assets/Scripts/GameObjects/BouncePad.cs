using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class BouncePad : MonoBehaviour
    {
        public float BoundForce;

        public void OnTriggerEnter(Collider other)
        {
            if(other.tag != "Player") return;
            AudioManager.Instance.PlaySfx(SoundIndex.BoundPad);
            PlayerController.Instance.Bounce(BoundForce);
        }
    }
}
