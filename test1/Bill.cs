namespace MediSureClinicBilling
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        // Member function to generate Bill
        public void ComputeBill()
        {
            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount * 0.10m;
            }
            else
            {
                DiscountAmount = 0;
            }
            FinalPayable = GrossAmount - DiscountAmount;
        }

        // Function to display bill
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

    public static class BillMananger
    {
        public static PatientBill LastBill;
        public static bool HasLastBill = false;

        public static void createBill()
        {
            PatientBill bill = new PatientBill();

            Console.WriteLine("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            bill.HasInsurance = insuranceInput.Trim().ToUpper() == "Y";

            bill.ConsultationFee = ReadDecimal("Enter Consultation Fee (>0): ", true);
            bill.LabCharges = ReadDecimal("Enter Lab Charges (>=0): ", false);
            bill.MedicineCharges = ReadDecimal("Enter Medicine Charges (>=0): ", false);

            bill.ComputeBill();
            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }

        // Function to view bill if available
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
        // Function to clear last bill
        public static void ClearBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }

        private static decimal ReadDecimal(string prompt, bool mustBePositive)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out value))
                {
                    if (mustBePositive && value <= 0)
                    {
                        Console.WriteLine("Value must be greater than 0.");
                        continue;
                    }
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
