using System;
using System.Collections.Generic;
using System.Linq;
using AssociationsService.Entities.Association;
using AssociationsService.ProductProviders;
using AssociationsService.Utils;

namespace AssociationsService
{
    internal class Program
    {
        static void Main()
        {
            var words = new[] { "shoes", "cheese", "wooden doll" };
            //var words = new[] { "tent", "pan" };
            //var words = new[] { "eqwedagfr", "", "123", "someuselessunknownshit" };
            //var words = new[] { "hobby", "joy", "rocket", "cheese", "cream", "cat", "bus", "book", "run", "gym" };
            var service = new Services.AssociationsService(new HttpUtil());

            var task = service.GetAssociationsAsync(words, 2);
            task.Wait();
            var associations = task.Result;

            Console.WriteLine("---- Words ----");
            foreach (string word in words)
            {
                Console.Write(word + " | ");
            }

            Console.WriteLine("\n");

            Console.WriteLine("---- Associations ----");
            var assotiationsList = associations as IList<Association> ?? associations.ToList();
            foreach (var association in assotiationsList)
            {
                string items = association.Items
                    .Aggregate("", (current, associationItem) => 
                        current + associationItem.Item + "(" + associationItem.Weight + ") | ");

                Console.WriteLine(association.Text + ": " + items);
            }

            Console.WriteLine("\n");

            Console.WriteLine("---- Products ----");
            var productService = new Services.ProductService(new [] { new EBayProductProvider(new HttpUtil()) });

            foreach (var association in assotiationsList)
            {
                Console.WriteLine(association.Text + ":");

                foreach (var item in association.Items)
                {
                    Console.WriteLine("\t" + item.Item + ":");

                    var products = productService.SearchProducts(item.Item)
                        .Where(product => product.ImageUrl != null)
                        .Take(3)
                        .ToList();

                    foreach (var product in products)
                    {
                        Console.WriteLine("\t\t->" + product.Title + " - " + product.Price.Value + " " + product.Price.Currency);
                        Console.WriteLine("\t\t\tSrc: " + product.SourceUrl);
                        Console.WriteLine("\t\t\tImage: " + product.ImageUrl);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}