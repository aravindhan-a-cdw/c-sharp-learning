
using System.Collections.Generic;

class Customer
{
    public string name { get; }

    uint aadharNumber;

    static Dictionary<uint, Customer> customers = new();

    public int cardCount = 0;

    public uint getAadharNumber()
    {
        return aadharNumber;
    }

    public Customer(string customerName, uint aadhar)
    {
        name = customerName;
        aadharNumber = aadhar;
        customers.Add(aadhar, this);
    }


    public static Customer? getCustomer(uint aadharNumber)
    {
        if (customers.ContainsKey(aadharNumber))
            return customers[aadharNumber];
        return null;
    }

    public override string ToString()
    {
        return string.Format($"Customer<Name:{this.name}, Aadhar Number:{this.aadharNumber}>");
    }

    List<Account> accounts = new();

    public List<Account> getAccounts()
    {
        return accounts;
    }

    public void addAccount(Account newAccount)
    {
        accounts.Add(newAccount);
    }

}