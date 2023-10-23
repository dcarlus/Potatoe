using Assets.Scripts.AppStrings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for controlling the animation states of a playable character.
/// </summary>
public class AnimationStateController : MonoBehaviour
{
    Animator m_animator;
    float m_velocityXValue = 0f;
    float m_velocityZValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalSpeed = Input.GetAxis(Inputs.Horizontal);
        var verticalSpeed = Input.GetAxis(Inputs.Vertical);

        var velocityVector = new Vector3(horizontalSpeed, verticalSpeed, 0f);
        velocityVector.Normalize();
        var velocityMagnitude = velocityVector.magnitude;
        m_animator.SetFloat(AnimationParameters.VelocityMagnitude, velocityMagnitude);

        // In case of improvements, values are stored in class member variables.
        m_velocityXValue = horizontalSpeed;
        m_velocityZValue = verticalSpeed;

        m_animator.SetFloat(AnimationParameters.VelocityX, m_velocityXValue);
        m_animator.SetFloat(AnimationParameters.VelocityZ, m_velocityZValue);
    }
}
