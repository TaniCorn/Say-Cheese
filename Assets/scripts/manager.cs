using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    [SerializeField][Tooltip("Total cows that we currently have")]
    private int totalCows;
    [SerializeField][Tooltip("Current wave")]
    int wave;

    [SerializeField] private Vector3[] roamPoints;
    [SerializeField] private GameObject spaceship;

    void RemoveCow()
    {
        totalCows--;
        if (totalCows <= 0)
        {
            //End game
            Debug.Log("<color=green>GAME OVER</color>");
        }
    }

    private void Start()
    {
        SpaceShip.SetRoamPoints(roamPoints);
        //Start first wave after x seconds(for exploration)


    }

    IEnumerator StartFirstWave()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        StartNextWave();
    }
    private void StartNextWave()
    {
        wave++;

        //Spawn alien spaceships
        GameObject sp = Instantiate(spaceship, new Vector3(0,0,0), Quaternion.identity, null);

    }

    
}
