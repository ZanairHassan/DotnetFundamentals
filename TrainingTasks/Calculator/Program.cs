using Calculator;

Console.WriteLine("Hello, World!");

Console.WriteLine("\n************************* Calculator ***************************");

CalculatorAction objCalculator = new CalculatorAction();
bool continueToExecute = true;

while (continueToExecute)
{
    Console.WriteLine("\nChoose Data Type");
    Console.WriteLine("1. Integer");
    Console.WriteLine("2. Float");
    Console.WriteLine("3. Double");
    Console.WriteLine("\nPress 4 if you want to exit.");

    Console.Write("\nEnter your choice:\t");
    int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            {
                Console.Write("\nEnter first number:\t");
                int firstNumber = Convert.ToInt32(Console.ReadLine());

                char op = objCalculator.ReadValidOperator();

                Console.Write("Enter second number:\t");
                int secondNumber = Convert.ToInt32(Console.ReadLine());

                objCalculator.Calculate(firstNumber, op, secondNumber);

                break;
            }
        case 2:
            {
                Console.Write("\nEnter first number:\t");
                float firstNumber = Convert.ToSingle(Console.ReadLine());

                char op = objCalculator.ReadValidOperator();

                Console.Write("Enter second number:\t");
                float secondNumber = Convert.ToSingle(Console.ReadLine());

                objCalculator.Calculate(firstNumber, op, secondNumber);

                break;
            }

        case 3:
            {
                Console.Write("\nEnter first number:\t");
                double firstNumber = Convert.ToDouble(Console.ReadLine());

                char op = objCalculator.ReadValidOperator();

                Console.Write("Enter second number:\t");
                double secondNumber = Convert.ToDouble(Console.ReadLine());

                objCalculator.Calculate(firstNumber, op, secondNumber);

                break;
            }

        case 4:
            {
                Console.WriteLine("\nCalculator Closed.");
                return;
            }

        default:
            {
                Console.WriteLine("\nInvalid Menu Choice.");
                continue;
            }
    }

    Console.Write("\nDo you want another calculation? (Y/N): ");

    char answer = Convert.ToChar(Console.ReadLine());

    if (char.ToUpper(answer) != 'Y')
    {
        continueToExecute = false;
        Console.WriteLine("\nCalculator Closed.");
    }
}
