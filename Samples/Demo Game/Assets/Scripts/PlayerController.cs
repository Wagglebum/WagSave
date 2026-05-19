using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float inputSmoothing = 3f;

    [Header("Boundary")]
    [SerializeField] private float boundaryRadius = 10f;
    [SerializeField] private Transform boundaryCenter;

    private Vector3 _origin;
    private Vector2 _smoothedInput;
    private bool _isPaused;

    private void Start()
    {
        _origin = boundaryCenter != null ? boundaryCenter.position : Vector3.zero;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            SetPaused(!_isPaused);

        if (!_isPaused)
            HandleMovement();
    }

    private void SetPaused(bool paused)
    {
        _isPaused = paused;
        Time.timeScale = paused ? 0f : 1f;
        ScoreManager.Instance?.SetStatus(paused ? "Paused" : "");
    }

    private void HandleMovement()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        var rawInput = new Vector2(
            (kb.dKey.isPressed ? 1f : 0f) - (kb.aKey.isPressed ? 1f : 0f),
            (kb.wKey.isPressed ? 1f : 0f) - (kb.sKey.isPressed ? 1f : 0f)
        );

        _smoothedInput = Vector2.MoveTowards(_smoothedInput, rawInput, inputSmoothing * Time.deltaTime);

        var direction = new Vector3(_smoothedInput.x, _smoothedInput.y, 0f).normalized;
        var move = direction * (moveSpeed * Time.deltaTime);
        var newPosition = transform.position + move;

        // Clamp to boundary radius
        var offset = newPosition - _origin;
        if (offset.magnitude > boundaryRadius)
            newPosition = _origin + offset.normalized * boundaryRadius;

        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = boundaryCenter != null ? boundaryCenter.position : Vector3.zero;
        Gizmos.color = new Color(0f, 1f, 0.5f, 0.3f);
        Gizmos.DrawSphere(center, boundaryRadius);
        Gizmos.color = new Color(0f, 1f, 0.5f, 1f);
        Gizmos.DrawWireSphere(center, boundaryRadius);
    }
}