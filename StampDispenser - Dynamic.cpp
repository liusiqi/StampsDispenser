#include <stdlib.h>
#include <assert.h>
#include <iostream>
using std::cout;
using std::endl;

int min(int a, int b)
{
    return a>b?b:a;
}

/// <summary>
/// Facilitates dispensing stamps for a postage stamp machine.
/// </summary>
class StampDispenser
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="StampDispenser"/> class that will be 
    /// able to dispense the given types of stamps.
    /// </summary>
    /// <param name="stampDenominations">
    /// The values of the types of stamps that the machine has.  
    /// Should be sorted in descending order and contain at least a 1.
    /// </param>
    /// <param name="numStampDenominations">
    /// The number of types of stamps in the stampDenominations array. 
    /// </param>
    StampDispenser(const int* stampDenominations, size_t numStampDenominations)
    {
        options = new int[numStampDenominations];
        for (int i = numStampDenominations - 1; i >= 0; i--)
        {
            options[numStampDenominations - i - 1] = stampDenominations[i];

        }
        num = numStampDenominations;
    }
    
    /// <summary>
    /// Returns the minimum number of stamps that the machine can dispense to
    /// fill the given request.
    /// </summary>
    /// <param name="request">
    /// The total value of the stamps to be dispensed.
    /// </param>
    /// <returns>
    /// The minimum number of stamps needed to fill the given request.
    /// </returns>
    int CalcNumStampsToFillRequest(int request)
    {
        int result = -1;
        int **mart = new int*[request+1];
        for(int i = 0; i <= request; ++i) 
        {
            mart[i] = new int[num];
        }
        for (int j = 0; j < num; j++)
        {
            for (int i = 0; i <= request; i++)
            
            {
                if (i == 0 || j == 0)
                {
                    mart[i][j] = i;
                }
                else
                {
                    if (options[j]>i)
                        mart[i][j] = mart[i][j-1];
                    else
                        mart[i][j] = min(mart[i-options[j]][j] + 1, mart[i][j-1]);
                }
            }
        }
        result = mart[request][num-1];
        
        for(int i = 0; i < request + 1; ++i) 
        {
            delete [] mart[i];
        }
        delete [] mart;
        
        return result;
    }

private:
    int* options;
    size_t num;
}; 

int main()
{
    int stampDenominations[] = {90, 30, 24, 10, 6, 2, 1};
    StampDispenser stampDispenser(stampDenominations, 7);
    cout << stampDispenser.CalcNumStampsToFillRequest(960000) << endl;

    return 0;
}
