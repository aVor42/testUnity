using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{

    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _timeToDamage = 5f;

    private float _damageTime;
    private bool _isDamage = true;

    private void Start()
    {
        _damageTime = _timeToDamage;
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _damageTime -= Time.deltaTime;
            if(_damageTime <= 0)
            {
                _isDamage = true;
                _damageTime = _timeToDamage;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    
        if( playerHealth != null && _isDamage)
        {
            playerHealth.ReduceHealth(_damage);
            _isDamage = false;
        }
    
    }
}
