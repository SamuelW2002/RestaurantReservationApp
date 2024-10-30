namespace Utility
{
    public static class EnumUtil
    {
        public static T ParseEnum<T>(string value) where T : Enum
        {
            if (Enum.TryParse(typeof(T), value, true, out var result))
            {
                return (T)result;
            }
            throw new ContractException($"Invalid value '{value}' for speciality type.");
        }
    }
}
