using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    /**Totorial Variables */
    internal static bool hasStarterArmor = false;
    internal static bool hasSword = false;
    internal static bool questTutorialNPCstarted = false;
    internal static bool questTutorialNPCfinished = false;
    internal static bool firstPersonCamera = false;

    /**Dungeon Variables */

    internal static bool hasShield = false;
    internal static bool hasWineglass = false;
    internal static bool hasBread = false;
    internal static bool interactedWithCross = false;
    internal static bool questCrossStarted = false;
    internal static bool questCrossFinished = false;
    internal static bool questBookStarted = false;
    internal static bool questBookFinished = false;
    internal static bool inputFieldActive = false;
    internal static bool hasPlateArmor  = false;
    internal static bool questStoneheedgeStarted = false;
    internal static bool questStoneheedgeFinished = false;
    internal static GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  
    }   

    // Update is called once per frame
    void Update()
    {   
        player = FindPlayer();

        if(player != null){
            CheckAlive();
        }

        EndGame(8);
        
        DestroyGameManager();
    }

    private void EndGame(float delay){
        if(questCrossFinished && questTutorialNPCfinished && questBookFinished && questStoneheedgeFinished){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player = null;
            ResetGameManager(); 
            TeleportPlayerAfterDelay("MainMenu", delay);
        }
    }

    private void EndGameWithoutCheck(float delay){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player = null;
        ResetGameManager(); 
        TeleportPlayerAfterDelay("MainMenu", delay);
    }

    public void TeleportPlayerAfterDelay(string sceneName, float delay)
    {
        StartCoroutine(TeleportAfterDelayCoroutine(sceneName, delay));
    }

    private IEnumerator TeleportAfterDelayCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene(sceneName); // Load the scene
    }

    private void ResetGameManager(){
        hasStarterArmor = false;
        hasShield = false;
        hasSword = false;
        hasWineglass = false;
        hasBread = false;
        interactedWithCross = false;
        questCrossStarted = false;
        questCrossFinished = false;
        questTutorialNPCstarted = false;
        questTutorialNPCfinished = false;
        questBookStarted = false;
        questBookFinished = false;
        inputFieldActive = false;
        hasPlateArmor  = false;
        firstPersonCamera = false;
        questStoneheedgeStarted = false;
        questStoneheedgeFinished = false;
    }

    private GameObject FindPlayer(){
        if(GameObject.Find("Player") != null && player == null){
            return GameObject.Find("Player");
        }
        return null;
    }
    
    private void CheckAlive(){
        if(player.GetComponent<PlayerFunctions>().GetPlayerHealth() <= 0){
            EndGameWithoutCheck(3);
        }
    }

    private void DestroyGameManager(){
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            Destroy(gameObject);
        }
    }
}
