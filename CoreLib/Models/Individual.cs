using CoreLib.Enums;

namespace CoreLib.Models
{
    public class Individual : BaseEntity<int>
    {
        public string Name { get; set; }
        public RelativeType RelativeType { get; set; }
        public string Color { get; set; }
    }
}