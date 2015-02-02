namespace Laserfiche.Recruiting.Screening.Stamps
{
    // At the first throught, I wanted to use greedy algorithm http://en.wikipedia.org/wiki/Greedy_algorithm, which is very straight forward for this question -- stamps with specific amounts.
    // I will talk a little about this code. But when I tested more input options with different amounts of stamps, greedy algorithm can't always give me the best(smallest number) of stamps. 
    // So then I used dynamic programming which is a bit harder to understand. I had other two files using dynamic programming.
    using System.Collections.Generic;
    using System.Diagnostics;

    public class StampDispenser
    {
        // set up a space in heap dynamically, c# is a little easier for developers than c++, it doesn't have to free this part of memory. 
        // Here in c#, we don't have to give the array a length when declaring it, and its contents are set to up null by default.
        public static int[] options;
        // constructor: save the input values from the input array into global array and ready to be used.
        public StampDispenser(int[] stampDenominations)
        {
            // Before copying values into the new array, we must give it a length.
            options = new int[stampDenominations.Length];
            for (int i = 0; i < stampDenominations.Length; i++)
            {
                options[i] = stampDenominations[i]; //copy from input and save values.
            }
        }

        public int CalcMinNumStampsToFillRequest(int request)
        {
            int stamps = 0; //output, number of stamps
            // in a for loop, i increases from 0 to the length of the input array. Also, if the request value is 0, jump out of the loop. 
            for (int i = 0; i < options.Length && request != 0; i++)
            {
                // Because the array for the amounts of stamps are decreasing, and greedy alogrithm is always checking the first(biggest) value,
                // if it is bigger than the request, goes to the next value. When it finds the first stamp that is smaller, it takes it then don't consider
                // it any more. 
                stamps += request / options[i]; // this step, when it finds the biggest value that can be used, it takes as much as it and add the number it takes to stamps. Ex, 21/10 = 2, two 10 has added to stamps.
                request = request % options[i]; // this step, request equals what is left out. Ex, 21%10 = 1, in the next loop, request is 1 which only needs the amount 1 stamp to add to stamps.
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
