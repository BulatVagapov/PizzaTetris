using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ServerInteraction server;
    public int pizzaIndex;
    private bool paused;
    private Spawner groupSpawner;
    private Group group;
    private ItemSpawner itemSpawner;
    [SerializeField] FinalWinCanvasController winCanvasController;
    [SerializeField] ModalPartTwoController modalPartTwoController;
    private GamePartTwo gamePartTwo;
    private int gamePart;
    [SerializeField] GameObject pizzaBase;


    #region Canvases
    [Header("Canvases")]
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject PizzaChoseCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject infoCanvas;
    [SerializeField] GameObject rulesCanvas;
    [SerializeField] GameObject gamePartOneCanvas;
    [SerializeField] GameObject gamePartTwoCanvas;
    [SerializeField] GameObject modalCanvasPartOne;
    [SerializeField] GameObject modalCanvasPartTwo;
    #endregion




    /// <summary>
    /// Установка стартовых значений элементов игры
    /// </summary>
    public void SetStartState()
    {
        rulesCanvas.SetActive(true);
        gamePartOneCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        winCanvas.SetActive(false);
        modalCanvasPartOne.SetActive(false);
        modalCanvasPartTwo.SetActive(false);
        infoCanvas.SetActive(false);
        gamePartTwoCanvas.SetActive(false);
        pizzaBase.SetActive(false);
        groupSpawner.enabled = false;
        itemSpawner.enabled = false;
        gamePartTwo.enabled = false;
        paused = false;
        pizzaIndex = 0;
    }

    /// <summary>
    /// Начало первой части игры
    /// </summary>
    public void StartPartOne()
    {
        Playfield.ClearGrid();
        groupSpawner.enabled = true;
        group.enabled = true;
        group.acceleratedFall = false;
        groupSpawner.spawnNext();
        gamePart = 1;
    }

    /// <summary>
    /// Начало второй части игры
    /// </summary>
    public void StartPartTwo()
    {
        gamePartTwo.enabled = true;
        gamePartTwo.SelectionOfDownloadableItems(pizzaIndex);
        gamePartTwo.HideAndShowLoadedItems(true);
        gamePartTwo.RestartItemPlaces();
        gamePartTwo.ResetAttempt();
        itemSpawner.enabled = true;
        gamePart = 2;
        gamePartTwo.itemIndex = 0;
        Playfield.ClearGrid();
        pizzaBase.SetActive(true);
    }

    /// <summary>
    /// Перезапуск актуальной части игры из меню паузы и проигрыша
    /// </summary>
    public void RestartPartFromPauseAndLoseMenu()
    {
        if (gamePart == 1)
        {
            gamePartOneCanvas.SetActive(true);
            StartPartOne();
        }
        else if (gamePart == 2)
        {
            gamePartTwoCanvas.SetActive(true);
            StartPartTwo();
        }

        if (paused)
        {
            Pause();
        }
    }

    /// <summary>
    /// Завершенеие части первой проигрышом или выйгрышем
    /// </summary>
    /// <param name="win"></param>
    public void PartOneOver(bool win)
    {
        groupSpawner.enabled = false;
        gamePartOneCanvas.SetActive(false);
        Playfield.ClearGrid();

        if (win)
        {
            modalCanvasPartTwo.SetActive(true);
            modalPartTwoController.SetModalPartTwo(pizzaIndex);
        }
        else
        {
            loseCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Завершение части второй проигрышом или выйгрешем
    /// </summary>
    /// <param name="win"></param>
    public void PartTwoOver(bool win)
    {
        itemSpawner.enabled = false;
        gamePartTwo.HideAndShowLoadedItems(false);
        gamePartTwo.enabled = false;
        gamePartTwoCanvas.SetActive(false);

        if (win)
        {
            winCanvas.SetActive(true);
            StartCoroutine(server.SendMessageToServer());
            winCanvasController.SetWinCanvas(pizzaIndex);
        }
        else
        {
            loseCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// Пауза
    /// </summary>
    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        pauseCanvas.SetActive(!paused);
        paused = !paused;
    }


    private void Awake()
    {
        groupSpawner = FindObjectOfType<Spawner>();
        itemSpawner = FindObjectOfType<ItemSpawner>();
        group = GetComponent<Group>();
        server = GetComponent<ServerInteraction>();
        groupSpawner.enabled = false;
        itemSpawner.enabled = false;
        gamePartTwo = GetComponent<GamePartTwo>();
    }


    // Start is called before the first frame update
    void Start()
    {
        SetStartState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
}
