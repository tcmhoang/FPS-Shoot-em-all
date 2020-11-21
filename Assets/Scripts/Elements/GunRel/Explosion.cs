using Assets.Scripts.Enemy;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Elements.GunRel
{
    public class Explosion : MonoBehaviour
    {
        private const int Damage = 25;

        public bool DamagePlayer, DamageEnemy;


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy" && DamageEnemy)
            {
                other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(Damage);
            }


            if (other.gameObject.tag == "Player" && DamagePlayer)
            {
                PlayerHealthController.Instance.Damage(Damage);
            }
        }
    }
}