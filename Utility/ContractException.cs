namespace Utility
{
    [Serializable]
    public class ContractException : Exception
    {
        public ContractException()
        {
        }

        public ContractException(string message)
            : base(message)
        {
        }
    }
}
