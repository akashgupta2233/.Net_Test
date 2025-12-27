    class Program
    {
        static void Main()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateTransaction();
                        break;

                    case "2":
                        ViewTransaction();
                        break;

                    case "3":
                        CalculateTransaction();
                        break;

                    case "4":
                        Console.WriteLine("\nThank you. Application closed normally.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }

        // Method to create new transaction
        public static void CreateTransaction()
        {
            SaleTransaction transaction = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            transaction.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(transaction.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.\n");
                return;
            }

            Console.Write("Enter Customer Name: ");
            transaction.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            transaction.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Invalid Quantity. Must be greater than 0.\n");
                return;
            }
            transaction.Quantity = qty;

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Invalid Purchase Amount. Must be greater than 0.\n");
                return;
            }
            transaction.PurchaseAmount = purchase;

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Invalid Selling Amount. Must be >= 0.\n");
                return;
            }
            transaction.SellingAmount = selling;

            // Compute profit/loss
            transaction.ComputeProfitLoss();

            // Save transaction
            SaleTransaction.LastTransaction = transaction;
            SaleTransaction.HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            Console.WriteLine($"Status: {transaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {transaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {transaction.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------\n");
        }

        // Method to view transaction
        public static void ViewTransaction()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.\n");
                return;
            }

            SaleTransaction.LastTransaction.PrintSummary();
            Console.WriteLine("------------------------------------------------------\n");
        }

        // Method to recalculate transaction
        public static void CalculateTransaction()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.\n");
                return;
            }

            SaleTransaction.LastTransaction.ComputeProfitLoss();
            SaleTransaction.LastTransaction.PrintSummary();
            Console.WriteLine("------------------------------------------------------\n");
        }
    }