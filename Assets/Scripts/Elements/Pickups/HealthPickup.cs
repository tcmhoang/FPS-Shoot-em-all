using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int HealAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        PlayerHealthController.Instance.Heal(HealAmount);
        Destroy(gameObject);
    }

}
