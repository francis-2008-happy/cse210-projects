using System;
using System.Collections.Generic;

namespace OnlineOrdering
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("123 Main St", "Rexburg", "ID", "USA");
            Address address2 = new Address("456 Oak Ave", "Toronto", "ON", "Canada");
            
            // Create customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Jane Smith", address2);
            
            // Create products
            Product product1 = new Product("Laptop", "P100", 999.99, 1);
            Product product2 = new Product("Mouse", "P101", 19.99, 2);
            Product product3 = new Product("Keyboard", "P102", 49.99, 1);
            Product product4 = new Product("Monitor", "P103", 199.99, 2);
            
            // Create orders
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);
            
            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);
            order2.AddProduct(product1); // Adding laptop again to test
            
            // Display order information
            Console.WriteLine("Order 1:");
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order1.CalculateTotalCost():0.00}\n");
            
            Console.WriteLine("Order 2:");
            Console.WriteLine("Packing Label:");
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine("Shipping Label:");
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order2.CalculateTotalCost():0.00}");
        }
    }

    public class Address
    {
        private string _street;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string street, string city, string stateProvince, string country)
        {
            _street = street;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.ToLower() == "usa";
        }

        public string GetFullAddress()
        {
            return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
        }
    }

    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public bool LivesInUSA()
        {
            return _address.IsInUSA();
        }

        public string GetName()
        {
            return _name;
        }

        public Address GetAddress()
        {
            return _address;
        }
    }

    public class Product
    {
        private string _name;
        private string _productId;
        private double _price;
        private int _quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        public double CalculateTotalCost()
        {
            return _price * _quantity;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetProductId()
        {
            return _productId;
        }

        public int GetQuantity()
        {
            return _quantity;
        }
    }

    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double total = 0;
            foreach (Product product in _products)
            {
                total += product.CalculateTotalCost();
            }

            // Add shipping cost
            total += _customer.LivesInUSA() ? 5 : 35;
            
            return total;
        }

        public string GetPackingLabel()
        {
            string label = "";
            foreach (Product product in _products)
            {
                label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
            }
            return label;
        }

        public string GetShippingLabel()
        {
            return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
        }
    }
}