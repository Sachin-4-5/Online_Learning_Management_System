using System.Text.RegularExpressions;

namespace OnlineLearningManagementSystem.BLL.Helpers
{
    public static class ValidationHelper
    {
        private static readonly Regex EmailRegex =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static bool IsValidEmail(string email) =>
            !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);

        public static bool IsNonEmpty(string s) =>
            !string.IsNullOrWhiteSpace(s);

        public static bool IsPositive(int id) => id > 0;
    }
}
