using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        List<string> history = new List<string>();
        Dictionary<string,double> assignations= new Dictionary<string, double>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public void saveVariable(string name,double value)
        {
            if (assignations.ContainsKey(name))
            {
                assignations[name] = value; // Overwrite existing value
                Console.WriteLine("Variable " + name + " has been overwrited");
                history.Add("Variable " + name + " overwrited.");
            }
            else
            {
                Console.WriteLine("Variable " + name + " has been declared");
                assignations.Add(name, value); // Add new key-value pair
                history.Add("Variable " + name + " assigned.");
            }
        }

        public double getVaribale(string name)
        {
            if (assignations.ContainsKey(name))
            {
                return assignations[name];
            }
            double cleanNum = 0;
            if (double.TryParse(name, out cleanNum))
            {
                return cleanNum;
            }
            else return double.NaN;
        }
        public void printVariable(string name)
        {
            if (assignations.ContainsKey(name))
            {
                history.Add("Variable " + name + " printed.");
                Console.WriteLine("Variable " + name + " = " + assignations[name]);
            }
            else
            {
                Console.WriteLine("Variable not found");
            }
        }

        public void deleteVariable(string name)
        {
            
            if (assignations.ContainsKey(name))
            {
                assignations.Remove(name);
                history.Add("Variable " + name + " deleted.");
                Console.WriteLine("Variable " +name +" deleted.");
            }
            else
            {
                Console.WriteLine("Variable "+name +" not found");
            }
        }
        public void showAllVariables()
        {
            Console.WriteLine("Showing all variables");
            foreach (KeyValuePair<string, double> kvp in assignations)
            {
                Console.WriteLine( kvp.Key + " = " + kvp.Value);
            }
        }
        public void showHistory()
        {
            Console.WriteLine("Showing all history");
            for(int i = history.Count()-1; i>=0; i--)
            {
                Console.WriteLine("Instruction "+i+" value: "+history[i]);
            }
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    history.Add(num1+" + "+num2+" = "+result);
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    history.Add(num1 + " - " + num2 + " = " + result);
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    history.Add(num1 + " * " + num2 + " = " + result);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    history.Add(num1 + " / " + num2 + " = " + result);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}