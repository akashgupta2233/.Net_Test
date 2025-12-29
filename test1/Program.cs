using System;
using MediSureClinicBilling;

/// <summary>
/// The main entry point for the MediSure Clinic Billing System.
/// Handles the user interface loop and menu navigation.
/// </summary>
class Program
{
    /// <summary>
    /// Displays a menu-driven interface to manage patient billing operations.
    /// </summary>
    public static void Main()
    {
        // Control flag to keep the application running until the user chooses to exit
        bool exit = false;

        while (!exit)
        {
            // --- Display Menu Interface ---
            Console.WriteLine("================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: "); // Using Write to keep input on the same line
            
            string input = Console.ReadLine();
            Console.WriteLine();

            // --- Process User Input ---
            switch (input)
            {
                case "1":
                    // Triggers the logic to gather patient data and generate a bill
                    BillMananger.createBill();
                    break;

                case "2":
                    // Displays the details of the most recently created bill
                    BillMananger.ViewBill();
                    break;

                case "3":
                    // Resets or deletes the current billing data in memory
                    BillMananger.ClearBill();
                    break;

                case "4":
                    // Sets the flag to false to break the while loop and close the app
                    Console.WriteLine("Thank you. Application closed normally.");
                    exit = true;
                    break;

                default:
                    // Handles scenarios where the user enters text or numbers not in the menu
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            // Adds a small spacing for better readability between operations
            Console.WriteLine();
        }
    }
}
