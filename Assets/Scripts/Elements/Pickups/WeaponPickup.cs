using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Elements.Pickups
{
    public class WeaponPickup : MonoBehaviour
    {
        public string GunName;
        private bool _isCollected;

        private void OnTriggerEnter(Collider other)
        {
            if (_isCollected || other.gameObject.tag != "Player") return;
            AudioManager.Instance.PlaySfx(SoundIndex.Gun);
            PlayerController.Instance.PickupGun(GunName);
            Destroy(gameObject);
            _isCollected = true;
        }
    }
}
