using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CaloriesCounter.Core.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "������ ��")]
        [Range(18, 100, ErrorMessage = "³� ������� ���� �� 18 �� 100 ����")]
        public int Age { get; set; }

        [Required(ErrorMessage = "������ �����")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "������ ����")]
        [Range(30, 300, ErrorMessage = "���� ������� ���� �� 30 �� 300 ��")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "������ ����")]
        [Range(100, 250, ErrorMessage = "���� ������� ���� �� 100 �� 250 ��")]
        public double Height { get; set; }

        [Required(ErrorMessage = "������ ����� ���������")]
        public ActivityLevel ActivityLevel { get; set; }

        [Required(ErrorMessage = "������ ����")]
        public Goal Goal { get; set; }
    }
}
