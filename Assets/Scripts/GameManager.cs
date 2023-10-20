using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
    public int punteo {get; private set;}
    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1);

    }



    public void GameOver()
    {
        // TODO: show game over screen
        SceneManager.LoadScene("GameOver");
        lives = 3;
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        Debug.Log("Mundo: " + world + " Nivel: " + stage);
        SceneManager.LoadScene($"{world}-{stage}");

    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
        Debug.Log("Vidas: " + lives);
        Debug.Log("Mundo: " + world + " Nivel: " + stage);
        if(world == 1 && stage == 2){
            punteo = 700;
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;
        Debug.Log("Vidas: " + lives);
        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;
        Debug.Log("Monedas: " + coins);
        punteo += 100;
        Debug.Log("Punteo: " + punteo);
        if (coins == 5)
        {
           
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
         Debug.Log("Se añadio Una Vida: " + lives);
         punteo += 2000;
         Debug.Log("Punteo: " + punteo);
         Debug.Log("Vidas: " + lives);
        lives++;
    }

    public void Starpower(){
        Debug.Log("Se activo la estrella");
        punteo += 1000;
        Debug.Log("Punteo: " + punteo);
    }

    public void Grow()
    {
        Player player = FindObjectOfType<Player>(); // Buscar una instancia de Player

        if (player != null && player.hongo)
        {
            punteo += 1000;
            Debug.Log("Se comió un Hongo: " + punteo);
        }
    }
 }