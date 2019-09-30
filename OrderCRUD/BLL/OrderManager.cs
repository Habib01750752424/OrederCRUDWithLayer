using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderCRUD.Repository;

namespace OrderCRUD.BLL
{
    public class OrderManager
    {
        OrderRepository _orderRepository = new OrderRepository();
        public bool CheckIfNumeric(string input)
        {
            return _orderRepository.CheckIfNumeric(input);
        }

        public bool Add(string customerName, string itemName, double price, int qty)
        {
            return _orderRepository.Add(customerName,itemName,price,qty);
        }

        public DataTable Display()
        {
            return _orderRepository.Display();
        }

        public bool Update(int id, string customerName, string itemName, double price, int qty)
        {
            return _orderRepository.Update(id,customerName,itemName,price,qty);
        }

        public bool IsNameExist(string customerName)
        {
            return _orderRepository.IsNameExist(customerName);
        }

        public bool Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public DataTable Search(string name)
        {
            return _orderRepository.Search(name);
        }
    }
}
