using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[SerializeField] private float m_CrouchSpeed = .36f;
	[SerializeField] private float m_MovementFading = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_GroundLayer;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_CeilingCheck;
	[SerializeField] private Collider2D m_CrouchDisableCollider;
    private bool m_Grounded;
    public bool m_FacingRight = true;
    private bool m_wasCrouching = false;
	const float k_GroundedRadius = .2f;
	const float k_CeilingRadius = .2f;
	private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	public BoolEvent OnCrouchEvent;

	private void Awake() {

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null) {
			OnLandEvent = new UnityEvent();
        }

		if (OnCrouchEvent == null) {
			OnCrouchEvent = new BoolEvent();
        }
	}

	private void FixedUpdate() {

		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_GroundLayer);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) {
				m_Grounded = true;

				if (!wasGrounded) {
					OnLandEvent.Invoke();
                }
			}
		}
	}


	public void Move(float move, bool crouch, bool jump) {

		if (!crouch) {
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_GroundLayer)) {
				crouch = false;
			}
		}

		if (m_Grounded || m_AirControl) {
			if (crouch) {
				if (!m_wasCrouching) {
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				move *= m_CrouchSpeed;

				if (m_CrouchDisableCollider != null) {
					m_CrouchDisableCollider.enabled = false;
                }
        	}

            else {
				if (m_CrouchDisableCollider != null) {
					m_CrouchDisableCollider.enabled = true;
                }

				if (m_wasCrouching) {
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementFading);

			if (move > 0 && !m_FacingRight) {
				Flip();
			}

			else if (move < 0 && m_FacingRight) {
				Flip();
			}
		}
		if (m_Grounded && jump) {
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	public void Flip() {
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		// Debug Wrong Direction Bug
		/* if (m_FacingRight)
			Debug.Log("right " + m_FacingRight);
		else {
			Debug.Log("left " + !m_FacingRight);
		} */
	}
}