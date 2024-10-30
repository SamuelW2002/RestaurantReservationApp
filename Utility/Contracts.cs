namespace Utility
{
    public static class Contracts
    {
        public static void Require(bool precondition, string message = "")
        {
            if (!precondition)
            {
                throw new ContractException(message);
            }
        }
    }
}
