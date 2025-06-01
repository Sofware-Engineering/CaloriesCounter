using CaloriesCounter.Core.Models;
using CaloriesCounter.Web.Services;
using System;
using System.Reflection;
using Xunit;

namespace CaloriesCounter.Tests.Services
{
    public class CalorieServiceTests
    {
        private readonly CalorieService _calorieService;

        public CalorieServiceTests()
        {
            _calorieService = new CalorieService();
        }

        [Fact]
        public void CalculateCalories_ForMaleUser_ReturnsCorrectBMR_TDEE_Calories()
        {
            // Arrange
            var user = new UserProfile
            {
                Id = 1,
                Age = 30,
                Weight = 70, // kg
                Height = 175, // cm
                Gender = Gender.Чоловік,
                ActivityLevel = ActivityLevel.ПомірнаАктивність,
                Goal = Goal.Підтримка
            };

            // Act
            var result = _calorieService.CalculateCalories(user);

            // Assert
            double expectedBMR = 10 * 70 + 6.25 * 175 - 5 * 30 + 5; // 1661.25
            double expectedTDEE = expectedBMR * 1.55;
            double expectedDaily = expectedTDEE;
            double expectedWeekly = expectedDaily * 7;

            Assert.Equal(expectedBMR, result.BMR, 2);
            Assert.Equal(expectedTDEE, result.TDEE, 2);
            Assert.Equal(expectedDaily, result.DailyCalories, 2);
            Assert.Equal(expectedWeekly, result.WeeklyCalories, 2);
        }

        [Fact]
        public void CalculateCalories_GeneratesWeeklyPlan_WithCorrectMacroDistribution()
        {
            // Arrange
            var user = new UserProfile
            {
                Id =2,
                Age = 25,
                Weight = 60,
                Height = 165,
                Gender = Gender.Жінка,
                ActivityLevel = ActivityLevel.ЛегкаАктивність,
                Goal = Goal.Схуднення
            };

            // Act
            var result = _calorieService.CalculateCalories(user);

            // Assert
            foreach (var day in result.WeeklyPlan)
            {
                var nutrition = day.Value;
                double calories = result.DailyCalories;

                Assert.Equal(calories * 0.30 / 4, nutrition.Protein, 2);
                Assert.Equal(calories * 0.30 / 9, nutrition.Fat, 2);
                Assert.Equal(calories * 0.40 / 4, nutrition.Carbs, 2);

                Assert.Equal(calories * 0.30, nutrition.MealDistribution[MealType.Сніданок], 2);
                Assert.Equal(calories * 0.35, nutrition.MealDistribution[MealType.Обід], 2);
                Assert.Equal(calories * 0.25, nutrition.MealDistribution[MealType.Вечеря], 2);
                Assert.Equal(calories * 0.10, nutrition.MealDistribution[MealType.Перекус], 2);
            }
        }
    }
}
