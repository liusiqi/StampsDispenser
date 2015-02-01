namespace Laserfiche.Recruiting.Screening.Stamps
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System;

    public class StampDispenser
    {
        public static int[] options;
        public StampDispenser(int[] stampDenominations)
        {
            options = new int[stampDenominations.Length];
            for (int i = 0; i < stampDenominations.Length; i++)
            {
                options[i] = stampDenominations[stampDenominations.Length - i - 1];
            }
        }

        public int CalcMinNumStampsToFillRequest(int request)
        {
            int stamps = -1;
            int[,] matrix = new int[request + 1, options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                for (int j = 0; j < request + 1; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        matrix[j, i] = j;
                    }
                    else
                    {
                        if (options[i] > j)
                        {
                            matrix[j, i] = matrix[j, i - 1];
                        }
                        else
                        {
                            matrix[j, i] = Math.Min(matrix[j - options[i], i] + 1, matrix[j, i - 1]);
                        }
                    }
                }
            }
            stamps = matrix[request, options.Length - 2];
            return stamps;
        }

        static void Main()
        {
            StampDispenser stampDispenser = new StampDispenser(new int[] { 90, 30, 24, 10, 6, 2, 1 });
            Debug.Assert(stampDispenser.CalcMinNumStampsToFillRequest(18) == 3);
        }
    }
}
