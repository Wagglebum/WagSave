using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float waypointReachedDistance = 0.1f;

    [Header("Boundary")]
    [SerializeField] private float boundaryRadius = 10f;
    [SerializeField] private Transform boundaryCenter;

    private Vector3 _origin;
    private Vector3 _targetPosition;

    private void Start()
    {
        _origin = boundaryCenter != null ? boundaryCenter.position : Vector3.zero;
        PickNewTarget();
    }

    private void Update()
    {
        MoveTowardTarget();

        if (Vector3.Distance(transform.position, _targetPosition) <= waypointReachedDistance)
            PickNewTarget();
    }

    private void PickNewTarget()
    {
        var randomPoint = Random.insideUnitCircle * boundaryRadius;
        _targetPosition = _origin + new Vector3(randomPoint.x, randomPoint.y, 0f);
    }

    private void MoveTowardTarget()
    {
        var direction = (_targetPosition - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ScoreManager.Instance.AddScore();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = boundaryCenter != null ? boundaryCenter.position : Vector3.zero;
        Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.3f);
        Gizmos.DrawSphere(center, boundaryRadius);
        Gizmos.color = new Color(1f, 0.2f, 0.2f, 1f);
        Gizmos.DrawWireSphere(center, boundaryRadius);
    }
}
