using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Elements.Pickups
{
    public class HealthPickup : MonoBehaviour
    {
        private bool _isCollected;

        public int HealAmount;

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected || other.gameObject.tag != "Player") return;
            AudioManager.Instance.PlaySfx(SoundIndex.Health);
            PlayerHealthController.Instance.Heal(HealAmount);
            _isCollected = true;
            Destroy(gameObject);
        }

    }
}
