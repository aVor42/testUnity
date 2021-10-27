using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AtackController _atackController;

    [SerializeField] private float _damage = 10f;

    private void Start()
    {
        _atackController = transform.root.GetComponent<AtackController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null && _atackController.IsAtack)
        {
            enemyHealth.ReduceHealth(_damage);
        }
    }

}
