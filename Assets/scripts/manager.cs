using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{

    [SerializeField][Tooltip("Total cows that we currently have")]
    private static int totalCows;
    private static int totalShips;
    [SerializeField][Tooltip("Current wave")]
    int wave = 0;


    [SerializeField] private GameObject AlienRoamPointsObject;
    [SerializeField] private Vector3[] alienRoamPoints;
    [SerializeField] private GameObject spaceship;

    public static void RemoveCow()
    {
        totalCows--;
        if (totalCows <= 0)
        {
            //End game
            Debug.Log("<color=green>GAME OVER</color>");
        }
    }
    public void RemoveShip() {
    
        totalShips--;
        if (totalShips <= 0)
            StartCoroutine(StartWave());

    }

    private void Start()
    {
       alienRoamPoints = GetChildrenAsRoamPoints(AlienRoamPointsObject);
        SpaceShip.SetRoamPoints(alienRoamPoints);
        //Start first wave after x seconds(for exploration)
        StartCoroutine(StartWave());
        totalCows = FindObjectsOfType<Cow>().Length;
    }

    private IEnumerator StartWave()
    {
        Debug.Log("<color=green>10 Seconds till next wave</color>");
        yield return new WaitForSecondsRealtime(10.0f);
        StartNextWave();
    }
    private void StartNextWave()
    {
        wave++;
        Debug.Log("<color=green>It is wave: </color>" + wave);

        int amountOfSpaceships = DecideNumberOfSpaceships();
        totalShips = amountOfSpaceships;
        for (int i = 0; i < amountOfSpaceships; i++)
        {
            GameObject sp = Instantiate(spaceship, new Vector3(i * 30, 0, 0), spaceship.transform.rotation, null) ;
            sp.GetComponent<SpaceShip>().speed = (wave * 2) + 10;
        }
        //Spawn alien spaceships

    }

    private int DecideNumberOfSpaceships()
    {
        if (wave > 10)
        {
            return totalCows;
        }
        if (wave < 2)
        {
            return wave;
        }
        int spa = totalCows - totalCows / wave;
        if (spa < 0)
        {
            return 1;
        }
        return spa;
    }

    private Vector3[] GetChildrenAsRoamPoints(GameObject go)
    {
        Transform[] f = go.GetComponentsInChildren<Transform>();
        Vector3[] roamPoints = new Vector3[f.Length];
        for (int i = 0; i < f.Length; i++)
        {
            roamPoints[i] = f[i].position;
        }
        return roamPoints;
    }


}
