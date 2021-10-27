using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource _attackSound;

    private bool _isAtack;

    public bool IsAtack { get => _isAtack; }
    
    public void FinishAtack()
    {
        _isAtack = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAtack = true;
            animator.SetTrigger("atack");
            _attackSound.Play();
        }

    }

}
