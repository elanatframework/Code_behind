namespace CodeBehind.HtmlData
{
    public class OptionTag
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected = false;

        public OptionTag()
        {

        }

        public OptionTag(string Value, string Text)
        {
            this.Value = Value;
            this.Text = Text;
        }

        public OptionTag(string Value, string Text, bool Selected)
        {
            this.Value = Value;
            this.Text = Text;
            this.Selected = Selected;
        }

        public string GetString()
        {
            string TmpSelected = (Selected) ? " selected" : "";
            return "<option value=\"" + Value + "\"" + TmpSelected + ">" + Text + "</option>";
        }
    }

    public class OptionTagCollection
    {
        private List<OptionTag> OptionTagList = new List<OptionTag>();

        public void Add(string Value, string Text)
        {
            OptionTagList.Add(new OptionTag(Value, Text));
        }

        public void Add(string Value, string Text, bool Selected)
        {
            OptionTagList.Add(new OptionTag(Value, Text, Selected));
        }

        public void Delete(string Value)
        {
            List<OptionTag> TmpOptionTagList = new List<OptionTag>();

            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value != Value)
                    TmpOptionTagList.Add(tag);
            }

            OptionTagList = TmpOptionTagList;
        }

        public void Empty()
        {
            OptionTagList = new List<OptionTag>();
        }

        public void ChangeText(string Value, string Text)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Text = Text;
                    break;
                }
            }
        }

        public void ChangeValue(string Value, string NewValue)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Value = NewValue;
                    break;
                }
            }
        }

        // Overload
        public void ChangeValue(string Value, string NewValue, string Text)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Value = NewValue;
                    tag.Text = Text;
                    break;
                }
            }
        }

        // Overload
        public void ChangeValue(string Value, string NewValue, string Text, bool Selected)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Value = NewValue;
                    tag.Text = Text;
                    tag.Selected = Selected;
                    break;
                }
            }
        }

        public void SetSelected(string Value)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                    tag.Selected = true;
                else
                    tag.Selected = false;
            }
        }

        public void SetSelectedIgnoreRest(string Value)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Selected = true;
                    break;
                }
            }
        }

        public void ClearSelected(string Value)
        {
            foreach (OptionTag tag in OptionTagList)
            {
                if (tag.Value == Value)
                {
                    tag.Selected = false;
                    break;
                }
            }
        }

        public string GetString(bool SplitByNewLine = false)
        {
            string ReturnValue = "";

            int ListCount = OptionTagList.Count;
            foreach (OptionTag tag in OptionTagList)
                ReturnValue += tag.GetString() + ((SplitByNewLine && (ListCount-- > 1)) ? Environment.NewLine : "");

            return ReturnValue;
        }
    }
}
