using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private float _health = 100f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Slider _slider;

    public void ReduceHealth(float damage)
    {
        _hitSound.Play();
        _health -= damage;
        _slider.value = _health / 100;
        _animator.SetTrigger("takeDamage");
        Debug.Log(_health);
        if (_health <= 0)
            Die();

    }

    private void Die()
    {
        gameObject.SetActive(false);
        _gameOverCanvas.SetActive(true);
        Debug.Log("Die");
    }
}
