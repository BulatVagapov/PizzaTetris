using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public float previosTime;
    public float fallTime = 0.5f;
    public bool acceleratedFall;
    private GameManager gameManager;
    private Transform currentGroup;
    [SerializeField] Spawner groupSpawner;


    /// <summary>
    /// Назначение текущей группы для управления ей
    /// </summary>
    /// <param name="group"></param>
    public void CurrentGroupAppoint(Transform group)
    {
        currentGroup = group;
    }

    /// <summary>
    /// проверка каждого блока в группе на соответствие позиции требованиям
    /// </summary>
    /// <returns></returns>
    bool isValidGridPos()
    {
        foreach (Transform child in currentGroup)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // Not inside Border?
            if (!Playfield.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != currentGroup)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Обновление сетки в случае если произошло смещение группы
    /// </summary>
    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == currentGroup)
                        Playfield.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in currentGroup)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    public void MovementToTheRight()
    {
        // Modify position
        currentGroup.position += new Vector3(1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            currentGroup.position += new Vector3(-1, 0, 0);
    }

    public void MovementToTheLeft()
    {
        // Modify position
        currentGroup.position += new Vector3(-1, 0, 0);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            currentGroup.position += new Vector3(1, 0, 0);
    }

    /// <summary>
    /// Вращение группы
    /// </summary>
    public void Rotation()
    {
        currentGroup.Rotate(0, 0, -90);

        // See if valid
        if (isValidGridPos())
            // It's valid. Update grid.
            updateGrid();
        else
            // It's not valid. revert.
            currentGroup.Rotate(0, 0, 90);
    }


    /// <summary>
    /// Проверка наличия свободного места под вновь созданной группой
    /// </summary>
    public void NoMorePlaysToGroup()
    {
        if (!isValidGridPos())
        {
            currentGroup = null;
            gameManager.PartOneOver(false);
            enabled = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        acceleratedFall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGroup != null)
        {
            //Включение и выключение форсированной скорости падения
            if (Input.GetKey(KeyCode.DownArrow))
            {
                acceleratedFall = true;
            }
            else if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                acceleratedFall = false;
            }

            // Move Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovementToTheLeft();
            }

            // Move Right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovementToTheRight();
            }

            // Rotate
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Rotation();
            }

            // Move Downwards and Fall
            else if (Time.time - previosTime > (acceleratedFall ? fallTime / 10 : fallTime))
            {
                // Modify position
                currentGroup.position += new Vector3(0, -1, 0);

                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();

                }
                else
                {
                    // It's not valid. revert.
                    currentGroup.position += new Vector3(0, 1, 0);

                    if (Playfield.GridIsFull())
                    {
                        //если поле заполнено - победа игрока
                        gameManager.PartOneOver(true);
                        Playfield.DeleteSuperfluous();
                        enabled = false;

                    }
                    else if (Playfield.HoleIsFound())
                    {
                        //если есть незаполненные области, которые нельзя заполнить - поражение
                        gameManager.PartOneOver(false);
                        enabled = false;
                    }
                    else
                    {
                        // Spawn next Group
                        groupSpawner.spawnNext();
                        NoMorePlaysToGroup();
                    }

                }

                previosTime = Time.time;
            }
        }

    }
}
