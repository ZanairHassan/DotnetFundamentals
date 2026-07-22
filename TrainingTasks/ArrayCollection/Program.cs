using ArrayCollection;

ArrayActions obj = new ArrayActions();
obj.PrintmultiDimentionArray();
obj.InputPrintmultiDimentionArray();
obj.ArithmaticActionOnArrays();

CollectionsActions objCol = new CollectionsActions();

int choice;

do
{
    Console.Clear();

    Console.WriteLine("========== STUDENT MANAGEMENT (Collection) ==========");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Display Students");
    Console.WriteLine("3. Search Student");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Exit");

    Console.Write("\nEnter Choice : ");
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            objCol.AddStudent();
            break;

        case 2:
            objCol.DisplayStudent();
            break;

        case 3:
            objCol.SearchStudent();
            break;

        case 4:
            objCol.UpdateStudent();
            break;

        case 5:
            objCol.DeleteStudent();
            break;

        case 6:
            Console.WriteLine("Thank You...");
            break;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }

    if (choice !=6)
    {
        Console.WriteLine("\nPress Any Key...");
        Console.ReadKey();
    }

} while (choice != 6);

DictionaryActions dictionaryActions = new DictionaryActions();
int choiceOperation;

do
{
    Console.Clear();

    Console.WriteLine("========== STUDENT MANAGEMENT (Dictionary) ==========");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. Display Students");
    Console.WriteLine("3. Search Student");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Display Roll Numbers");
    Console.WriteLine("7. Display Student Names");
    Console.WriteLine("8. Exit");

    Console.Write("\nEnter Choice :\t");
    choiceOperation = Convert.ToInt32(Console.ReadLine());

    switch (choiceOperation)
    {
        case 1:
            dictionaryActions.AddStudent();
            break;

        case 2:
            dictionaryActions.DisplayStudents();
            break;

        case 3:
            dictionaryActions.SearchStudent();
            break;

        case 4:
            dictionaryActions.UpdateStudent();
            break;

        case 5:
            dictionaryActions.DeleteStudent();
            break;

        case 6:
            dictionaryActions.DisplayKeys();
            break;

        case 7:
            dictionaryActions.DisplayStudentNames();
            break;

        case 8:
            Console.WriteLine("Thank You...");
            break;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }

    if (choiceOperation != 8)
    {
        Console.WriteLine("\nPress Any Key...");
        Console.ReadKey();
    }

} while (choiceOperation != 8);