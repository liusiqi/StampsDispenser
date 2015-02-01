#include <stdlib.h>
#include <assert.h>

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
        for (int i = 0; i < numStampDenominations; i++)
        {
            options[i] = stampDenominations[i];
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
        int stamps = 0;
        for (int i = 0; i < num && request!=0; i++)
        {
            stamps += request/options[i];
            request = request%options[i];
        }
        return stamps;
    }

private:
    int* options;
    size_t num;
}; 

int main()
{
    int stampDenominations[] = {90, 30, 24, 10, 6, 2, 1};
    StampDispenser stampDispenser(stampDenominations, 7);
    assert(stampDispenser.CalcNumStampsToFillRequest(18) == 3);

    return 0;
}
