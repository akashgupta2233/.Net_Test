using System;
using MediSureClinicBilling;  

class Program
{
    public static void Main()   
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Enter your option: ");
            string n = Console.ReadLine();
            Console.WriteLine();

            switch (n)
            {
                case "1": 
                    BillMananger.createBill();
                    break;
                case "2": 
                    BillMananger.ViewBill();
                    break;
                case "3": 
                    BillMananger.ClearBill();
                    break;
                case "4": 
                    Console.WriteLine("Thank you. Application closed normally."); 
                    exit = true;
                    break; 
                default: 
                    Console.WriteLine("Invalid option. Please try again."); 
                    break;              
            }
        }
    }
}
