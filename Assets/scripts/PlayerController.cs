using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private float minX = -10;
    private float maxX = 10;
    private float minY = -4;
    private float maxY = 6;
    private float minZ = -7;
    private float maxZ = 7;

    public bool gameOver;
    public Material mat;
    public int points = 0;
    public bool hasBeenClick;

    private void Start()
    {
        points = 0;
        hasBeenClick = false;

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
            hasBeenClick = true;
        }
    }
}
