using System;

namespace SudokuWebAPI
{
    public class Sudoku
    {
        public string board;

        public Sudoku(string board)
        {
            this.board = board;
        }

        static char[,] GenerateMultiDimBoard(char[] board)
        {
            char[,] newBoard = new char[9, 9];
            int rowCellCounter = 0;
            for (int i = 0; i < board.Length; i++)
            {
                if (rowCellCounter != 0 && rowCellCounter % 9 == 0)
                {
                    rowCellCounter = 0;
                }
                newBoard[i / 9, rowCellCounter] = board[i];
                rowCellCounter += 1;
            }
            return newBoard;
        }

        static Boolean CheckRow(char[,] board, char possibleSolution, int index)
        {
            for (int i = 0; i < 9; i++)
            {
                char tempChar = board[index, i];
                if (tempChar == possibleSolution)
                {
                    return true;
                }
            }
            return false;
        }

        static Boolean CheckCol(char[,] board, char possibleSolution, int index)
        {
            for (int i = 0; i < 9; i++)
            {
                char tempChar = board[i, index];
                if (tempChar == possibleSolution)
                {
                    return true;
                }
            }
            return false;
        }

        static Boolean CheckBox(char[,] board, char possibleSolution, int rowIndex, int colIndex)
        {
            int localRowIndex = (rowIndex / 3) * 3;
            int localColIndex = (colIndex / 3) * 3;
            for (int i = localRowIndex; i < localRowIndex + 3; i++)
            {
                for (int j = localColIndex; j < localColIndex + 3; j++)
                {
                    char tempChar = board[i, j];
                    if (tempChar == possibleSolution)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static char CheckSolution(char[,] board, int rowIndex, int colIndex)
        {
            var returnSolution = '0';
            var counter = 0;

            for (int i = 1; i < 10; i++)
            {
                char num = char.Parse(i.ToString());
                if (CheckRow(board, num, rowIndex) == false && CheckCol(board, num, colIndex) == false && CheckBox(board, num, rowIndex, colIndex) == false)
                {
                    returnSolution = num;
                    counter += 1;
                }
            }

            if (counter != 1)
            {
                returnSolution = '0';
            }

            return returnSolution;
        }

        static char[,] UpdateBoard(char[,] board, int rowIndex, int colIndex, char value)
        {
            board[rowIndex, colIndex] = value;
            return board;
        }

        public static string Solve(string boardString)
        {
            char[] board = boardString.ToCharArray();
            char[,] multiDimBoard = GenerateMultiDimBoard(board);
            var isSolved = false;
            string returnString = "";

            while (!isSolved)
            {
                //initialize counter of 0 cell in the board
                int zeroCounter = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {

                        //if 0 cell is detected, increase the counter and check through same row, column and 3x3 box
                        if (multiDimBoard[i, j] == '0')
                        {
                            zeroCounter += 1;
                            char num = CheckSolution(multiDimBoard, i, j);
                            if (num != '0')
                            {
                                //if number is confirmed, update the board and decrease the counter
                                multiDimBoard = UpdateBoard(multiDimBoard, i, j, num);
                                zeroCounter -= 1;
                            }
                        }
                    }
                }

                //if no more 0 cell, change the boolean value to end the while loop
                if (zeroCounter == 0)
                {
                    isSolved = !isSolved;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    returnString += multiDimBoard[i, j];
                }
            }

            return returnString;
        }
    }
}
