using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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

    public class CheckBoxItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Checked = false;

        public CheckBoxItem()
        {

        }

        public CheckBoxItem(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
            this.Text = "";
        }

        public CheckBoxItem(string Name, string Value, string Text)
        {
            this.Name = Name;
            this.Value = Value;
            this.Text = Text;
        }

        public CheckBoxItem(string Name, string Value, bool Checked)
        {
            this.Name = Name;
            this.Value = Value;
            this.Text = "";
            this.Checked = Checked;
        }

        public CheckBoxItem(string Name, string Value, string Text, bool Checked)
        {
            this.Name = Name;
            this.Value = Value;
            this.Text = Text;
            this.Checked = Checked;
        }

        public string GetString(string Index = "")
        {
            string TmpChecked = (Checked) ? " checked" : "";
            string TmpIndex = (!string.IsNullOrEmpty(Index)) ? "$" + Index : "";

            return "<input type=\"checkbox\" name=\"" + Name + TmpIndex + "\" value=\"" + Value + "\" " + TmpChecked + "><label for=\"" + Name + TmpIndex + "\">" + Text + "</label>";
        }
    }

    public class CheckBoxItemIndex
    {
        public string UseIndexerFromZero { private set; get; } = "index_from_zero";
        public string UseIndexerFromOne { private set; get; } = "index_from_one";
    }

    public class CheckBoxItemCollection
    {
        private List<CheckBoxItem> CheckBoxItemList = new List<CheckBoxItem>();

        public void Add(string Name, string Value)
        {
            CheckBoxItemList.Add(new CheckBoxItem(Name, Value));
        }

        public void Add(string Name, string Value, string Text)
        {
            CheckBoxItemList.Add(new CheckBoxItem(Name, Value, Text));
        }

        public void Add(string Name, string Value, bool Selected)
        {
            CheckBoxItemList.Add(new CheckBoxItem(Name, Value, Selected));
        }

        public void Add(string Name, string Value, string Text, bool Selected)
        {
            CheckBoxItemList.Add(new CheckBoxItem(Name, Value, Text, Selected));
        }

        public void Delete(string Name)
        {
            List<CheckBoxItem> TmpCheckBoxItemList = new List<CheckBoxItem>();

            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name != Name)
                    TmpCheckBoxItemList.Add(item);
            }

            CheckBoxItemList = TmpCheckBoxItemList;
        }

        public void Empty()
        {
            CheckBoxItemList = new List<CheckBoxItem>();
        }

        public void ChangeText(string Name, string Text)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Text = Text;
                    break;
                }
            }
        }

        public void ChangeValue(string Name, string Value)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Value = Value;
                    break;
                }
            }
        }

        public void ChangeName(string Name, string NewName)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Name = NewName;
                    break;
                }
            }
        }


        // Overload
        public void ChangeName(string Name, string NewName, string Value)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Name = NewName;
                    item.Value = Value;
                    break;
                }
            }
        }

        // Overload
        public void ChangeName(string Name, string NewName, string Value, string Text)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Name = NewName;
                    item.Value = Value;
                    item.Text = Text;
                    break;
                }
            }
        }

        // Overload
        public void ChangeName(string Name, string NewName, string Value, string Text, bool Checked)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Name = NewName;
                    item.Value = Value;
                    item.Text = Text;
                    item.Checked = Checked;
                    break;
                }
            }
        }

        public void SetChecked(string Name)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                    item.Checked = true;
                else
                    item.Checked = false;
            }
        }

        public void SetCheckedIgnoreRest(string Name)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Checked = true;
                    break;
                }
            }
        }

        public void ClearChecked(string Name)
        {
            foreach (CheckBoxItem item in CheckBoxItemList)
            {
                if (item.Name == Name)
                {
                    item.Checked = false;
                    break;
                }
            }
        }

        public string GetString(string Index, bool SplitByNewLine = false)
        {
            bool UseIndexer = false;
            int i = 0;
            if (Index == "index_from_zero")
                UseIndexer = true;

            if (Index == "index_from_one")
            {
                i = 1;
                UseIndexer = true;
            }

            string ReturnValue = "";

            int ListCount = CheckBoxItemList.Count;
            foreach (CheckBoxItem item in CheckBoxItemList)
                ReturnValue += "<li>" + item.GetString((UseIndexer? (i++).ToString() : Index)) + "</li>" + ((SplitByNewLine && (ListCount-- > 1)) ? Environment.NewLine : "");

            string NewLine = (SplitByNewLine) ? Environment.NewLine : "";

            ReturnValue = "<ul>" + NewLine + ReturnValue + NewLine + "</ul>";

            return ReturnValue;
        }

        // Overload
        public string GetString(bool SplitByNewLine = false)
        {
            return GetString("", SplitByNewLine);
        }
    }
}
