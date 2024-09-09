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
        internal static string ErrorPagePathBeforeValue { get; private set; }
        internal static string ErrorPagePathAfterValue { get; private set; }


        internal static void SetValue()
        {
            if (StaticObjectHasInitialization)
                return;

            CodeBehindOptions options = new CodeBehindOptions();

            if (options.UseDefaultController)
            {
                DefaultController = options.DefaultController;

                if (options.PutTwoUnderlinesEqualToDashForController)
                    DefaultController = DefaultController.Replace("__", "-");
                if (DefaultController.StartsWith(options.IgnorePrefixController))
                    DefaultController = DefaultController.Remove(0, options.IgnorePrefixController.Length);
                if (DefaultController.EndsWith(options.IgnoreSuffixController))
                    DefaultController = DefaultController.GetTextBeforeLastValue(options.IgnoreSuffixController);

                DefaultController = (options.AccessControllerByLowerCase || options.JustAccessControllerByLowerCase) ? DefaultController.ToLower() : DefaultController;
            }

            PreventAccessDefaultAspx = options.PreventAccessDefaultAspx;
            ViewPath = options.ViewPath;
            DefaultRole = options.DefaultRole;
            ViewPlace = options.WebFormsViewPlace;
            UseDefaultController = options.UseDefaultController;          
            UseSectionInDefaultController = options.UseSectionInDefaultController;
            SetBreakForDefaultController = options.SetBreakForDefaultController;

            ErrorPagePathBeforeValue = options.ErrorPagePath.GetTextBeforeValue("{value}");
            ErrorPagePathAfterValue = options.ErrorPagePath.GetTextAfterValue("{value}");

            StaticObjectHasInitialization = true;
        }
    }
}
