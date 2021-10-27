using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speedX = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private AudioSource _jumpSound;

    private float _horizontal = 0f;
    private bool _isOnGround = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;
    private bool _isFinished = false;
    private bool _isLeverArm = false;

    private Rigidbody2D _rigitbody;
    private Finish _finish;
    private LeverArm _leverArm;

    private const float _speedXMultiplayer = 50f;

    private void Start(){
        _rigitbody = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").
            GetComponent<Finish>();
        _leverArm = FindObjectOfType<LeverArm>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _animator.SetFloat("SpeedX", Math.Abs(_horizontal));
        _isJump = Input.GetKey(KeyCode.W) && _isOnGround;
        if (Input.GetKeyDown(KeyCode.F) && _isFinished)
        {
            _finish.FinishLevel();
        }
        if(Input.GetKeyDown(KeyCode.F) && _isLeverArm)
        {
            _leverArm.ActivateLeverArm();
        }
    }

    private void FixedUpdate()
    {
        _rigitbody.velocity = new Vector2(
           _horizontal * _speedX * _speedXMultiplayer * Time.fixedDeltaTime, 
            _rigitbody.velocity.y);

        if (_isJump)
        {
            _jumpSound.Play();
            _rigitbody.AddForce(new Vector2(0f, 400f));
            _isOnGround = false;
            _isJump = false;
        }

        if((_horizontal > 0f && !_isFacingRight) || 
            (_horizontal < 0f && _isFacingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = _playerModel.localScale;
        playerScale.x *= -1;
        _playerModel.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
            _isOnGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isFinished = collision.tag == "Finish";
        _isLeverArm = collision.GetComponent<LeverArm>() != null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isFinished = !(collision.tag == "Finish");
        _isLeverArm = !(collision.GetComponent<LeverArm>() != null);
    }
}
