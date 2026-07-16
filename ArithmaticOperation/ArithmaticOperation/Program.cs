using ArithmaticOperation;

Operations objOperations = new Operations();
bool continueExecute = true;
while (continueExecute)
{
    Console.WriteLine("Enter first number");
    int a = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter second number");
    int b = Convert.ToInt32(Console.ReadLine());
    while (true)
    {
        Console.WriteLine("Enter the Arithmatic Operation Form (+, -, *, /, %)");
        char op = Convert.ToChar(Console.ReadLine());
        switch (op)
        {
            case '+':
                {
                    objOperations.Addition(a, b); 
                    break;
                }
            case '-':
                {
                    objOperations.Substraction(a, b); break;
                }
            case '*':
                {
                    objOperations.Multipication(a, b); break;
                }
            case '/':
                {
                    objOperations.Division(a, b); break;
                }
            case '%':
                {
                    objOperations.MOD(a, b); break;
                }
            case 'i':
                {
                    objOperations.Increment(a, b); break;
                }
            case 'd':
                {
                    objOperations.Decrement(a, b); break;
                }

            default:
                {
                    Console.WriteLine("Invalid operator! Try again."); continue;
                }
        }
        break;

    }
    Console.Write("\nDo you want another calculation? (Y/N): ");
    char choice = Convert.ToChar(Console.ReadLine());

    if (char.ToUpper(choice) != 'Y')
    {
        continueExecute = false;
        Console.WriteLine("Calculator Closed.");
    }
}

