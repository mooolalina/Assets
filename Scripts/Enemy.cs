using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {
    public int _health_enemy; 
    [SerializeField] private TextMeshProUGUI enemyhealthText;
    private GameObject[] friends;
    private GameObject player; 
    [SerializeField] private PlayerController playerController; 
    private GameObject closestFriend; 
    public GameObject nearest; 
    [SerializeField] private float _enemySpeed; 

    private void Start() {
        _health_enemy = Random.Range(1, 51);
        enemyhealthText.text = _health_enemy.ToString(); 
        friends = GameObject.FindGameObjectsWithTag("Friend");
    }

    void Update() {
        player = GameObject.FindGameObjectWithTag("Player"); 
        int playerHealth = playerController.health; 

        if (_health_enemy > playerHealth) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _enemySpeed * Time.deltaTime);
        }
        else {
            friends = GameObject.FindGameObjectsWithTag("Friend");
            nearest = FindClosestFriend(); 
            if (nearest != null) {
                transform.position = Vector3.MoveTowards(transform.position, nearest.transform.position, _enemySpeed * Time.deltaTime);
            }
        }
    }

    GameObject FindClosestFriend() {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position; 
        foreach (GameObject go in friends) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude; 
            if (curDistance < distance) {
                closestFriend = go; 
                distance = curDistance; 
            }
        }
        return closestFriend; 
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Friend")) {
            Friend friend = collision.gameObject.GetComponent<Friend>(); 
            int healthFriend = friend._health; 
            _health_enemy += healthFriend; 
            enemyhealthText.text = _health_enemy.ToString(); 
            Destroy(collision.gameObject); 
        }
    }

    public bool IsAlive() {
        return _health_enemy > 0; 
    }

    public void Hit(int damage) {
        _health_enemy -= damage; 
    }

    public void Eat() {
        Destroy(gameObject);
    }
}
