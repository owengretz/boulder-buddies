using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public bool testingSpawn;
    private Transform spawnPos;

    public static GameManager instance;

    public TMP_Text timeText;
    private float speedrunTime;
    //public TMP_Text signTimeText;
    //public TMP_Text signDeathsText;
    public TMP_Text deathsText;

    private Vector3 respawnPos;
    private bool gameOver;
    [HideInInspector] public bool restarting;

    public PlayerMovement[] players;
    public GameObject rope;

    private int deaths;

    public Animator sceneTransitionAnim;


    public static event Action OnReset;
    public static void TriggerReset()
    {
        if (OnReset != null)
            OnReset();
    }

    private void Awake()
    {
        instance = this;

        GameObject spawn = GameObject.Find("Testing Spawn Pos");
        if (spawn != null) spawnPos = spawn.transform;
    }

    private void Start()
    {
        speedrunTime = 0f;
        deaths = 0;

        if (testingSpawn && spawnPos != null)
        {
            NewCheckpoint(spawnPos.position);
            foreach (PlayerMovement player in players) player.Respawn(spawnPos.position);
        }
        else if (deaths == 0)
        {
            Vector3 menuSpawnPos = new Vector3(-12f, -0.5f, 0f);
            foreach (PlayerMovement player in players) player.Respawn(menuSpawnPos);
            //NewCheckpoint(new Vector2(-16.5f, 13f));
        }
    }

    public void LoadMap(Vector3 triggerPos, string mapName)
    {
        StartCoroutine(LoadMapRoutine(triggerPos, mapName));
    }
    private IEnumerator LoadMapRoutine(Vector3 triggerPos, string mapName)
    {
        sceneTransitionAnim.SetTrigger("Fade In");

        yield return new WaitForSeconds(0.5f);

        Vector3 p1pos = triggerPos - GameObject.Find("Player 1").transform.position;
        Vector3 p2pos = triggerPos - GameObject.Find("Player 2").transform.position;
        Vector3 spawn = new Vector3(-15.5f, 23f, 0f);
        SpawnPlayersAtStart(spawn + p1pos, spawn + p2pos);

        SceneManager.UnloadSceneAsync("Menu");

        var op = SceneManager.LoadSceneAsync(mapName, LoadSceneMode.Additive);

        yield return new WaitUntil(() => op.isDone);

        CameraMovement.instance.SetPosition(-15.5f);

        yield return new WaitForSeconds(0.1f);

        sceneTransitionAnim.SetTrigger("Fade Out");
    }

    public void SpawnPlayersAtStart(Vector2 p1pos, Vector2 p2pos)
    {
        players[0].transform.position = p1pos;
        players[1].transform.position = p2pos;
    }

    public void NewCheckpoint(Vector3 position)
    {
        respawnPos = position;
    }

    public void PlayerDied()
    {
        if (restarting)
            return;

        restarting = true;

        foreach (PlayerMovement player in players)
        {
            Instantiate(player.deathParticlePrefab, player.transform.position, Quaternion.identity);
            player.GetComponent<SpriteRenderer>().enabled = false;
        }
        rope.SetActive(false);

        deaths++;
        deathsText.text = "Deaths: " + deaths;

        StartCoroutine(RespawnPlayers());
    }

    private IEnumerator RespawnPlayers()
    {

        yield return new WaitForSeconds(1f);

        TriggerReset();

        foreach (PlayerMovement player in players) player.GetComponent<SpriteRenderer>().enabled = true;

        if (respawnPos == Vector3.zero) SpawnPlayersAtStart(new Vector2(-16.5f, 16f), new Vector2(-16.5f, 18f));
        else foreach(PlayerMovement player in players) player.Respawn(respawnPos);

        rope.SetActive(true);

        restarting = false;
    }


    private void Update()
    {
        if (!gameOver)
        {
            speedrunTime += Time.deltaTime;
            string minutes = Mathf.Floor(speedrunTime / 60).ToString("00");
            string seconds = Mathf.Floor(speedrunTime % 60).ToString("00");
            timeText.text = minutes + ':' + seconds;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameOver)
            return;

        gameOver = true;
        string minutes = Mathf.Floor(speedrunTime / 60).ToString("00");
        string seconds = Mathf.Floor(speedrunTime % 60).ToString("00");
        timeText.text = minutes + ':' + seconds;
        //signTimeText.text = "YOUR TIME:\n" + minutes + ':' + seconds;

        //signDeathsText.text = "DEATHS:\n" + deaths.ToString();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
