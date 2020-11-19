using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Elements.Pickups
{
    public class AmmoPickup : MonoBehaviour
    {
        private bool _collected;

        private void OnTriggerEnter(Collider other)
        {
            if (_collected || other.gameObject.tag != "Player") return;
            PlayerController.Instance.ActiveGun.GetAmmo();   
            _collected = true;
            Destroy(gameObject);
        }
    }
}
