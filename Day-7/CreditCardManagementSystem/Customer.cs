
using System.Collections;

class Customer
{
    string name;

    uint aadharNumber;

    static Hashtable customers = new Hashtable();

    public int cardCount = 0;

    public uint getAadharNumber()
    {
        return aadharNumber;
    }

    private Customer(string customerName, uint aadhar)
    {
        name = customerName;
        aadharNumber = aadhar;
        if (customers.Contains(aadhar))
            customers.Add(aadhar, this);
    }

    public static Customer getCustomer(string customerName, uint aadharNumber)
    {
        if (customers.Contains(aadharNumber))
            return (Customer)customers[aadharNumber];
        Customer newCustomer = new Customer(customerName, aadharNumber);
        customers.Add(aadharNumber, newCustomer);
        return newCustomer;
    }

    public override string ToString()
    {
        return string.Format($"Customer<Name:{this.name}, Aadhar Number:{this.aadharNumber}>");
    }

    ArrayList accounts = new ArrayList();

    public ArrayList getAccounts()
    {
        return accounts;
    }

    public void addAccount(Account newAccount)
    {
        accounts.Add(newAccount);
    }

}