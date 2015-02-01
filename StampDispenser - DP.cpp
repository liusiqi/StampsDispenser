#include <stdlib.h>
#include <assert.h>
#include <iostream>
using std::cout;
using std::endl;

int min(int a, int b)
{
    return a>b?b:a;
}

class StampDispenser
{
public:
    StampDispenser(const int* stampDenominations, size_t numStampDenominations)
    {
        options = new int[numStampDenominations];
        for (int i = numStampDenominations - 1; i >= 0; i--)
        {
            options[numStampDenominations - i - 1] = stampDenominations[i];
        }
        num = numStampDenominations;
    }

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
    assert(stampDispenser.CalcNumStampsToFillRequest(18) == 3);
    return 0;
}
