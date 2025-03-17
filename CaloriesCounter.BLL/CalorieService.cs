using CaloriesCounter.Core.Models;
using System.Reflection;

namespace CaloriesCounter.Web.Services
{
    public class CalorieService
    {
        public CalorieResult CalculateCalories(UserProfile userProfile)
        {
            var result = new CalorieResult
            {
                UserProfileId = userProfile.Id,
                CalculationDate = DateTime.UtcNow
            };

            result.BMR = userProfile.Gender == Gender.������
                ? 10 * userProfile.Weight + 6.25 * userProfile.Height - 5 * userProfile.Age + 5
                : 10 * userProfile.Weight + 6.25 * userProfile.Height - 5 * userProfile.Age - 161;

            result.TDEE = userProfile.ActivityLevel switch
            {
                ActivityLevel.������� => result.BMR * 1.2,
                ActivityLevel.�������������� => result.BMR * 1.375,
                ActivityLevel.��������������� => result.BMR * 1.55,
                ActivityLevel.��������������� => result.BMR * 1.725,
                ActivityLevel.��������������������� => result.BMR * 1.9,
                _ => result.BMR * 1.2
            };

            result.DailyCalories = userProfile.Goal switch
            {
                Goal.��������� => result.TDEE - 500,
                Goal.ϳ������� => result.TDEE,
                Goal.�������� => result.TDEE + 500,
                _ => result.TDEE
            };

            result.WeeklyCalories = result.DailyCalories * 7;

            GenerateWeeklyPlan(result);
            return result;
        }

        private static void GenerateWeeklyPlan(CalorieResult result)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                var dailyNutrition = new DailyNutrition
                {
                    Calories = result.DailyCalories,
                    Protein = (result.DailyCalories * 0.30) / 4,
                    Fat = (result.DailyCalories * 0.30) / 9,
                    Carbs = (result.DailyCalories * 0.40) / 4,
                    MealDistribution = new Dictionary<MealType, double>
                    {
                        { MealType.�������, result.DailyCalories * 0.30 },
                        { MealType.���, result.DailyCalories * 0.35 },
                        { MealType.������, result.DailyCalories * 0.25 },
                        { MealType.�������, result.DailyCalories * 0.10 }
                    }
                };

                result.WeeklyPlan[day] = dailyNutrition;
            }
        }
    }
}
