using System;

namespace QuickMartTraders
{
    // Core Entity Class
    public class SaleTransaction
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }
        public string ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set; }

        public static SaleTransaction LastTransaction { get; set; }
        public static bool HasLastTransaction { get; set; } = false;

        // Method to calculate profit/loss
        public void ComputeProfitLoss()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            ProfitMarginPercent = (PurchaseAmount > 0) 
                ? (ProfitOrLossAmount / PurchaseAmount) * 100 
                : 0;
        }

        // Method to print invoice-style summary
        public void PrintSummary()
        {
            Console.WriteLine("-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {InvoiceNo}");
            Console.WriteLine($"Customer: {CustomerName}");
            Console.WriteLine($"Item: {ItemName}");
            Console.WriteLine($"Quantity: {Quantity}");
            Console.WriteLine($"Purchase Amount: {PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {SellingAmount:F2}");
            Console.WriteLine($"Status: {ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
        }
    }
}
