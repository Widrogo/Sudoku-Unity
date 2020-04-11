using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    const int N = 9;
    public int[,] grid = new int[N, N];
    public Color FilledColor;
    public Color TextColor;

    private void Awake()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                grid[i, j] = 0;
            }
        }
    }
    public void Clear()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                grid[i, j] = 0;
            }
        }
        for (int x = 0; x < 81; x++)
        {
            gameObject.transform.GetChild(x).gameObject.GetComponent<InputField>().text = null;
        }
    }
    public void Solve()
    {
        int x = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                string str = gameObject.transform.GetChild(x).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text;
                if (str != string.Empty)
                {
                    ColorBlock cb = gameObject.transform.GetChild(x).gameObject.GetComponent<InputField>().colors;
                    cb.normalColor = FilledColor;
                    gameObject.transform.GetChild(x).gameObject.GetComponent<InputField>().colors = cb;
                    gameObject.transform.GetChild(x).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().color = TextColor;
                    grid[i, j] = int.Parse(str);
                }
                else
                {
                    grid[i, j] = 0;
                }
                x++;
            }
        }


        if (SolveSudoku(grid))
        {
            PrintGrid(ref grid);
        }
        else
        {
            Debug.Log("No Solution");
        }
    }

    public void PrintGrid(ref int[,] grid)
    {
        int[] tempGrid = new int[81];
        int x = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                //Debug.Log("Array[" + i + "]" + "[" + j + "]" + "value : " + grid[i, j]);
                tempGrid[x] = grid[i, j];
                x++;
            }
        }

        StartCoroutine(gameObject.GetComponent<LerpNumbers>().LerpOutput(tempGrid));
    }

    private bool FindUnassignedLocation(int[,] grid, ref int row, ref int col)
    {
        for (row = 0; row < N; row++)
        {
            for (col = 0; col < N; col++)
            {
                if (grid[row, col] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool UsedInRow(int[,] grid, int row, int num)
    {
        for (int col = 0; col < N; col++)
        {
            if (grid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    private bool UsedInCol(int[,] grid, int col, int num)
    {
        for (int row = 0; row < N; row++)
        {
            if (grid[row, col] == num)
            {
                return true;
            }
        }
        return false;
    }

    private bool UsedInBox(int[,] grid, int BoxStartRow, int BoxStartCol, int num)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (grid[row + BoxStartRow, col + BoxStartCol] == num)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsSafe(int[,] grid, int row, int col, int num)
    {
        return !UsedInRow(grid, row, num) && !UsedInCol(grid, col, num) && !UsedInBox(grid, row - row % 3, col - col % 3, num);
    }

    private bool SolveSudoku(int[,] grid)
    {
        int row = new int();
        int col = new int();
        if (!FindUnassignedLocation(grid, ref row, ref col))
        {
            return true;
        }
        for (int num = 1; num <= 9; num++)
        {
            if (IsSafe(grid, row, col, num))
            {
                grid[row, col] = num;
                if (SolveSudoku(grid))
                {
                    return true;
                }
                grid[row, col] = 0;
            }
        }
        return false;
    }
}
