using System.Collections;
using UnityEngine;

/// <summary>
/// Manage the inputs for a playable character.
/// </summary>
public class PlayerInputsManager
{
    /// <summary>
    /// Inputs of the player to get the keys/buttons that are pressed and run
    /// algorithm accordingly.
    /// </summary>
    private PlayerInputs m_playerInputs;

    /// <summary>
    /// Read value from the inputs for setting the movement of the player.
    /// </summary>
    private Vector2 m_inputReadMovement;

    /// <summary>
    /// Read flag from the inputs to know if the player should be forced
    /// walking or not.
    /// </summary>
    private bool m_inputReadWalking;

    
    /// <summary>
    /// Speed for smoothing movement over time.
    /// </summary>
    private float m_smoothInputSpeed = 0.2f;

    /// <summary>
    /// The current input vector value for the movement of the player in order
    /// to smooth it over time.
    /// </summary>
    private Vector2 m_currentInputVector;

    /// <summary>
    /// Smooth input velocity used by the Vector2.SmoothDamp() method.
    /// </summary>
    private Vector2 m_smoothInputVelocity;

    /// <summary>
    /// Creates a new instance of PlayerInputsManager.
    /// </summary>
    public PlayerInputsManager()
    {
        m_playerInputs = new PlayerInputs();

        m_playerInputs.CharacterControls.Move.performed +=
            ctx => m_inputReadMovement = ctx.ReadValue<Vector2>();

        m_playerInputs.CharacterControls.Move.canceled +=
            ctx => m_inputReadMovement = Vector2.zero;

        m_playerInputs.CharacterControls.Walk.performed +=
            ctx => m_inputReadWalking = ctx.ReadValueAsButton();
    }

    /// <summary>
    /// Gets a Vector3 for the movement of the player, smoothed over time
    /// according to the previous speed.
    /// </summary>
    /// <returns>A Vector3 for the movement of the player.</returns>
    public Vector3 MovementVector()
    {
        var movement = new Vector2(m_inputReadMovement.x, m_inputReadMovement.y);

        if (m_inputReadWalking)
        {
            movement = m_inputReadMovement / 4f;
        }

        // Smooth the values over time.
        m_currentInputVector = Vector2.SmoothDamp(
            m_currentInputVector,
            movement,
            ref m_smoothInputVelocity,
            m_smoothInputSpeed
        );

        return new Vector3(m_currentInputVector.x, 0f, m_currentInputVector.y);
    }

    /// <summary>
    /// Enables the inputs of the playable character.
    /// </summary>
    public void Enable()
    {
        m_playerInputs.CharacterControls.Enable();
    }

    /// <summary>
    /// Disables the inputs of the playable character.
    /// </summary>
    public void Disable()
    {
        m_playerInputs.CharacterControls.Disable();
    }
}