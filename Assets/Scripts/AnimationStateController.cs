using Assets.Scripts.AppStrings;
using UnityEngine;

/// <summary>
/// Script for controlling the animation states of a playable character.
/// </summary>
public class AnimationStateController : MonoBehaviour
{
    Animator m_animator;
    // ID references to the animator variables.
    int m_velocityMagnitudeHash;
    int m_velocityXHash;
    int m_velocityZHash;

    PlayerInputs m_playerInputs;
    Vector2 m_currentMovement;
    //bool m_walkPressed;

    // Called even before Start().
    void Awake()
    {
        m_playerInputs = new PlayerInputs();
        m_playerInputs.CharacterControls.Movement.performed += ctx =>
        {
            m_currentMovement = ctx.ReadValue<Vector2>();
        };
        //m_playerInputs.CharacterControls.Walk.performed += ctx => m_walkPressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update.
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_velocityMagnitudeHash = Animator.StringToHash(AnimationParameters.VelocityMagnitude);
        m_velocityXHash = Animator.StringToHash(AnimationParameters.VelocityX);
        m_velocityZHash = Animator.StringToHash(AnimationParameters.VelocityZ);
    }

    // Update is called once per frame.
    void Update()
    {
        handleMovements();
    }

    void handleMovements()
    {
        var currentMovement = m_currentMovement;

        var velocityVector = new Vector3(currentMovement.x, 0f, currentMovement.y);
        velocityVector.Normalize();
        var velocityMagnitude = velocityVector.magnitude;
        m_animator.SetFloat(m_velocityMagnitudeHash, velocityMagnitude);

        //if (m_walkPressed)
        //{
        //    currentMovement /= 2f;
        //}

        m_animator.SetFloat(m_velocityXHash, currentMovement.x);
        m_animator.SetFloat(m_velocityZHash, currentMovement.y);
    }

    void OnEnable()
    {
        m_playerInputs.CharacterControls.Enable();
    }

    void OnDisable()
    {
        m_playerInputs.CharacterControls.Disable();
    }
}
