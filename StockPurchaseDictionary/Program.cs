using System;
using System.Collections.Generic;
using System.Linq;

namespace StockPurchaseDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> stocks = new Dictionary<string, string>();
            stocks.Add("GM", "General Motors");
            stocks.Add("CAT", "Caterpillar");
            stocks.Add("FB", "Facebook");
            stocks.Add("HCA", "HCA HealthCare");
            stocks.Add("AAPL", "Apple Inc");
            stocks.Add("CCL", "Carnival Corp");
            stocks.Add("CI", "Cigna Corp");

            List<(string ticker, int shares, double price)> purchases = new List<(string, int, double)>();

            purchases.Add((ticker: "GM", shares: 150, price: 23.21));
            purchases.Add((ticker: "GM", shares: 32, price: 17.87));
            purchases.Add((ticker: "GM", shares: 80, price: 19.02));
            purchases.Add((ticker: "AAPL", shares: 30, price: 15.02));
            purchases.Add((ticker: "AAPL", shares: 22, price: 20.45));
            purchases.Add((ticker: "HCA", shares: 45, price: 16.00));

           Dictionary<string, double> aggregatedPurchase = new Dictionary<string, double>();
            aggregatedPurchase.Add("HCA HealthCare", 100.00);

            var ownershipReport = stocks
                .Join(purchases,
                stock => stock.Key,
                purchase => purchase.ticker,
                (stock, purchase) => new { stockName = stock.Value, stockValuation = purchase.shares * purchase.price }
                );

            // Iterate over the purchases and update the valuation for each stock
            // Does the company name key already exist in the report dictionary? // If it does, update the total valuation 
            // If not, add the new key and set its value

            foreach (var stock in ownershipReport)
            {
                if (aggregatedPurchase.ContainsKey(stock.stockName))
                {
                    aggregatedPurchase[stock.stockName] = aggregatedPurchase[stock.stockName] + stock.stockValuation;
                }
                else {
                    aggregatedPurchase.Add(stock.stockName, stock.stockValuation);
                }
            }

            foreach (var (stockName, stockValuation) in aggregatedPurchase)
            {
                Console.WriteLine($"{stockName}: {stockValuation}");
            }
            Console.ReadLine();
        }
    }
}
