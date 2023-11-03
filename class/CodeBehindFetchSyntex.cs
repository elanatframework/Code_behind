namespace CodeBehind
{
    public class CodeBehindFetchSyntex
    {
        public bool FindSyntex { get; private set; } = false;
        public int StartSyntex { get; private set; } = 0;
        public int EndSyntex { get; private set; } = 0;

        /// <param name="OpenSyntaxValue">A Two-Letter String Must Be Entered</param>
        /// <param name="CloseSyntaxValue">A Two-Letter String Must Be Entered</param>
        public string FetchInnerText(string FileText, string OpenSyntaxValue, string CloseSyntaxValue)
        {
            int Index = 0;
            string InnerText = "";
            int OpenedSyntexCount = 1;
            int CloseedSyntexCount = 0;

            while (Index < FileText.Length)
            {
                if (FileText[Index] == OpenSyntaxValue[0])
                    if (Index + 1 < FileText.Length)
                        if (FileText[Index + 1] == OpenSyntaxValue[1])
                        {
                            if (FindSyntex)
                                break;

                            FindSyntex = true;
                            StartSyntex = Index;

                            if (Index + 2 < FileText.Length)
                                Index += 2;

                            while (Index < FileText.Length)
                            {
                                if (FileText[Index] == OpenSyntaxValue[0])
                                    if (Index + 1 < FileText.Length)
                                        if (FileText[Index + 1] == OpenSyntaxValue[1])
                                            OpenedSyntexCount++;

                                if (FileText[Index] == CloseSyntaxValue[0])
                                    if (Index + 1 < FileText.Length)
                                        if (FileText[Index + 1] == CloseSyntaxValue[1])
                                            CloseedSyntexCount++;

                                if (OpenedSyntexCount <= CloseedSyntexCount)
                                    break;

                                InnerText += FileText[Index++];
                            }
                        }

                if (OpenedSyntexCount <= CloseedSyntexCount)
                {
                    EndSyntex = Index + 2;
                    break;
                }

                Index++;
            }

            return InnerText;
        }
    }
}