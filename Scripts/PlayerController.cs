using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private TextMeshProUGUI forceText; 
    public int health = 0; 

    private void Start()
    {
        if (forceText == null)
        {
            Debug.LogError("ForceText не присоединен в инспекторе!");
        }
        else
        {
            forceText.text = health.ToString();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.linearVelocity.y, _joystick.Vertical * _moveSpeed);
        
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friend"))
        {
            Friend friend = collision.gameObject.GetComponent<Friend>();
            if (friend != null)
            {
                int healthFriend = friend._health; 
                health += healthFriend;

                forceText.text = health.ToString(); 
                Destroy(collision.gameObject); 
            }
        }
    }
}
