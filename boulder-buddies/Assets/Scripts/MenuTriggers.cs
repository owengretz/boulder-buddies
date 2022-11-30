using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTriggers : MonoBehaviour
{

    public enum Worlds { THEGRASSLANDS, THEVOLCANO};

    public Worlds trigger;


    private int playersIn = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersIn++;
        }

        if (playersIn == 2)
        {
            GameManager.instance.LoadMap(transform.position, trigger.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playersIn--;
        }
    }
}
