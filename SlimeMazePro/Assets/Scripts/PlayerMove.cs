using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public Transform respawnPt;
    public GameObject joystick;

    private float speedMultiplier;
    private Animator anim;
    private CharacterController controller;
    private Vector3 lastPosition;
    private Vector3 playerVelocity;

    private Touch touch;

    public bool controls = true;

    private GameObject gm;
    private GameManager gameMngr;

    [SerializeField] private bl_Joystick Joystick;

    static PlayerMove instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();

        gm = GameObject.Find("GameManager");
        gameMngr = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        if (controls)
        {
            //Movement
            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

#if UNITY_ANDROID

            speedMultiplier = 0.2f;

            Vector3 Jmove = new Vector3(Joystick.Horizontal, 0, Joystick.Vertical);

            controller.Move(Jmove * Time.deltaTime * playerSpeed * speedMultiplier);

            if (Jmove != Vector3.zero)
            {
                gameObject.transform.forward = Jmove;
            }

            playerVelocity.y += -9.81f * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

#endif

#if UNITY_EDITOR

            joystick.SetActive(false);

            speedMultiplier = 1f;

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            controller.Move(move * Time.deltaTime * playerSpeed * speedMultiplier);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            playerVelocity.y += -9.81f * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

#endif

            // Speed is the distance between my current position - last position over time
            float speed = Vector3.Distance(lastPosition, transform.position) / Time.deltaTime;

            anim.SetFloat("Speed", speed);
            lastPosition = this.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("You died");
            anim.SetTrigger("Dead");
            gameMngr.currentLevelCoins = 0;
            gameMngr.LoseLive();
            Respawn();
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOverScene")
        {
            gameObject.SetActive(false);
        }

        else if (scene.name != "MainMenuScene")
        {
            joystick = GameObject.Find("Joystick");
            Joystick = FindObjectOfType<bl_Joystick>();
            respawnPt = GameObject.Find("RespawnPt").transform;
            Respawn();
        }
    }

    public void Respawn()
    {
        GameObject nl = GameObject.Find("WinArea");
        NextLevel level = nl.GetComponent<NextLevel>();
        for (int i = 0; i < level.coins.Count; i++)
        {
            level.coins[i].SetActive(true);
        }

        GetComponent<CharacterController>().enabled = false;
        gameObject.transform.position = respawnPt.position;
        GetComponent<CharacterController>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            controls = false;
            gameMngr.totalCoins += gameMngr.currentLevelCoins;
            gameMngr.currentLevelCoins = 0;
            Respawn();
        }
    }
}
