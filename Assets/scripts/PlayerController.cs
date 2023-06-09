using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float minX = -10;
    private float maxX = 10;
    private float minY = -4;
    private float maxY = 6;
    private float minZ = -6;
    private float maxZ = 6;

    public bool gameOver;
    public Material mat;
    public int points = 0;
    public bool hasBeenClick;
    public int lives = 3;
    private AudioSource _audiosource;
    public AudioClip _audioclip;
    public TextMeshProUGUI textLives;
    public GameObject restartPanel;
    public TextMeshProUGUI textPoints;

    private void Start()
    {
        points = 0;
        hasBeenClick = false;
        lives = 3;
        textLives.text = $"Lives: 3";
        restartPanel.SetActive(false);
        textPoints.text = $"Points: 0";

        StartCoroutine("GenerateNextRandomPos");

        mat = GetComponent<MeshRenderer>().material;
    }

    

    private Vector3 GenerateRandomPos()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

        return pos;
    }

    private IEnumerator GenerateNextRandomPos()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(2);
            
            if (!hasBeenClick)
            {
                lives--;
                points -= 5;
                textLives.text = $"Lives: {lives}";
                textPoints.text = $"Points: {points}";


                if(lives == 0)
                {
                    Debug.Log($"Game Over");
                    gameOver = true;
                    mat.color = Color.red;
                    restartPanel.SetActive(true);
                    break;
                }
            }

            transform.position = GenerateRandomPos();
            mat.color = Color.blue;
            hasBeenClick = false;
        }
    }

    private void OnMouseDown()
    {
        if (!hasBeenClick)
        {
            mat.color = Color.green;
            points++;
            textPoints.text = $"Points: {points}";
            hasBeenClick = true;
            _audiosource.PlayOneShot(_audioclip, 1);
        }
    }
}
