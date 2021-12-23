using System;
namespace Solution
{
      public class User
    {
        #region Fields

        public int rank;
        public int progress;

        #endregion

        #region Constructor

        public User()
        {
            rank = -8;
            progress = 0;
        }

        #endregion

        #region Methods

        public void incProgress(int task)
        {
          Console.WriteLine(task + " " + rank);
          if (rank < 8)
          {
          int delta = task - rank;
                    if (task > 0 && rank < 0)
                    {
                        delta--;
                    }
                    else if (task < 0 && rank > 0)
                    {
                        delta++;
                    }
            if (task >= -8 && task <= 8 && task != 0 && delta > -2 )
            {
                if (delta == 0)
                {
                    progress += 3;
                }
                else if (delta == -1)
                {
                    progress++;
                }
                else
                {
                    
                    progress += 10 * delta * delta;
                }

                while (progress >= 100)
                {
                    rank = RankUp();
                    progress -= 100;
                    if (rank == 8)
                      {
                      progress = 0;
                    }
                }

                
            }
            else
            {
                throw new ArgumentException("task parameter is invalid");
            }
            }
        }
        

        private int RankUp()
        {
            if (rank == 8)
            {
                return 8;
            }
            if (rank == -1)
            {
                return 1;
            }
            else
            {
                return ++rank;
            }
        }

        #endregion
    }
}
