using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{

    [SerializeField][Tooltip("Total cows that we currently have")]
    private static int totalCows;
    private static int totalShips;
    [SerializeField][Tooltip("Current wave")]
    int wave = 0;
    float timeSurvived = 0;

    [SerializeField] private GameObject AlienRoamPointsObject;
    [SerializeField] private GameObject AlienSpawnPointsObject;
    [SerializeField] private Vector3[] alienRoamPoints;
    [SerializeField] private Vector3[] alienSpawnPoints;
    [SerializeField] private GameObject spaceship;

    public TMP_Text totalCowsText;
    public TMP_Text totalTimeSurvivedText;

    public GameObject gameOverPanel;

    public GameObject transitionPanel;
    public static void RemoveCow()
    {
        totalCows--;
        FindObjectOfType<manager>().totalCowsText.text = totalCows.ToString();
        if (totalCows <= 0)
        {
            //End game
            Debug.Log("<color=green>GAME OVER</color>");
            Cursor.lockState = CursorLockMode.Confined;

            GameObject panel = FindObjectOfType<manager>().gameOverPanel;
            panel.transform.localScale = Vector3.zero;
            panel.SetActive(true);
            LeanTween.scale(panel, new Vector3(1.3f, 1.3f, 1.3f), 0.4f);
            LeanTween.scale(panel, new Vector3(1, 1, 1), 0.1f).setDelay(0.4f);

            
        }
    }

    public void RemoveShip() {
    
        totalShips--;
        if (totalShips <= 0)
            StartCoroutine(StartWave());

    }

    private void FixedUpdate()
    {
        timeSurvived += Time.deltaTime;
        totalTimeSurvivedText.text = ((int)timeSurvived).ToString();
    }
    private void Start()
    {
        TransitionOut();

        alienRoamPoints = GetChildrenAsVectorPoints(AlienRoamPointsObject);
       alienSpawnPoints = GetChildrenAsVectorPoints(AlienSpawnPointsObject);
        SpaceShip.SetRoamPoints(alienRoamPoints);
        //Start first wave after x seconds(for exploration)
        StartCoroutine(StartWave());
        totalCows = FindObjectsOfType<Cow>().Length;
        totalCowsText.text = totalCows.ToString();
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
            int randomSpawn = Random.Range(0, alienSpawnPoints.Length);
            GameObject sp = Instantiate(spaceship, alienSpawnPoints[randomSpawn], spaceship.transform.rotation, null) ;
            sp.GetComponent<SpaceShip>().speed = (wave * 2) + 10;
        }
        
    }

    private int DecideNumberOfSpaceships()
    {
        if (wave >= 7)
            return totalCows;
        if (wave >= 5)
            return totalCows / 2;
        if (wave >= 3)
            return 2;
        return 1;
        //if (wave > 10)
        //{
        //    return totalCows;
        //}
        //if (wave < 2)
        //{
        //    return wave;
        //}
        //int spa = totalCows - totalCows / wave;
        //if (spa < 0)
        //{
        //    return 1;
        //}
        //return spa;
    }

    private Vector3[] GetChildrenAsVectorPoints(GameObject go)
    {
        Transform[] f = go.GetComponentsInChildren<Transform>();
        Vector3[] roamPoints = new Vector3[f.Length];
        for (int i = 0; i < f.Length; i++)
        {
            roamPoints[i] = f[i].position;
        }
        return roamPoints;
    }

    public void Retry()
    {
        TransitionIn();
        LeanTween.delayedCall(1.0f, () => { SceneManager.LoadScene("Level"); });
        
    }
    public void MainMenu()
    {
        Debug.LogError("<color=Red>Go to manager.cs, Function MainMenu() to add the main menu scene name</color>");
        TransitionIn();
        LeanTween.delayedCall(1.0f, () => { SceneManager.LoadScene("Main Menu"); });

    }

    public void TransitionIn()
    {
        transitionPanel.transform.position = new Vector3(-Screen.width, Screen.height / 2);
        LeanTween.move(transitionPanel, new Vector2(Screen.width / 2, Screen.height / 2), 1.0f);
    }
    public void TransitionOut()
    {
        transitionPanel.transform.position = new Vector3(Screen.width/2, Screen.height / 2);
        LeanTween.move(transitionPanel, new Vector2(-Screen.width, Screen.height / 2), 1.0f);
    }
}
