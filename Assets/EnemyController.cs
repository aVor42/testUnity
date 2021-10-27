using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _walkDistance = 6f;
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _timeToWait = 5f;
    [SerializeField] private Transform enemyModelTransform;

    private Rigidbody2D _rb;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Transform _playerTransform;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private float _waitTime;
    private bool _isChasingPlayer = false;

    public bool IsFacingRight
    {
        get => _isFacingRight;
    }

    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * _walkDistance;
        _waitTime = _timeToWait;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {

        if (_isWait)
            Wait();
        
        _isWait = ShouldWait();
    }

    private void Wait()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = _timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private bool ShouldWait()
    {
        bool isOutOfRightBoundary = _isFacingRight &&
            transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight &&
            transform.position.x <= _leftBoundaryPosition.x;

        return isOutOfRightBoundary || isOutOfLeftBoundary;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }

    private void FixedUpdate()
    {
        Vector2 nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChasingPlayer)
        {
            ChasePlayer(nextPoint);
        } 
        else if (!_isWait )
        {
            Patrol(nextPoint);
        }
    }

    private void Patrol(Vector2 nextPoint)
    {
        if (!_isFacingRight)
            nextPoint.x *= -1;
        _rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void ChasePlayer(Vector2 nextPoint)
    {
        float distance = _playerTransform.position.x - transform.position.x;
        if(distance < 0)
        {
            nextPoint *= -1;

            if (_isFacingRight)
                Flip();
        }else if(distance > 0 && !_isFacingRight)
        {
            Flip();
        }

        _rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransform.localScale;
        playerScale.x *= -1;
        enemyModelTransform.localScale = playerScale;
    }

}
