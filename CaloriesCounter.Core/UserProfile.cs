using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CaloriesCounter.Core.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "¬каж≥ть в≥к")]
        [Range(18, 100, ErrorMessage = "¬≥к повинен бути в≥д 18 до 100 рок≥в")]
        public int Age { get; set; }

        [Required(ErrorMessage = "¬каж≥ть стать")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "¬каж≥ть вагу")]
        [Range(30, 300, ErrorMessage = "¬ага повинна бути в≥д 30 до 300 кг")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "¬каж≥ть зр≥ст")]
        [Range(100, 250, ErrorMessage = "«р≥ст повинен бути в≥д 100 до 250 см")]
        public double Height { get; set; }

        [Required(ErrorMessage = "¬каж≥ть р≥вень активност≥")]
        public ActivityLevel ActivityLevel { get; set; }

        [Required(ErrorMessage = "¬каж≥ть мету")]
        public Goal Goal { get; set; }
    }
}
