namespace CodeBehind
{
    public static class StaticObject
    {
        private static bool StaticObjectHasInitialization { get; set; } = false;
        public static bool PreventAccessDefaultAspx { get; private set; } = false;
        public static string ViewPath { get; private set; }
        public static string DefaultRole { get; private set; }
        public static string ViewPlace { get; private set; }
        public static bool UseDefaultController { get; private set; } = false;
        public static string DefaultController { get; private set; }
        public static bool UseSectionInDefaultController { get; private set; } = false;
        public static bool SetBreakForDefaultController { get; private set; } = false;
        public static char OsDirectorySplitter = OperatingSystem.IsWindows() ? '\\' : '/';

        public static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();
            PreventAccessDefaultAspx = options.PreventAccessDefaultAspx;
            ViewPath = options.ViewPath;
            DefaultRole = options.DefaultRole;
            ViewPlace = options.WebFormsViewPlace;
            UseDefaultController = options.UseDefaultController;
            DefaultController = options.DefaultController;
            UseSectionInDefaultController = options.UseSectionInDefaultController;
            SetBreakForDefaultController = options.SetBreakForDefaultController;

            StaticObjectHasInitialization = true;
        }
    }
}
