using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public Transform cameraL2Pos;
    public GameObject player;
    public bool isGrounded;
    public bool canJump;
    public CapsuleCollider playerCollider;

    public JumpProjectile jumpProjectile;
    public UpdateStoneState updateStoneState;
    public UpdateStoneState updateStoneState_Bot;



    [Header("UI")]
    public GameObject levelFailedPanel;
    public GameObject mainMenu;
    public GameObject characterSelection;
    public GameObject levelCompletePanel;
    public GameObject gamePlayScreen;
    public GameObject pauseScreen;

    public Text mainMenuCoinText;
    public Text lvlCompCoinText;
    public Text lvlFailCoinText;
    public Text charSelectCoinTex;

    [Header("Character Selection Environment")]
    public GameObject charcterSelect_Env;

    [Header("Scripts Reference")]
    public CameraFollowScripts cameraFollow;
    public UpdateStoneState_L2 updateStoneState_L2;
    public UpdateStoneState_L2 updateStoneState_Bot_L2;
    public JumpProjectile Bot_AI;
    public CharacterSelection charSelect;
    public Score scoreScript;

    [Header("Game Objects")]
    public Transform[] level5_PlayerRotation;
    public Transform[] level4_PlayerRotation;
    public GameObject _camera;
    public GameObject _swipe;

    public bool lvl5Rot;
    public bool lvl4Rot;
    public bool fallCheckRight;
    public bool fallCheckMiddle;
    public bool fallCheckLeft;

    public GameObject[] levels;

    public GameObject menuEnvironment;
    public Transform menuPos;

    public GameObject soundBtnOn;
    public GameObject soundBtnOf;

    private void Awake()
    {
       
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            inst = null;
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("VibrateEnable", 1);

      //  PlayerPrefs.SetInt("CurrentLeve", 1);

        playerCollider = player.GetComponent<CapsuleCollider>();
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
           // Debug.Log("Restart Load");
            PlayerPrefs.SetInt("Restart", 0);
            
            menuEnvironment.SetActive(false);
            //  LevelSet(PlayerPrefs.GetInt("CurrentLeve"));
            LevelSet();

        }


        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioStop();
            soundBtnOn.SetActive(false);
            soundBtnOf.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Sound") == 0)
        {
            AudioResume();
            soundBtnOn.SetActive(true);
            soundBtnOf.SetActive(false);
        }
    }

    private void Update()
    {
       
    }

    public void Restart()
    {
        Time.timeScale = 1;
        levelFailedPanel.SetActive(false);
        PlayerPrefs.SetInt("Restart", 1);
        SceneManager.LoadScene("Gameplay");
        

    }

    public void SetCameraFollowTarget(Transform target)
    {
        cameraFollow.Target = target;
    }

    public void SetPlayerPositionAndRotation(Transform trans)
    {
        player.transform.SetPositionAndRotation(trans.position, trans.rotation);
    }

    public void SetPlayer(GameObject gameObject)
    {
        player = gameObject;
    }

    public void SetJumpProjectileTarget(Rigidbody rb)
    {
        jumpProjectile.ballBody = rb;
    }

    public void SetUpdateStoneState_Script(UpdateStoneState state)
    {
        updateStoneState = state;
    }

    public void SetLevel5_PlayerRotation()
    {
        lvl5Rot = true;
        lvl4Rot = false;
    }

    public void SetLevel4_PlayerRotation()
    {
        lvl4Rot = true;
    }

    public void SetCameraFollow_Lvl5_Script()
    {
        _camera.GetComponent<CameraFollowScripts>().enabled = false;
        _camera.GetComponent<CameraSmooth>().enabled = true;
    }

    public void SetJumpHieght(float i)
    {
        jumpProjectile.JumpValue = i;
    }

    public void Disable_Sides_Swipe()
    {
        _swipe.GetComponent<Swipe>().sidesControl = true;
    }

    public void SetBotAI(Rigidbody rb)
    {
        Bot_AI.ballBody = rb;
    }

    public void Set_UpdateStoneStateBot(UpdateStoneState up)
    {
        updateStoneState_Bot = up;
    }

    public void LevelSet()
    {
        // Debug.Log("Level Set");
        //if (PlayerPrefs.GetInt("Restart") == 0)
        //{
        //    menuEnvironment.SetActive(false);

        //    Restart();
        //}
        
            PlayerPrefs.SetInt("CurrentLeve", PlayerPrefs.GetInt("CurrentLeve"));
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].SetActive(false);
            }

            levels[PlayerPrefs.GetInt("CurrentLeve")].SetActive(true);
            mainMenu.SetActive(false);
        


    }

    public void SetActive_CharSelection()
    {
        characterSelection.SetActive(true);
        charcterSelect_Env.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SetDeActive_CharSelection()
    {
        characterSelection.SetActive(false);
        charcterSelect_Env.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        

        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
        SetCameraForMenu();
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        levelCompletePanel.SetActive(false);
        menuEnvironment.SetActive(true);
        mainMenu.SetActive(true);
        
    }

    public void NextLevel()
    {
      

        scoreScript.score = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levelCompletePanel.SetActive(false);

     int lvl =  PlayerPrefs.GetInt("CurrentLeve");

        if (lvl == 0)
        {
            
            PlayerPrefs.SetInt("CurrentLeve", 3);
            LevelSet();
        }
        else if(lvl == 3)
        {
            
            PlayerPrefs.SetInt("CurrentLeve", 4);
            LevelSet();
        }
        else if (lvl == 4)
        {
            
            PlayerPrefs.SetInt("CurrentLeve", 1);
            LevelSet();
        }
        else if (lvl == 1)
        {
           
            PlayerPrefs.SetInt("CurrentLeve", 2);
            LevelSet();
        }
    }

    public void LevelPrefSet()
    {
        int lvl = PlayerPrefs.GetInt("CurrentLeve");

        if (lvl == 0)
        {

            PlayerPrefs.SetInt("CurrentLeve", 3);
            
        }
        else if (lvl == 3)
        {

            PlayerPrefs.SetInt("CurrentLeve", 4);
            
        }
        else if (lvl == 4)
        {

            PlayerPrefs.SetInt("CurrentLeve", 1);
            
        }
        else if (lvl == 1)
        {

            PlayerPrefs.SetInt("CurrentLeve", 2);
            
        }
    }

    public void SetCameraForMenu()
    {
        _camera.transform.SetPositionAndRotation(menuPos.position, menuPos.rotation);
    }

    public void Pause()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioStop();
            
        }
        else if (PlayerPrefs.GetInt("Sound") == 0)
        {
            AudioResume();
            
        }

        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        gamePlayScreen.SetActive(false);
    }

    public void Resume()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioStop();
            
        }
        else if (PlayerPrefs.GetInt("Sound") == 0)
        {
            AudioResume();
            
        }

        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        gamePlayScreen.SetActive(true);
    }

    public void CoinSetMainMenu()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin"));
        mainMenuCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        
    }

    public void CoinSetComplete(int i)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + i);
        
        lvlCompCoinText.text = i.ToString();
        
    }

    public void CoinSetFail(int i)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + i);
        
        lvlFailCoinText.text = i.ToString();
    }

    public void CoinSetCharSelect()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin"));

        charSelectCoinTex.text = PlayerPrefs.GetInt("Coin").ToString();
    }


    //Sounds Set
    public void SetSoundOn()
    {
        
        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Sound", 0);
        }
       
    }

    public void SetSoundOff()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Sound", 1);
        }
        

    }

    public void AudioStop()
    {
        AudioListener.volume = 0;
    }

    public void AudioResume()
    {
        AudioListener.volume = 1;
    }

    public void GameComplete()
    {
        if (PlayerPrefs.GetInt("CurrentLeve") == 2)
        {
            PlayerPrefs.SetInt("CurrentLeve", 0);
           // PlayerPrefs.SetInt("GameComplete", 1);
        }
    }

    public void SetCamPosL2()
    {
        _camera.transform.SetPositionAndRotation(cameraL2Pos.position,cameraL2Pos.rotation);
    }
}
