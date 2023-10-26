using Assets.Scripts.AppStrings;
using UnityEngine;

/// <summary>
/// Dedicated class for animator management of a playable character.
/// </summary>
public class PlayerAnimationStateManager
{
    /// <summary>
    /// Animator containing the different animation states, their transitions
    /// and parameters.
    /// </summary>
    private Animator m_animator;

    /// <summary>
    /// ID reference to the animator velocityMagnitude parameter.
    /// </summary>
    private int m_velocityMagnitudeHash;

    /// <summary>
    /// Creates a new PlayerAnimationStateManager instance.
    /// </summary>
    /// <param name="animator">
    /// Animator component of the playable character game object.
    /// </param>
    public PlayerAnimationStateManager(Animator animator)
    {
        m_animator = animator;
        m_velocityMagnitudeHash = Animator.StringToHash(AnimationParameters.VelocityMagnitude);
    }

    /// <summary>
    /// Changes the movement velocity in the animator.
    /// </summary>
    /// <param name="velocity">Value of the velocity.</param>
    public void changeMovementVelocity(float velocity)
    {
        m_animator.SetFloat(m_velocityMagnitudeHash, velocity);
    }
}