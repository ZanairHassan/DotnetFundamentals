using VariablesAndDataTypes;

VariablesAction objVariableAcion = new VariablesAction();
objVariableAcion.StudentDetails();
Console.Write("Length:\t");
int length = Convert.ToInt32(Console.ReadLine());
Console.Write("Wirdth:\t");
int wirdth = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("\n************************* Area Of Rectangular ************");
objVariableAcion.AreaOfRectangular(length, wirdth);

Console.WriteLine("\n************************* Swap ***************************");
objVariableAcion.SwapNumbers(length, wirdth);

Console.WriteLine("\n************************* Circle Actions ***************************");
Console.Write("Enter Radius:\t");
double radius = Convert.ToDouble(Console.ReadLine());
objVariableAcion.CircleAction(radius);

Console.WriteLine("\n************************* Logical Comparision ***************************");

Console.Write("A:\t");
int a = Convert.ToInt32(Console.ReadLine());
Console.Write("B:\t");
int b = Convert.ToInt32(Console.ReadLine());
objVariableAcion.LogicalComparision(a, b);

Console.WriteLine("\n************************* Variable Casting ***************************");

objVariableAcion.VariableCasting();

Console.WriteLine("Press any key exit the program");
Console.ReadLine();