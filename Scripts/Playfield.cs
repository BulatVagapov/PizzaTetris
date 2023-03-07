using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    //перменные для сетки
    public static int w = 8;
    public static int h = 25;
    public static Transform[,] grid = new Transform[w, h];



    /// <summary>
    /// Метод округдения позиции до целых чисел
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
                           Mathf.Round(v.y));
    }

    /// <summary>
    /// проверка на нахождние внутри игрового поля
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0);
    }

    /// <summary>
    /// зачистка поля (перед началом новой игры)
    /// </summary>
    public static void ClearGrid()
    {
        //for (int x = 0; x < w; x++)
        //{
        //    for (int y = 0; y < h; y++)
        //    {
        //        if (grid[x, y] != null)
        //        {
        //            Destroy(grid[x, y].gameObject);
        //            grid[x, y] = null;
        //        }

        //    }
        //}
        GameObject[] groups = GameObject.FindGameObjectsWithTag("Group");

        foreach(GameObject g in groups)
        {
            Destroy(g);
        }


    }

    /// <summary>
    /// удаление лишних блоков после заполнения корЖа и победы в первой части игры
    /// </summary>
    public static void DeleteSuperfluous()
    {
        for(int x = 0; x < w; x++)
        {
            for (int y = 6; y < h; y++)
            {
                if(grid[x,y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                }

            }
        }
    }

   
    /// <summary>
    /// сообщает когда поле заполнено полностью
    /// </summary>
    /// <returns></returns>
    public static bool GridIsFull()
    {
        bool full = true;
        for (int x = 0; x < w; x++)
        {
            for(int y = 0; y < 6; y++)
            {
                if(grid[x, y] == null)
                {
                    full = false;
                }
            }
        }

        return full;
    }

    /// <summary>
    /// метод необходим для опредлениея незаполненных оластей. представляет соседние строки в виде простых числе и вычисляет сумму или разность в зависимости от условия
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    public static int[] FindDifference(int y)
    {
        int[] difference = new int[w];
        int current;
        int above;

        for(int x = 0; x < w; x++)
        {
            if(grid[x, y] != null)
            {
                current = 1;
            }
            else
            {
                current = 0;
            }

            if(grid[x, y + 1] != null)
            {
                above = 1;
            }
            else
            {
                above = 0;
            }

            if(current == above)
            {
                difference[x] = current + above;
            }
            else
            {
                difference[x] = above - current;
            }
        }
        return difference;
    }

    /// <summary>
    /// метод определяет где в рядах имеются заполненные области
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static bool LookingForAHole(int[] difference)
    {
        bool hole = false;

        for(int i = 0; i < difference.Length; i++)
        {
            if(difference[i] == 1)
            {
                if(i > 1 && i < difference.Length - 2)
                {
                    if((difference[i - 1] == 2 || difference[i - 1] == -1 || difference[i - 2] == 2 || difference[i - 2] == -1) && (difference[i + 1] == 2 || difference[i + 1] == -1 || difference[i + 2] == 2 || difference[i + 2] == -1))
                    {
                        hole = true;
                        break;
                    }
                }else if(i == 1)
                {
                    if ((difference[i - 1] == 2 || difference[i - 1] == -1) && (difference[i + 1] == 2 || difference[i + 1] == -1 || difference[i + 2] == 2 || difference[i + 2] == -1))
                    {
                        hole = true;
                        break;
                    }
                }else if(i == 0)
                {
                    if (difference[i + 1] == 2 || difference[i + 1] == -1 || difference[i + 2] == 2 || difference[i + 2] == -1)
                    {
                        hole = true;
                        break;
                    }
                }else if(i == difference.Length - 2)
                {
                    if ((difference[i - 1] == 2 || difference[i - 1] == -1 || difference[i - 2] == 2 || difference[i - 2] == -1) && (difference[i + 1] == 2 || difference[i + 1] == -1))
                    {
                        hole = true;
                        break;
                    }
                }else if(i == difference.Length - 1)
                {
                    if (difference[i - 1] == 2 || difference[i - 1] == -1 || difference[i - 2] == 2 || difference[i - 2] == -1)
                    {
                        hole = true;
                        break;
                    }
                }
            }
        }




        return hole;
    }

    /// <summary>
    /// определет, что есть незаполненные области, которые не возможно запонить
    /// </summary>
    /// <returns></returns>
    public static bool HoleIsFound()
    {
        bool holeIsFound = false;
        for(int y = 0; y < h - 1; y++)
        {
            holeIsFound = LookingForAHole(FindDifference(y));
            if (holeIsFound)
            {
                break;
            }
        }
        return holeIsFound;
    }
}
