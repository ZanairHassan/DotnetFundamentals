using ControlStatements;

Console.WriteLine("Hello, World!");
ControlActions obj = new ControlActions();
Console.Write("Enter a number:\t");
int number = Convert.ToInt32(Console.ReadLine());
if (obj.EvenOdd(number))
{
    Console.WriteLine($"The number {number} is Even.");
}
else
    Console.WriteLine($"The number {number} is Odd.");
Console.WriteLine("******************* Prime Number ***********************");

if (number > 0)
{
    Console.Write("The Prime Numbers are:\t");
    for (int num = 2; num <= number; num++)
    {
        if (obj.IsPrime(num))
        {
            Console.Write(num + " ");
        }
    }
    Console.WriteLine();
}
obj.Factorial(number);
obj.NumberTable(number);
obj.ExecuteAuthentication();
NumberGuessingGame objGame = new NumberGuessingGame();
objGame.GameAction();