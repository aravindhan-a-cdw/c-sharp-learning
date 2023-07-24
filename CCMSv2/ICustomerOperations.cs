interface ICustomerOperations
{
    public abstract static void ApplyCreditCard(Customer customer, Bank bank);
    public abstract static void ViewBalance(Customer customer, Bank bank);
    public abstract static void CustomerBlockCreditCard(Customer customer, Bank bank);
    public abstract static void PayForPurchase(Customer customer, Bank bank);
    public abstract static void DepositMoney(Customer customer, Bank bank);

}