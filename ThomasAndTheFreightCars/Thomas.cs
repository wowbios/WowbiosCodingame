namespace ThomasAndTheFreightCars;

public class Thomas
{
    public static int Solve(string input)
    {
        int[] cars = input.Split(' ').Select(int.Parse).ToArray();
        return GetMaxTrainLength(cars);
    }

    static int GetMaxTrainLength(int[] cars)
    {
        BuildChildren(
            int.MinValue,
            int.MaxValue,
            int.MaxValue,
            int.MinValue,
            0,
            cars,
            0);
        return max;
    }

    private static int max = 0;
    static void BuildChildren(
        int front,
        int rear,
        int sfront,
        int srear,
        int length,
        int[] cars,
        int i)
    {
        if (i == cars.Length || (length + cars.Length - i) < max)
        {
            if (length > max)
                max = length;
            return;
        }

        var car = cars[i];
        // front
        if (front < car && car > srear)
        {
            BuildChildren(
                car,
                rear,
                Math.Min(sfront, car),
                srear,
                length + 1,
                cars,
                i + 1);
        }

        // rear
        if (rear > car && car < sfront)
        {
            BuildChildren(front,
                car,
                sfront,
                Math.Max(srear, car),
                length + 1,
                cars,
                i + 1);
        }

        // skip
        BuildChildren(front,
            rear,
            sfront,
            srear,
            length,
            cars,
            i + 1);
    }
}