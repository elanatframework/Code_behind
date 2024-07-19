namespace CodeBehind
{
    public static class StaticObject
    {
        private static bool StaticObjectHasInitialization { get; set; } = false;
        public static bool PreventAccessDefaultAspx { get; private set; } = false;
        public static string ViewPath { get; private set; }
        public static string DefaultRole { get; private set; }
        public static string ViewPlace { get; private set; }

        public static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();
            PreventAccessDefaultAspx = options.PreventAccessDefaultAspx;
            ViewPath = options.ViewPath;
            DefaultRole = options.DefaultRole;
            ViewPlace = options.WebFormsViewPlace;

            StaticObjectHasInitialization = true;
        }
    }
}
