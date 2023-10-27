using UnityEngine;

/// <summary>
/// Script for controlling the animation states of a playable character.
/// </summary>
public class PlayerAnimationStateController : MonoBehaviour
{
    /// <summary>
    /// Animator management of the playable character.
    /// </summary>
    private PlayerAnimationStateManager m_animManager;

    /// <summary>
    /// Inputs management of the playable character.
    /// </summary>
    private PlayerInputsManager m_inputsManager;

    //private Vector3 m_oldVelocityVector;

    // Called even before Start().
    void Awake()
    {
        m_inputsManager = new PlayerInputsManager();
    }

    // Start is called before the first frame update.
    void Start()
    {
        var animator = GetComponent<Animator>();
        m_animManager = new PlayerAnimationStateManager(animator);
    }

    // Update is called once per frame.
    void Update()
    {
        var velocityVector = m_inputsManager.MovementVector();
        handleDisplacement(velocityVector);
        handleRotation(velocityVector);
        //m_oldVelocityVector = velocityVector;

        m_animManager.setCrouchingFlag(m_inputsManager.isCrouching());
    }

    /// <summary>
    /// Handle the displacements of the character.
    /// </summary>
    /// <param name="velocityVector">Velocity vector from inputs.</param>
    void handleDisplacement(Vector3 velocityVector)
    {
        var velocityMagnitude = velocityVector.magnitude;
        m_animManager.changeMovementVelocity(velocityMagnitude);
    }

    /// <summary>
    /// Handle the rotations of the character.
    /// </summary>
    /// <param name="velocityVector">Velocity vector from inputs.</param>
    void handleRotation(Vector3 velocityVector)
    {
        var currentPosition = transform.position;
        var positionLookAt = currentPosition + velocityVector;
        transform.LookAt(positionLookAt);

        // UNDER TESTING!
        //var isMoving = velocityVector.magnitude > 0.01f;
        //var dotVelocityVectors = Vector3.Dot(velocityVector, m_oldVelocityVector);
        //var isDirectionChanged = dotVelocityVectors < 0.1f;

        //if (isMoving && isDirectionChanged)
        //{
        //    Debug.Log("Direction changed to opposite?!");
        //}
    }

    void OnEnable()
    {
        m_inputsManager.Enable();
    }

    void OnDisable()
    {
        m_inputsManager.Disable();
    }
}
