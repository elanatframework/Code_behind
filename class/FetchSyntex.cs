using System.Net;

namespace CodeBehind
{
    internal class Apostrophe
    {
        internal bool IsBetweenApostrophe(string text, int index)
        {
            if (index + 1 >= text.Length)
                return false;

            bool InsideApostrophe = false;
            bool FindNextLine = false;
            char QuoteChar = '\0';

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (c == '\n' && InsideApostrophe)
                {
                    FindNextLine = true;
                }

                if (c == '\'' && !InsideApostrophe)
                {
                    InsideApostrophe = true;
                    QuoteChar = c;
                }
                else if (c == '"' && !InsideApostrophe)
                {
                    InsideApostrophe = true;
                    QuoteChar = c;
                }
                else if (c == '`' && !InsideApostrophe)
                {
                    InsideApostrophe = true;
                    QuoteChar = c;
                }
                else if (c == QuoteChar && InsideApostrophe)
                {
                    InsideApostrophe = false;
                    FindNextLine = false;
                    QuoteChar = '\0';
                }
                else if (i == index && InsideApostrophe)
                {
                    bool IsLastCharInLine = false;

                    if ((i + 1) < text.Length)
                        if (text[i + 1] == '\n')
                            IsLastCharInLine = true;

                    return !FindNextLine && !IsLastCharInLine;
                }
            }

            return false;
        }
    }

    internal class FullTrim
    {
        internal string FullTrimInStartOverBackslash(string Text)
        {
            if (Text.Length < 1)
                return Text;

            string ReturnValue = Text;
            bool IsTrue = true;
            while (IsTrue)
            {
                if (Text.Length < 1)
                    return "";

                if (ReturnValue[0] != ' ' && ReturnValue[0] != '\\')
                    break;

                while (ReturnValue[0] == ' ')
                {
                    ReturnValue = ReturnValue.Remove(0, 1);
                    if (Text.Length < 1)
                        return "";
                }


                while (ReturnValue[0] == '\\')
                {
                    if (ReturnValue.Length > 1)
                    {
                        if (ReturnValue[1] == 'n' || ReturnValue[1] == 't' || ReturnValue[1] == 'r')
                            ReturnValue = ReturnValue.Remove(0, 2);
                        else
                        {
                            IsTrue = false;
                            break;
                        }
                    }
                    else
                    {
                        IsTrue = false;
                        break;
                    }
                }
            }

            return ReturnValue;
        }

        internal string FullTrimInStart(string Text)
        {
            if (Text.Length < 1)
                return Text;

            string ReturnValue = Text;

            while (ReturnValue[0] == ' ' || ReturnValue[0] == '\n' || ReturnValue[0] == '\t' || ReturnValue[0] == '\r')
            {
                ReturnValue = ReturnValue.Remove(0, 1);
                if (ReturnValue.Length < 1)
                    return "";
            }

            return ReturnValue;
        }

        internal string FullTrimInEndOverBackslash(string Text)
        {
            if (Text.Length < 1)
                return Text;

            string ReturnValue = Text;
            bool IsTrue = true;
            while (IsTrue)
            {
                if (ReturnValue[ReturnValue.Length - 1] != ' ' && ReturnValue[ReturnValue.Length - 1] != 'n' && ReturnValue[ReturnValue.Length - 1] != 't' && ReturnValue[ReturnValue.Length - 1] != 'r')
                    break;

                while (ReturnValue[ReturnValue.Length - 1] == ' ')
                    ReturnValue = ReturnValue.Remove(ReturnValue.Length - 1, 1);

                while (ReturnValue[ReturnValue.Length - 1] == 'n' || ReturnValue[ReturnValue.Length - 1] == 't' || ReturnValue[ReturnValue.Length - 1] == 'r')
                {
                    if (ReturnValue.Length > 1)
                    {
                        if (ReturnValue[ReturnValue.Length - 2] == '\\')
                            ReturnValue = ReturnValue.Remove(ReturnValue.Length - 2, 2);
                        else
                        {
                            IsTrue = false;
                            break;
                        }
                    }
                    else
                    {
                        IsTrue = false;
                        break;
                    }
                }
            }

            return ReturnValue;
        }

        internal string FullTrimInEnd(string Text)
        {
            if (Text.Length < 1)
                return Text;

            string ReturnValue = Text;

            while (ReturnValue[ReturnValue.Length - 1] == ' ' || ReturnValue[ReturnValue.Length - 1] == '\n' || ReturnValue[ReturnValue.Length - 1] == '\t' || ReturnValue[ReturnValue.Length - 1] == '\r')
            {
                ReturnValue = ReturnValue.Remove(ReturnValue.Length - 1, 1);
                if (ReturnValue.Length < 1)
                    return "";
            }

            return ReturnValue;
        }

        internal string FullTrimAllOverBackslash(string Text)
        {
            return FullTrimInStartOverBackslash(FullTrimInEndOverBackslash(Text));
        }

        internal string FullTrimAll(string Text)
        {
            return FullTrimInStart(FullTrimInEnd(Text));
        }
    }

    internal class CodeBehindFetchStandardSyntex
    {
        internal bool FindSyntex { get; private set; } = false;
        internal int StartSyntex { get; private set; } = 0;
        internal int EndSyntex { get; private set; } = 0;

        /// <param name="OpenSyntaxValue">A Two-Letter String Must Be Entered</param>
        /// <param name="CloseSyntaxValue">A Two-Letter String Must Be Entered</param>
        internal string FetchInnerText(string FileText, string OpenSyntaxValue, string CloseSyntaxValue)
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

    internal class CodeBehindFetchRazorSyntex
    {
        internal int RazorIndexLength { get; private set; }
        internal HtmlData.NameValueCollection AddedTextForEndedCharacter;
        internal string FetchSyntexWithEndedCharacter(string BlockText)
        {
            RazorIndexLength = 0;
            AddedTextForEndedCharacter = new HtmlData.NameValueCollection();

            if (BlockText.Length < 3)
                return "";

            if (BlockText[0] != '@')
                return "";
            
            Apostrophe apostrophe = new Apostrophe();

            char SecondValue = BlockText[1];

            int Index = 2;
            string InnerText = "";
            int OpenedSyntexCount = 1;
            int CloseedSyntexCount = 0;

            if (SecondValue == '(')
            {
                while (Index < BlockText.Length)
                {
                    if (BlockText[Index] == ')')
                        if(!apostrophe.IsBetweenApostrophe(BlockText, Index))
                            CloseedSyntexCount++;

                    if (BlockText[Index] == '(')
                        if (!apostrophe.IsBetweenApostrophe(BlockText, Index))
                            OpenedSyntexCount++;
                        
                    if (OpenedSyntexCount <= CloseedSyntexCount)
                        break;

                    InnerText += BlockText[Index];

                    Index++;
                }

                RazorIndexLength = Index + 1;

                // Encode Between Double Quotes 
                if (InnerText.Length > 2)
                    if (InnerText[0] == '"' && InnerText[InnerText.Length - 1] == '"')
                        if (!ExistDoubleQuotes(InnerText.Substring(1, InnerText.Length - 2)))
                        {
                            InnerText = InnerText.Remove(0, 1);
                            InnerText = InnerText.Remove(InnerText.Length - 1, 1);
                            InnerText = InnerText.Replace('\\' + '"'.ToString(), '"'.ToString());

                            InnerText = '"' + WebUtility.HtmlEncode(InnerText) + '"';
                        }

                return InnerText;
            }

            if (SecondValue == '{')
            {
                // Fetch Block 
                while (Index < BlockText.Length)
                {
                    if (BlockText[Index] == '}')
                        if (!apostrophe.IsBetweenApostrophe(BlockText, Index))
                            CloseedSyntexCount++;

                    if (BlockText[Index] == '{')
                        if (!apostrophe.IsBetweenApostrophe(BlockText, Index))
                            OpenedSyntexCount++;

                    if (OpenedSyntexCount <= CloseedSyntexCount)
                        break;

                    InnerText += BlockText[Index];

                    Index++;
                }


                string[] InnerTextLines = InnerText.Split('\n');
                FullTrim ft = new FullTrim();

                for (int i = 0; i < InnerTextLines.Length; i++)
                {
                    string Line = InnerTextLines[i] + '\n';

                    if (ft.FullTrimInStart(Line).Length > 0)
                        if (ft.FullTrimInStart(Line)[0] == '@')
                        {
                            string TmpLine = ft.FullTrimInStart(Line);

                            if (TmpLine.Length > 1)
                            {
                                if (TmpLine[1] == ':')
                                {
                                    AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(TmpLine.GetTextAfterValue("@:")).GetList());
                                    continue;
                                }
                            }
                        }

                    if (ft.FullTrimInStart(Line).Length > 0)
                    {
                        if (ft.FullTrimInStart(Line)[0] == '<')
                        {
                            // Fetch Single Line Text Tag
                            string TmpLine = ft.FullTrimAll(Line);
                            if (TmpLine.StartsWith("<text>") && TmpLine.EndsWith("</text>"))
                            {
                                AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(TmpLine.GetTextAfterValue("<text>").GetTextBeforeLastValue("</text>")).GetList());
                                continue;
                            }

                            // Fetch Multi Line Text Tag
                            if (TmpLine.StartsWith("<text>") && !TmpLine.EndsWith("</text>"))
                            {
                                AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(TmpLine.GetTextAfterValue("<text>")).GetList());
                                i++;
                                for (; i < InnerTextLines.Length; i++)
                                {
                                    string TextLine = InnerTextLines[i];

                                    if (ft.FullTrimInEnd(TextLine).EndsWith("</text>"))
                                    {
                                        AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(TextLine.GetTextBeforeLastValue("</text>")).GetList());
                                        break;
                                    }

                                    AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(TextLine + '\n').GetList());
                                }

                                continue;
                            }

                            AddedTextForEndedCharacter.AddList(FetchExpressionsInLine(Line).GetList());
                            continue;
                        }
                    }

                    AddedTextForEndedCharacter.Add("add_code", Line);
                }

                RazorIndexLength = Index + 2;
            }

            return "";
        }

        internal string FetchSyntexWithEndedCharacterText(string BlockText)
        {
            if (BlockText.Length < 3)
                return "";

            if (BlockText[0] != '@')
                return "";

            Apostrophe apostrophe = new Apostrophe();

            char SecondValue = BlockText[1];

            int Index = 2;
            string InnerText = "";
            int OpenedSyntexCount = 1;
            int CloseedSyntexCount = 0;

            if (SecondValue != '{')
                return "";

            // Fetch Block 
            while (Index < BlockText.Length)
            {
                if (BlockText[Index] == '}')
                    if (!apostrophe.IsBetweenApostrophe(BlockText, Index))
                        CloseedSyntexCount++;

                if (BlockText[Index] == '{')
                    if (!apostrophe.IsBetweenApostrophe(BlockText, Index))
                        OpenedSyntexCount++;

                if (OpenedSyntexCount <= CloseedSyntexCount)
                    break;

                InnerText += BlockText[Index];

                Index++;
            }

            return InnerText;
        }

        internal int ExpressionsIndexLength { get; private set; }
        internal string FetchExpressions(string Expression)
        {
            ExpressionsIndexLength = 0;
            Apostrophe apostrophe = new Apostrophe();

            int Index = 0;
            string InnerText = "";
            bool StartParentheses = false;
            bool ExistAwait = false;
            int AwaitLength = 0;
            int OpenedSyntexCount = 0;
            int CloseedSyntexCount = 0;

            if (Expression.Length < 2)
                return Expression;

            Expression = Expression.Remove(0,1);

            // Add Await
            if (Expression.Length > 6)
                if (Expression.Substring(0,6) == "await")
                    if (Expression[6] == '/' || Expression[6] == ' ')
                    {
                        int ExpressionLength = Expression.Length;

                        FullTrim ft = new FullTrim();
                        Expression = ft.FullTrimInStart(Expression.Remove(0, 6));

                        AwaitLength = ExpressionLength - Expression.Length;

                        ExistAwait = true;
                    }


            while (Index < Expression.Length)
            {
                if (Expression[Index] == ')')
                    if (!apostrophe.IsBetweenApostrophe(Expression, Index))
                        CloseedSyntexCount++;

                if (Expression[Index] == '(')
                    if (!apostrophe.IsBetweenApostrophe(Expression, Index))
                    {
                        StartParentheses = true;
                        OpenedSyntexCount++;
                    }

                if ((OpenedSyntexCount <= CloseedSyntexCount) && StartParentheses)
                {
                    InnerText += ')';
                    StartParentheses = false;
                    OpenedSyntexCount = 0;
                    CloseedSyntexCount = 0;
                    Index++;

                    if (Index + 1 < Expression.Length)
                        if (Expression[Index] == '.')
                        {
                            InnerText += '.';
                            Index++;
                            continue;
                        }

                    break;
                }

                if (!char.IsLetter(Expression[Index]) && !char.IsNumber(Expression[Index]) && Expression[Index] != '.' && Expression[Index] != '_' && Expression[Index] != '-' && !StartParentheses && !apostrophe.IsBetweenApostrophe(Expression, Index))
                    break;

                InnerText += Expression[Index];

                Index++;
            }

            ExpressionsIndexLength = Index + AwaitLength + 1;

            if (ExistAwait)
                InnerText = "await " + InnerText;

            return InnerText;
        }

        private HtmlData.NameValueCollection FetchExpressionsInLine(string LineText)
        {
            HtmlData.NameValueCollection NameValues = new HtmlData.NameValueCollection();
            string MyTextCode = "";
            string WriteText = "";

            for (int i = 0; i < LineText.Length; i++)
            {
                if (LineText[i] == '@')
                {
                    if (i + 1 < LineText.Length)
                        if (LineText[i + 1] == '@')
                        {
                            WriteText += "@";
                            i++;
                            continue;
                        }

                    if (!string.IsNullOrEmpty(MyTextCode))
                        MyTextCode += " + " + GetWriteText(WriteText);
                    else
                        MyTextCode += GetWriteText(WriteText);

                    WriteText = "";

                    if (LineText[i + 1] == '(')
                    {
                        CodeBehindFetchRazorSyntex TmpSyntex = new CodeBehindFetchRazorSyntex();
                        string WriteCode = TmpSyntex.FetchSyntexWithEndedCharacter(LineText.Substring(i));

                        if (!string.IsNullOrEmpty(WriteCode))
                        {
                            if (!string.IsNullOrEmpty(MyTextCode))
                                MyTextCode += " + " + WriteCode;
                            else
                                MyTextCode += WriteCode;
                        }

                        i += TmpSyntex.RazorIndexLength - 1;
                        continue;
                    }
                    else
                    {
                        string WriteCode = FetchExpressions(LineText.Substring(i));

                        if (!string.IsNullOrEmpty(WriteCode))
                        {
                            if (!string.IsNullOrEmpty(MyTextCode))
                                MyTextCode += " + " + WriteCode;
                            else
                                MyTextCode += WriteCode;
                        }

                        i += ExpressionsIndexLength - 1;
                    }
                }
                else
                    WriteText += LineText[i];
            }

            if (!string.IsNullOrEmpty(WriteText))
            {
                if (!string.IsNullOrEmpty(MyTextCode))
                    MyTextCode += " + " + GetWriteText(WriteText);
                else
                    MyTextCode += GetWriteText(WriteText);
            }
            if (!string.IsNullOrEmpty(MyTextCode))
                NameValues.Add("write_code", MyTextCode);

            return NameValues;
        }

        private string GetWriteText(string Text)
        {
            if (Text.Length > 0)
            {
                Text = Text.Replace("\\", "\\\\");

                Text = Text.Replace("\"", @"\" + "\"");

                Text = Text.Replace('\n'.ToString(), "\\" + "n");

                return  "\"" + Text + "\"";
            }
            else
                return "";
        }

        private bool ExistDoubleQuotes(string Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '"')
                    if (i > 0)
                    {
                        if (Text[i - 1] != '\\')
                            return true;
                    }
                    else
                        return true;
            }

            return false;
        }
    }
}
