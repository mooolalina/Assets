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

    public GameObject WinPanel; 
    public GameObject LosePanel; 

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
        Vector3 direction = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            _rigidbody.AddForce(direction * _moveSpeed);

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
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                int healthEnemy = enemy._health_enemy;

                enemy.Hit(health);
                health -= healthEnemy;
                forceText.text = health.ToString();

                if (!enemy.IsAlive())
                {
                    Destroy(collision.gameObject); 
                    forceText.text = health.ToString();
                    Time.timeScale = 0; 
                    WinPanel.SetActive(true); 
                }
                else
                {
                    if (health <= 0)
                    {
                        Debug.Log("Игрок проиграл!");
                        Time.timeScale = 0; 
                        LosePanel.SetActive(true); 
                    }
                }
            }
        }
    }
}
