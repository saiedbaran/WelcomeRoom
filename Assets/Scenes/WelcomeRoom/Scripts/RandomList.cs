using System.Collections.Generic;

public class RandomList
{
    public static List<int> RandomIntList(List<int> inputList)
    {
        List<int> test = new List<int>();
        test = inputList;

        System.Random _random = new System.Random();

        int myGO;

        int n = inputList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = inputList[r];
            inputList[r] = inputList[i];
            inputList[i] = myGO;
        }

        return inputList;
    }
}