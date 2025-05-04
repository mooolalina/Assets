using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int _health_enemy; // Здоровье противника
    [SerializeField] private TextMeshProUGUI enemyhealthText; // Текстовое поле для отображения здоровья
    private GameObject[] friends; // Массив объектов типа Friend
    private GameObject player; // Объект игрока
    [SerializeField] private PlayerController playerController; // Ссылка на контроллер игрока
    private GameObject closestFriend; // Ближайший объект Friend
    public GameObject nearest; // Для удобства разработки
    [SerializeField] private float _enemySpeed; // Скорость движения противника

    private void Start()
    {
        // Установка стартового здоровья противника
        _health_enemy = Random.Range(1, 51); // Здоровье от 1 до 50
        enemyhealthText.text = _health_enemy.ToString(); // Отображение здоровья
        friends = GameObject.FindGameObjectsWithTag("Friend"); // Получение всех объектов Friend
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Получение объекта игрока
        int playerHealth = playerController.health; // Получение здоровья игрока

        // Движение к игроку, если здоровье противника больше здоровья игрока
        if (_health_enemy > playerHealth)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _enemySpeed * Time.deltaTime);
        }
        else
        {
            // Обновление массива друзей
            friends = GameObject.FindGameObjectsWithTag("Friend");
            nearest = FindClosestFriend(); // Поиск ближайшего объекта Friend
            if (nearest != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, nearest.transform.position, _enemySpeed * Time.deltaTime);
            }
        }
    }

    GameObject FindClosestFriend()
    {
        float distance = Mathf.Infinity; // Начальное значение для расстояния
        Vector3 position = transform.position; // Позиция противника
        foreach (GameObject go in friends)
        {
            Vector3 diff = go.transform.position - position; // Разница между позициями
            float curDistance = diff.sqrMagnitude; // Квадрат расстояния
            if (curDistance < distance)
            {
                closestFriend = go; // Запоминаем ближайшего друга
                distance = curDistance; // Обновляем минимальное расстояние
            }
        }
        return closestFriend; // Возвращаем ближайшего друга
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Friend")) // Проверка на столкновение с объектом Friend
        {
            Friend friend = collision.gameObject.GetComponent<Friend>(); // Получаем компонент Friend
            int healthFriend = friend._health; // Получаем здоровье друга
            _health_enemy += healthFriend; // Увеличиваем здоровье противника
            enemyhealthText.text = _health_enemy.ToString(); // Обновляем текст здоровья
            Destroy(collision.gameObject); // Уничтожаем объект Friend
        }
    }

    public bool IsAlive()
    {
        return _health_enemy > 0; // Проверка, жив ли противник
    }

    public void Hit(int damage)
    {
        _health_enemy -= damage; // Уменьшение здоровья противника
    }
}
