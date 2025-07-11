using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public bool CanResumeMovement = true;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    private EventBus _eventBus;
    private bool _isGameEnded;
    private bool _isDied;

    [Inject]
    private void Construct(Player player, EventBus eventBus)
    {
        _playerTransform = player.transform;
        _eventBus = eventBus;
    }
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<EnemyMesh>().Animator;

        _eventBus.OnGameEnded += EndGame;
    }

    private void Update()
    {
        FollowPlayer();

        _animator.SetBool("IsMoving", _navMeshAgent.velocity.magnitude > 0.05f);
    }

    private void FollowPlayer()
    {
        if (!_navMeshAgent.enabled || _isGameEnded || _isDied)
            return;

        _navMeshAgent.SetDestination(_playerTransform.position);

        if (!_navMeshAgent.isStopped)
            transform.LookAt(new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z));
    }
    private void EndGame()
    {
        _isGameEnded = true;
        StopMovement();
    }

    public void StopMovement()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
    }
    public void ResumeMovement()
    {
        if (!CanResumeMovement)
            return;
        
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }
    public void Die()
    {
        _isDied = true;
        StopMovement();
    }

    private void OnDisable()
    {
        _eventBus.OnGameEnded -= EndGame;
    }
}
