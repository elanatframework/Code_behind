namespace CodeBehind
{
    internal static class StaticObject
    {
        private static bool StaticObjectHasInitialization { get; set; } = false;
        internal static bool PreventAccessDefaultAspx { get; private set; } = false;
        internal static string ViewPath { get; private set; }
        internal static string DefaultRole { get; private set; }
        internal static string ViewPlace { get; private set; }
        internal static bool UseDefaultController { get; private set; } = false;
        internal static string DefaultController { get; private set; }
        internal static bool UseSectionInDefaultController { get; private set; } = false;
        internal static bool SetBreakForDefaultController { get; private set; } = false;
        internal static char OsDirectorySplitter = OperatingSystem.IsWindows() ? '\\' : '/';

        internal static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();
            PreventAccessDefaultAspx = options.PreventAccessDefaultAspx;
            ViewPath = options.ViewPath;
            DefaultRole = options.DefaultRole;
            ViewPlace = options.WebFormsViewPlace;
            UseDefaultController = options.UseDefaultController;
            DefaultController = (options.AccessControllerByLowerCase || options.JustAccessControllerByLowerCase) ? options.DefaultController.ToLower() : options.DefaultController;
            UseSectionInDefaultController = options.UseSectionInDefaultController;
            SetBreakForDefaultController = options.SetBreakForDefaultController;

            StaticObjectHasInitialization = true;
        }
    }
}
