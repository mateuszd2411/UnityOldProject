using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage = 100f;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
