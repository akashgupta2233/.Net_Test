using System;

namespace MediSureClinicBilling
{
    /// <summary>
    /// Represents the billing details for an individual patient.
    /// Handles the storage and calculation of costs and discounts.
    /// </summary>
    public class PatientBill
    {
        // --- Properties ---
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        /// <summary>
        /// Calculates the total gross amount, applicable insurance discounts, 
        /// and the final amount the patient needs to pay.
        /// </summary>
        public void ComputeBill()
        {
            // Sum of all individual medical charges
            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;

            // Apply a 10% discount if the patient is insured
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount * 0.10m;
            }
            else
            {
                DiscountAmount = 0;
            }

            // Deduct discount from gross total to get final amount
            FinalPayable = GrossAmount - DiscountAmount;
        }

        /// <summary>
        /// Outputs a formatted receipt of the bill to the console.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("----------- Last Bill -----------");
            Console.WriteLine($"BillId: {BillId}");
            Console.WriteLine($"Patient: {PatientName}");
            Console.WriteLine($"Insured: {(HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }
    }

    /// <summary>
    /// Static utility class to manage the lifecycle of patient bills.
    /// Controls creating, viewing, and clearing the billing records.
    /// </summary>
    public static class BillMananger
    {
        // Stores the most recently processed bill in memory
        public static PatientBill LastBill;
        
        // Track whether a bill has been created to prevent errors when viewing
        public static bool HasLastBill = false;

        /// <summary>
        /// Prompts user for patient data, computes costs, and stores the record.
        /// </summary>
        public static void createBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            // Handle insurance status as a boolean check
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            bill.HasInsurance = insuranceInput.Trim().ToUpper() == "Y";

            // Use the helper method for robust decimal input validation
            bill.ConsultationFee = ReadDecimal("Enter Consultation Fee (>0): ", true);
            bill.LabCharges = ReadDecimal("Enter Lab Charges (>=0): ", false);
            bill.MedicineCharges = ReadDecimal("Enter Medicine Charges (>=0): ", false);

            // Execute the business logic calculation
            bill.ComputeBill();
            
            // Save to static memory
            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }

        /// <summary>
        /// Checks for an existing bill and displays it; otherwise, notifies the user.
        /// </summary>
        public static void ViewBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
            }
            else
            {
                LastBill.Display();
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Resets the billing storage.
        /// </summary>
        public static void ClearBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }

        /// <summary>
        /// Helper method to ensure user enters a valid decimal value.
        /// Loops until input is valid to prevent program crashes.
        /// </summary>
        /// <param name="prompt">The text to show the user.</param>
        /// <param name="mustBePositive">If true, value must be > 0. If false, value must be >= 0.</param>
        /// <returns>A validated decimal value.</returns>
        private static decimal ReadDecimal(string prompt, bool mustBePositive)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Check if the input is actually a number
                if (decimal.TryParse(input, out value))
                {
                    // Validation for positive requirements (e.g., fees shouldn't be zero)
                    if (mustBePositive && value <= 0)
                    {
                        Console.WriteLine("Value must be greater than 0.");
                        continue;
                    }
                    // Validation for non-negative requirements (e.g., charges can't be negative)
                    if (!mustBePositive && value < 0)
                    {
                        Console.WriteLine("Value must be non-negative.");
                        continue;
                    }
                    return value;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    }
}
