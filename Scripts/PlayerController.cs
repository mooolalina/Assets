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
        // Получаем направление движения на основе джойстика
        Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        // Если джойстик не в центре
        if (direction.magnitude > 0.1f)
        {
            // Добавляем силу к Rigidbody
            _rigidbody.AddForce(direction * _moveSpeed);

            // Поворачиваем объект в сторону движения
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
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
