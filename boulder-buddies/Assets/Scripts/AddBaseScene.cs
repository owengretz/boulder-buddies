using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddBaseScene : MonoBehaviour
{



    private IEnumerator Start()
    {
        if (GameManager.instance == null)
        {
            var op = SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Additive);

            yield return new WaitUntil(() => op.isDone);
        }

        GameObject.Find("Cam").SetActive(false);
    }
}
