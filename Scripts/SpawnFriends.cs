using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SpawnFriends : MonoBehaviour
{
    [SerializeField] private GameObject _friendPrefab;
    [SerializeField] private GameObject LosePanel; 
    [SerializeField] private GameObject WinPanel; 

    void Start()
    {
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        Time.timeScale = 1; 

        for (int i = 0; i < 50; i++)
        {
            float x = Random.Range(50f, 830f); 
            float z = Random.Range(80f, 730f); 
            Instantiate(_friendPrefab, new Vector3(x, 30f, z), Quaternion.identity);
        }
    }

    public void ShowLosePanel()
    {
        LosePanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void ReloadScene()
    {
         Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
