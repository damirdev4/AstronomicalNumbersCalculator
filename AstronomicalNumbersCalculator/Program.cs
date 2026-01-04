using AstronomicalNumbersCalculator;

try
{
    bool exit = false;

    while (!exit)
    {
        NumberCustomType number1 = RequestNumber("Enter the first number: ");
        NumberCustomType number2 = RequestNumber("Enter the second number: ");

        var addition = number1 + number2;
        var subtraction = number1 - number2;
        var multiplication = number1 * number2;

        Console.WriteLine($"Addition: {addition.ToString()}");
        Console.WriteLine($"Subtraction: {subtraction.ToString()}");
        Console.WriteLine($"Multiplication: {multiplication.ToString()}");

        Console.Write("If you want to continue press 'Enter', otherwise type 'exit': ");

        string? pressKey = Console.ReadLine();

        if (pressKey?.Trim().ToLower() == "exit")
        {
            exit = true;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static NumberCustomType RequestNumber(string message)
{
    NumberCustomType? result;
    while (true)
    {
        Console.Write(message);
        string input = Console.ReadLine()!;

        if (NumberCustomType.TryParse(input, out result))
        {
            return result!;
        }
        Console.WriteLine("Please enter a valid number.");
    }
}

