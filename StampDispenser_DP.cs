namespace Laserfiche.Recruiting.Screening.Stamps
{
    // here I used dynamic programming,
    using System.Collections.Generic;
    using System.Diagnostics;
    using System;

    public class StampDispenser
    {
        // set up a space in heap dynamically, c# is a little easier for developers than c++, it doesn't have to free this part of memory. 
        // Here in c#, we don't have to give the array a length when declaring it, and its contents are set to up null by default.
        public static int[] options;
        public StampDispenser(int[] stampDenominations)
        {
            // constructor: save the input values from the input array into global array and ready to be used.
            options = new int[stampDenominations.Length];
            for (int i = 0; i < stampDenominations.Length; i++)
            {
                // this time, I store the input values from the input array the opporsite way: from smallest to larggest. 
                // the reason is I want to store all the possibilities with the request value increasing.
                // I will talk more more about this in the following function.
                options[i] = stampDenominations[stampDenominations.Length - i - 1];
            }
        }

        public int CalcMinNumStampsToFillRequest(int request)
        {
            int stamps = -1; //set up the output variable.
             // here I set up an 2 dimensional array. The column title is from 0 to the request value increasing by 1 each time.
             // The row title is the how many types of stamps I choose. Ex {1, 2, 6, 10}, row 1 is 1 means I only use 1 for building
             // up the request; row 2 is 2 means I will use 1 and 2 to build up the request; row 3 is 3 means I will use 1 2 and 6 to 
             // build up the request. I want to fill out the table like this one for example {1, 7, 15} with request is 21:
             //     0	  1   2   3	 4	  5   6	7	 8	  9   10	 11  12	 13  14	 15  16	 17  18	 19  20	21
            // 1   0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21
            // 2   0   1   2   3   4   5   6   1   2   3     4    5    6    7    2    3   4     5    6   7    8   3
            // 3   0   1   2   3   4   5   6   1   2   3     4    5   6     7    2    1   2     3    4   5    6   3
            // That is the reason I stored the amounts of stamps increasingly. I want to fill out all the possible values from smallest
            // to largest. Also, the best thing of dynamic programming is it nevers drops the previous result, it will always save the previous and current
            // results so as to find out the best options. 
            // The equation can be summarized to be M[i,j] = MIN(the number of stamps by considering the biggest value VS the number of stamps by not considering the biggest value)
            int[,] matrix = new int[request + 1, options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                for (int j = 0; j < request + 1; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        matrix[j, i] = j; // this case is if either the request or the options are 0, the request are only using 1 to form. That is what the first row on the table above shows.
                    }
                    else
                    {
                        // j is the request, i is the index for stamp options. 
                        // j%options[i] is the remain values after choose the biggest stamp, i - 1 means then I will use the rest options to form what I still need.
                        // j/options[i] is how many biggest stamps I've choosed.
                        // matrix[j, i - 1] means I don't want to use the biggest stamp, I want to use the rest options to form the request.
                        // Compare this 2 ways and find the minimum stamps I can use.
                        matrix[j, i] = Math.Min(matrix[j % options[i], i - 1] + j / options[i], matrix[j, i - 1]);
                        //if (options[i] > j)
                        //{
                        //    matrix[j, i] = matrix[j, i - 1];
                        //}
                        //else
                        //{
                        //    matrix[j, i] = Math.Min(matrix[j - options[i], i] + 1, matrix[j, i - 1]);
                        //}
                    }
                }
            }
            stamps = matrix[request, options.Length - 1]; // after fill out the table, what I need is just find the location of the request and fill in the options length. The reason I subtract 1 at the length is because it is index; the reason I don't subtract 1 for request is because I don't want the request not just for array index.
            return stamps;
        }

        static void Main()
        {
            StampDispenser stampDispenser = new StampDispenser(new int[] { 90, 30, 24, 10, 6, 2, 1 });
            Debug.Assert(stampDispenser.CalcMinNumStampsToFillRequest(18) == 3);
        }
    }
}
