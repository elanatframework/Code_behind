namespace CodeBehind
{
    public static class StaticObject
    {
        private static bool StaticObjectHasInitialization { get; set; } = false;
        public static bool PreventAccessDefaultAaspx { get; private set; } = false;

        public static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();
            PreventAccessDefaultAaspx = options.PreventAccessDefaultAaspx;

            StaticObjectHasInitialization = true;
        }
    }
}