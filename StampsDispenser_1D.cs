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
            int[] matrix = new int[request + 1];
            matrix[0] = 0;
            for (int i = 1; i <= request; i++)
            {
                matrix[i] = -1;
                for (int j = 0; j < options.Length; j ++)
                {
                    if (options[j] > i)
                        break;
                    if (matrix[i - options[j]] + 1 < matrix[i] || matrix[i] == -1)
                    {
                        matrix[i] = matrix[i - options[j]] + 1;
                    }
                }
            }
            stamps = matrix[request];
            return stamps;
        }

        static void Main()
        {
            StampDispenser stampDispenser = new StampDispenser(new int[] { 90, 30, 24, 10, 6, 2, 1 });
            Debug.Assert(stampDispenser.CalcMinNumStampsToFillRequest(18) == 3);
        }
    }
}
