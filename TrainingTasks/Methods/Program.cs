using Methods;

Console.WriteLine("Hello, World!");
GradeCalculator obj= new GradeCalculator();
char choice;

do
{
    Console.Clear();

    Console.Write("Enter Student Name:\t");
    string name = Console.ReadLine();

    int[] marks = obj.GetMarks();

    double total = obj.CalculateTotal(marks);

    double percentage = obj.CalculatePercentage(total);

    string grade = obj.GetGrade(percentage);

    obj.DisplayResult(name, total, percentage, grade);

    Console.Write("\nCalculate another student? (Y/N):\t");
    choice = char.ToUpper(Convert.ToChar(Console.ReadLine()));

} while (choice == 'Y' || choice == 'y');

Console.WriteLine("\nThank You!");