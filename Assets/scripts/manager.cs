using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    [SerializeField][Tooltip("Total cows that we currently have")]
    private int totalCows;
    [SerializeField][Tooltip("Current wave")]
    int wave;

    void RemoveCow()
    {
        totalCows--;
        if (totalCows <= 0)
        {
            //End game
        }
    }

    private void Start()
    {
        //Start first wave after x seconds(for exploration)

    }

    private void StartNextWave()
    {
        wave++;
        //Spawn alien spaceships
    }
}
