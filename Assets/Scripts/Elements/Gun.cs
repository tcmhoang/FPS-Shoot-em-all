using UnityEngine;

namespace Assets.Scripts.Elements
{
    public class Gun : MonoBehaviour
    {
        public GameObject Bullet;

        public bool CanAutoFire;

        public float FireRate;

        public int CurrentAmmo;

        public int PickupAmount;

        public Transform FirePoint;

        [HideInInspector] public bool CanFireNow;
        private float _fireCounter;

        public float ZoomAmount;
        public string Name;

        // Start is called before the first frame update
        private void Start()
        {
            UpdateUi();
        }

        // Update is called once per frame
        private void Update()
        {
            CanFireNow = _fireCounter <= 0;
            if (!CanFireNow) _fireCounter -= Time.deltaTime;
        }


        public void Reload()
        {
            _fireCounter = FireRate;
        }

        public void RemoveABullet()
        {
            if (CurrentAmmo <= 0) return;
            CurrentAmmo--;
            UpdateUi();
        }

        public void UpdateUi()
        {
            UIController.Instance.AmmoText.text = $"AMMO: {CurrentAmmo}";
        }

        public bool HasAmmo()
        {
            return CurrentAmmo > 0;
        }

        public void GetAmmo()
        {
            CurrentAmmo += PickupAmount;
            UpdateUi();
        }
    }
}