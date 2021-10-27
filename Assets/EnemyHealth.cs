using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _totalHealth = 100f;
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;

    private float _health;

    private void Start()
    {
        _health = _totalHealth;
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        _slider.value = _health / _totalHealth;
        _animator.SetTrigger("takeDamage");
        Debug.Log(_health);
        if (_health <= 0)
            Die();

    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Die");
    }

}
