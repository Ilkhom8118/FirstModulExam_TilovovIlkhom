namespace FirstModeulExam
{
    internal class Program
    {
        static void Main(string[] args)
        {


        }

        // ==================== 1 - Variant ====================

        // ==================== 1 - Savol ====================

        public static int EvenNum(List<int> nums)
        {
            var count = 0;
            foreach (var num in nums)
            {
                if (num % 2 == 0)
                {
                    count++;
                }
            }
            return count;
        }

        // ==================== 2 - Savol ====================

        public static bool FiveAlpha(List<string> str)
        {
            var countAlpha = 0;
            foreach (var alpha in str)
            {
                var count = 0;
                if (alpha == alpha.ToUpper())
                {
                    count++;
                }
            }
            if (countAlpha == 5)
            {
                return true;
            }
            return false;
        }

        // ======================== 3 - savol ========================

        public static string threeAlpha(string str)
        {
            return str.Substring(0, 3);
        }


        //======================== 4 - savol ========================

        public static bool FirstPDP(string str)
        {
            return str.Substring(0, 3) == "PDP" ? true : false;
        }
    }
}
