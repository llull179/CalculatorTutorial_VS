using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {

        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp) 
            {
                // Declare variables and set to empty.
                string operation = "";

                Console.WriteLine("Choose an action from the following list:");
                Console.WriteLine("\tc - Calculate");
                Console.WriteLine("\th - Show history");
                Console.WriteLine("\ta - Assing variable");
                Console.WriteLine("\ts - Show one variable");
                Console.WriteLine("\tsa - Show all variables");
                Console.WriteLine("\td - Delete variable");
                Console.WriteLine("\tend - End");
                operation = Console.ReadLine();

                

                switch (operation)
                {
                    case "c":
                        string numInput1 = "";
                        string numInput2 = "";
                        double result = 0;

                        // Ask the user to type the first number.
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();
                        double cleanNum1 = calculator.getVaribale(numInput1);
                        while (cleanNum1 == double.NaN)
                        {
                            Console.Write("This is not valid input. Please enter an integer value or a saved variable: ");
                            numInput1 = Console.ReadLine();
                            cleanNum1 = calculator.getVaribale(numInput1);      
                        }

                        // Ask the user to type the second number.
                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();
              
                        double cleanNum2 = calculator.getVaribale(numInput2);
                        while (cleanNum2 == double.NaN)
                        {
                            Console.Write("This is not valid input. Please enter an integer value or an assigned variable: ");
                            numInput2 = Console.ReadLine();
                            cleanNum2 = calculator.getVaribale(numInput2);
                        }

                        // Ask the user to choose an operator.
                        Console.WriteLine("Choose an operator from the following list:");
                        Console.WriteLine("\ta - Add");
                        Console.WriteLine("\ts - Subtract");
                        Console.WriteLine("\tm - Multiply");
                        Console.WriteLine("\td - Divide");
                        Console.WriteLine("\th - History");
                        Console.Write("Your option? ");

                        string op = Console.ReadLine();

                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                        break;

                    case "a":
                        string name = "";
                        string value = "";
                        Console.WriteLine("Name of the variable");
                        name = Console.ReadLine();
                        Console.WriteLine("Value of the variable");
                        value = Console.ReadLine();

                        double cleanValue = 0;
                        while (!double.TryParse(value, out cleanValue))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            value = Console.ReadLine();
                        }
                        calculator.saveVariable(name, cleanValue);
                        break;
                         
                    case "h":
                        calculator.showHistory();
                        break;

                    case "s":
                        Console.WriteLine("Select variable to print");
                        string nameValue = "";
                        nameValue = Console.ReadLine();
                        calculator.printVariable(nameValue);
                        break;
                    case "sa":
                        calculator.showAllVariables();
                        break;
                    case "d":
                        Console.WriteLine("Select variable to delete");
                        string nameVarDelet = "";
                        nameVarDelet = Console.ReadLine();
                        calculator.deleteVariable(nameVarDelet);
                        break;
                    case "end":
                        endApp = true;
                        break;

                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.


               
            }
            calculator.Finish();
            return;
        }
    }
}