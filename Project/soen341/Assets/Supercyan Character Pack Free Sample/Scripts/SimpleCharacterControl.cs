﻿using UnityEngine;
using System.Collections.Generic;

public class SimpleCharacterControl : MonoBehaviour {

	// m_animator.SetTrigger("Jump");
	// m_animator.SetTrigger("Land");

	private enum ControlMode
	{
		Tank
	}

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
	[SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    private ControlMode m_controlMode = ControlMode.Tank;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }
    
	void Update () {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch(m_controlMode)
        {
            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
        m_wasGrounded = m_isGrounded;
    }

    public static char[] actionSequence = new char[100];
    public static int actionPointer = 0;

	public void right()
    {
        print("dickbutt right, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'r';
        actionPointer++;
	}

    public void forward()
    {
        print("dickbutt forward, pointer: " + actionPointer);
        actionSequence[actionPointer] = 'f';
        actionPointer++;
    }

    public void runCode()
    {
        StartCoroutine(runActions(1));
    }

    float v = 0;
    float h = 0;

    public System.Collections.IEnumerator runActions(int Whatever)
    {
        Vector3 initialPosition = transform.position;
        Vector3 initialRotation = transform.eulerAngles;
        for (int action = 0; action < actionPointer; action++)
        {
            Vector3 positionBeforeAction = transform.position;
            Vector3 rotationBeforeAction = transform.eulerAngles;
            switch (actionSequence[action])
            {
                case 'f':
                    while (System.Math.Abs(positionBeforeAction.x - transform.position.x) < 0.9889f && System.Math.Abs(positionBeforeAction.z - transform.position.z) < 0.9889f)
                    {
                        v = 1f;
                        yield return new WaitForSeconds(0);
                    }
                    print("position - x: " + (float)(System.Math.Round(transform.position.x, 0) + 0.3) + ", y: " + (float)(System.Math.Round(transform.position.z, 0) + 0.3));
                    transform.position = new Vector3((float)(System.Math.Round(transform.position.x, 0)+0.3), 0, (float)(System.Math.Round(transform.position.z, 0)+0.3));
                    break;
                case 'r':
                    while (System.Math.Abs(transform.eulerAngles.y - rotationBeforeAction.y) < 89)
                    {
                        h = 1.0f;
                        yield return new WaitForSeconds(0);
                    }
                    print("angle: " + (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100));
                    transform.eulerAngles = new Vector3(0, (float)(System.Math.Round(transform.eulerAngles.y / 100, 1) * 100), 0);
                    break;
                default:
                    h = 0.0f;
                    v = 0.0f;
                    break;
            }
            v = 0.0f;
            h = 0.0f;
        }
        print("end of actions");
        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;
    }

    private void TankUpdate()
    {   
        bool walk = true;

        if (v < 0) {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if(walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}