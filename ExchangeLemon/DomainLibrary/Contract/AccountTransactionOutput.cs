namespace BlueLight.Main
{
    public class AccountTransactionOutput
    {
        public UserProfileDetail userProfileDetail { get; set; }
        public AccountTransaction accountTransaction { get; set; }
        public decimal Amount
        {
            get
            {
                return accountTransaction.Amount;
            }
        }
    }
}