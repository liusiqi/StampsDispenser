namespace Laserfiche.Recruiting.Screening.Stamps
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class StampDispenser
    {
        public static int[] options;
        public StampDispenser(int[] stampDenominations)
        {
            options = new int[stampDenominations.Length];
            for (int i = 0; i < stampDenominations.Length; i++)
            {
                options[i] = stampDenominations[i];
            }
        }

        public int CalcMinNumStampsToFillRequest(int request)
        {
            int stamps = 0;
            for (int i = 0; i < options.Length && request != 0; i++)
            {
                stamps += request / options[i];
                request = request % options[i];
            }
            return stamps;
        }

        static void Main()
        {
            StampDispenser stampDispenser = new StampDispenser(new int[] { 90, 30, 24, 10, 6, 2, 1 });
            Debug.Assert(stampDispenser.CalcMinNumStampsToFillRequest(18) == 3);
        }
    }
}
