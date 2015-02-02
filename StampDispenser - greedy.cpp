#include <stdlib.h>
#include <assert.h>

class StampDispenser
{
public:
    StampDispenser(const int* stampDenominations, size_t numStampDenominations)
    {
        options = new int[numStampDenominations];
        for (int i = 0; i < numStampDenominations; i++)
        {
            options[i] = stampDenominations[i];
        }
        num = numStampDenominations;
    }

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
