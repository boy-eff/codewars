namespace Solution {
  using System;
  public class BattleshipField {
    public static bool ValidateBattlefield(int[,] field) {
      int i = 1;
            int j = 1;
            bool isCorrect = true;
            bool moreThenOneCell = false;
            bool isHorizontal = false;
            int[] ships = new int[4];
            int[,] extendedArr = new int[12, 12];
            for (int innerI = 1;innerI < 11; innerI++)
            {
                for (int innerJ = 1; innerJ < 11; innerJ++)
                {
                    extendedArr[innerI, innerJ] = field[innerI - 1, innerJ - 1];
                }
            }
            


            int k, m;
            for (i = 1; i < 11; i++)
            {
                for (j = 1; j < 11; j++)
                {
                    if (extendedArr[i,j] == 1)
                    {
                        k = i;
                        m = j;
                        if (extendedArr[i, j + 1] == 1)
                        {
                            isHorizontal = true;

                        }
                        else if (extendedArr[i + 1, j] == 1 && isHorizontal)
                        {
                            isCorrect = false;
                        }
                        while (extendedArr[k,m] == 1)
                        {
                            extendedArr[k, m] = 2;
                            if (extendedArr[k-1,m-1] == 1 || extendedArr[k - 1, m + 1] == 1 || extendedArr[k + 1, m - 1] == 1 || extendedArr[k + 1, m + 1] == 1)
                            {
                                isCorrect = false;
                            }
                            else
                            {
                                extendedArr[k - 1, m - 1] = 2;
                                extendedArr[k - 1, m + 1] = 2;
                                extendedArr[k + 1, m - 1] = 2;
                                extendedArr[k + 1, m + 1] = 2;
                            }
                            if (isHorizontal) m++;
                            else k++;
                        }
                        if (isHorizontal) ships[m - j - 1]++;
                        else ships[k - i - 1]++;
                        isHorizontal = false;

                    }
                }
            }
            for (int innerI = 0; innerI < 4; innerI++)
            {
                if (ships[innerI] != 4 - innerI) isCorrect = false;
            }
      return isCorrect;
    }
  }
}
