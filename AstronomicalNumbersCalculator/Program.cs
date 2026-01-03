using AstronomicalNumbersCalculator;

try
{
    bool exit = false;

    while (!exit)
    {
        var number1 = NumberCustomType.EnterNumber("Enter the first number: ");
        var number2 = NumberCustomType.EnterNumber("Enter the second number: ");
        
        var addition = number1 + number2;
        var subtraction = number1 - number2;
        var multiplication = number1 * number2;
        
        Console.WriteLine($"Addition: {addition.ToString()}");
        Console.WriteLine($"Subtraction: {subtraction.ToString()}");
        Console.WriteLine($"Multiplication: {multiplication.ToString()}");
        
        Console.Write("If you want to continue press 'enter', otherwise type 'exit': ");
        
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


