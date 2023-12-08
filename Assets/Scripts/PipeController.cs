using System;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

class PipeController : MonoBehaviour
{
    List<GameObject> pipes;
    public float currentTimer;
    public float timeInterval;
    public static float pipeVelocity = 2.5f;
    public GameObject pipeObject;
    public float pipeInterval = 5f;
    public float spawnHeight = 2f;
    public float difficultyConstant = 0f;

    void Start()
    {
        pipes = new List<GameObject>();
        timeInterval = 3.75f;
        currentTimer = 0f;
    }

    void Update()
    {
        UpdatePipesLogic();
    }

    private void UpdatePipesLogic()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= timeInterval)
        {
            currentTimer = 0f;

            GameObject bottomPipe = CreatePipe(spawnHeight - pipeInterval / 2f, 0f);
            GameObject topPipe = CreatePipe(spawnHeight + pipeInterval / 2f, 180f);
            pipes.Add(bottomPipe);
            pipes.Add(topPipe);

            System.Random random = new System.Random();
            spawnHeight = random.Next(-4, 4);
            timeInterval -= 1f / 30f;
        }
    }

    public GameObject CreatePipe(float height, float angle)
    {
        GameObject pipe = new("Pipe", typeof(Pipe));
        Pipe pipeComponent = pipe.GetComponent<Pipe>();
        pipeComponent.pipeObject = pipeObject;

        pipeComponent.y = height;
        pipe.transform.eulerAngles = new Vector3(0f, 0f, angle);
        return pipe;
    }
}