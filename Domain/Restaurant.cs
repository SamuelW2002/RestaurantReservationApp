using Utility;

namespace Domain
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public SpecialityEnum Speciality { get; set; }

        public Restaurant() { }

        public Restaurant(string name, string speciality)
        {
            Id = new Guid();
            Name = name;
            Speciality = EnumUtil.ParseEnum<SpecialityEnum>(speciality);
        }
    }
}
