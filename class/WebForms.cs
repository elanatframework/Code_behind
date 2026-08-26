// WebForms.cs 2.1 - The Back-End Part of WebForms Core Technology, Owned by Elanat (https://elanat.net)
// Compatible with WebFormsJS version 2.1

using System.Text;

namespace CodeBehind
{
    public class WebForms
    {
        private const char GS = (char)29;
        private const char US = (char)31;

        private StringBuilder WebFormsData = new StringBuilder();

        internal void Add(string Name, string Value)
        {
            if (WebFormsData.Length > 0)
                WebFormsData.Append('\n');

            WebFormsData.Append(Name);
            WebFormsData.Append('=');
            WebFormsData.Append(Value);
        }

        internal void Add(string Name)
        {
            if (WebFormsData.Length > 0)
                WebFormsData.Append('\n');

            WebFormsData.Append(Name);
        }

        internal void AddToUp(string name, string value)
        {
            string line = $"{name}={value}";

            if (WebFormsData.Length > 0)
                line += "\n";

            WebFormsData.Insert(0, line);
        }

        internal void AddToUp(string name)
        {
            string line = name;

            if (WebFormsData.Length > 0)
                line += "\n";

            WebFormsData.Insert(0, line);
        }

        internal string GetLineByIndex(int Index)
        {
            if (WebFormsData.Length == 0)
                return "";

            string data = WebFormsData.ToString();
            string[] lines = data.Split('\n');

            if (Index < 0)
                Index = lines.Length + Index;

            if (Index < 0 || Index >= lines.Length)
                return "";

            return lines[Index];
        }

        internal void UpdateLineByIndex(int Index, string Name, string Value)
        {
            if (WebFormsData.Length == 0)
                return;

            string data = WebFormsData.ToString();
            string[] lines = data.Split('\n');

            if (Index < 0)
                Index = lines.Length + Index;

            if (Index < 0 || Index >= lines.Length)
                return;

            lines[Index] = Name + (string.IsNullOrEmpty(Value) ? "" : "=" + Value);

            WebFormsData.Clear();
            WebFormsData.Append(string.Join("\n", lines));
        }

        // For Extension
        public void AddLine(string Name, string Value) => Add(Name, Value);

        // Add
        // Creates the Data if it does not exist; otherwise, Appends the New Value to the Existing Value.
        public void AddId(string InputPlace, string Id) => Add("ai" + InputPlace, Id);
        public void AddName(string InputPlace, string Name) => Add("an" + InputPlace, Name);
        public void AddValue(string InputPlace, string Value) => Add("av" + InputPlace, Value);
        public void AddClass(string InputPlace, string Class) => Add("ac" + InputPlace, Class);
        public void AddStyle(string InputPlace, string Style) => Add("as" + InputPlace, Style);
        public void AddStyle(string InputPlace, string Name, string Value) => Add("as" + InputPlace, Name + ':' + Value);
        public void AddOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("ao" + InputPlace, Value + GS + Text + (Selected ? GS + "1" : ""));
        public void AddCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("ak" + InputPlace, Value + GS + Text + (Checked ? GS + "1" : ""));
        public void AddTitle(string InputPlace, string Title) => Add("al" + InputPlace, Title);
        public void AddLabel(string InputPlace, string Label) => Add("aA" + InputPlace, Label);
        public void AddText(string InputPlace, string Text) => Add("at" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddTextToUp(string InputPlace, string Text) => Add("pt" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => Add("aa" + InputPlace, Attribute + GS + ((Splitter != '\0') ? Splitter.ToString() : "") + (!string.IsNullOrEmpty(Value) ? GS + Value : ""));
        public void AddTag(string InputPlace, string TagName, string Id = "") => Add("nt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? GS + Id : ""));
        public void AddTagToUp(string InputPlace, string TagName, string Id = "") => Add("ut" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? GS + Id : ""));
        public void AddTagBefore(string InputPlace, string TagName, string Id = "") => Add("bt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? GS + Id : ""));
        public void AddTagAfter(string InputPlace, string TagName, string Id = "") => Add("ft" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? GS + Id : ""));
        public void AddHidden(string InputPlace, string Name, string Value, string Id = "") => Add("ah" + InputPlace, Name + GS + Value + (!string.IsNullOrEmpty(Id) ? GS + Id : ""));

        // Set
        // Creates the Data if it does not exist; otherwise, Replaces the Existing Value with the New Value.
        public void SetId(string InputPlace, string Id) => Add("si" + InputPlace, Id);
        public void SetName(string InputPlace, string Name) => Add("sn" + InputPlace, Name);
        public void SetValue(string InputPlace, string Value) => Add("sv" + InputPlace, Value);
        public void SetClass(string InputPlace, string Class) => Add("sc" + InputPlace, Class);
        public void SetStyle(string InputPlace, string Style) => Add("ss" + InputPlace, Style);
        public void SetStyle(string InputPlace, string Name, string Value) => Add("ss" + InputPlace, Name + ':' + Value);
        public void SetOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("so" + InputPlace, Value + GS + Text + (Selected ? GS + "1" : ""));
        public void SetChecked(string InputPlace, bool Checked = false) => Add("sk" + InputPlace, Checked ? "1" : "0");
        public void SetCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("sk" + InputPlace, Value + GS + Text + (Checked ? GS + "1" : ""));
        public void SetTitle(string InputPlace, string Title) => Add("sl" + InputPlace, Title);
        public void SetLabel(string InputPlace, string Label) => Add("sA" + InputPlace, Label);
        public void SetText(string InputPlace, string Text) => Add("st" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void SetAttribute(string InputPlace, string Attribute, string Value = "") => Add("sa" + InputPlace, Attribute + GS + (!string.IsNullOrEmpty(Value) ? GS + Value : ""));
        public void SetWidth(string InputPlace, string Width) => Add("sw" + InputPlace, Width);
        public void SetWidth(string InputPlace, int Width) => SetWidth(InputPlace, Width.ToString() + "px");
        public void SetHeight(string InputPlace, string Height) => Add("sh" + InputPlace, Height);
        public void SetHeight(string InputPlace, int Height) => SetHeight(InputPlace, Height.ToString() + "px");
        public void SetBackgroundColor(string InputPlace, string Color) => Add("bc" + InputPlace, Color);
        public void SetTextColor(string InputPlace, string Color) => Add("tc" + InputPlace, Color);
        public void SetFontName(string InputPlace, string Name) => Add("fn" + InputPlace, Name);
        public void SetFontSize(string InputPlace, string Size) => Add("fs" + InputPlace, Size);
        public void SetFontSize(string InputPlace, int Size) => Add("fs" + InputPlace, Size.ToString() + "px");
        public void SetFontBold(string InputPlace, bool Bold) => Add("fb" + InputPlace, Bold ? "1" : "0");
        public void SetVisible(string InputPlace, bool Visible) => Add("vi" + InputPlace, Visible ? "1" : "0");
        public void SetTextAlign(string InputPlace, string Align) => Add("ta" + InputPlace, Align);
        public void SetReadOnly(string InputPlace, bool ReadOnly) => Add("sr" + InputPlace, ReadOnly ? "1" : "0");
        public void SetDisabled(string InputPlace, bool Disabled) => Add("sd" + InputPlace, Disabled ? "1" : "0");
        public void SetFocus(string InputPlace, bool Focus) => Add("sf" + InputPlace, Focus ? "1" : "0");
        public void SetMinLength(string InputPlace, string Length) => Add("mn" + InputPlace, Length);
        public void SetMinLength(string InputPlace, int Length) => SetMinLength(InputPlace, Length.ToString());
        public void SetMaxLength(string InputPlace, string Length) => Add("mx" + InputPlace, Length);
        public void SetMaxLength(string InputPlace, int Length) => Add(InputPlace, Length.ToString());
        public void SetSelectedValue(string InputPlace, string Value) => Add("ts" + InputPlace, Value);
        public void SetSelectedIndex(string InputPlace, string Index) => Add("ti" + InputPlace, Index);
        public void SetSelectedIndex(string InputPlace, int Index) => SetSelectedIndex(InputPlace, Index.ToString());
        public void SetCheckedValue(string InputPlace, string Value, bool Selected) => Add("ks" + InputPlace, Value + GS + (Selected ? "1" : "0"));
        public void SetCheckedIndex(string InputPlace, string Index, bool Selected) => Add("ki" + InputPlace, Index + GS + (Selected ? "1" : "0"));
        public void SetCheckedIndex(string InputPlace, int Index, bool Selected) => SetCheckedIndex(InputPlace, Index.ToString(), Selected);

        // Insert
        // Creates the Data only if it does not exist; otherwise, does nothing.
        public void InsertId(string InputPlace, string Id) => Add("ii" + InputPlace, Id);
        public void InsertName(string InputPlace, string Name) => Add("in" + InputPlace, Name);
        public void InsertValue(string InputPlace, string Value) => Add("iv" + InputPlace, Value);
        public void InsertClass(string InputPlace, string Class) => Add("ic" + InputPlace, Class);
        public void InsertStyle(string InputPlace, string Style) => Add("is" + InputPlace, Style);
        public void InsertStyle(string InputPlace, string Name, string Value) => Add("is" + InputPlace, Name + ':' + Value);
        public void InsertOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("io" + InputPlace, Value + GS + Text + (Selected ? GS + "1" : ""));
        public void InsertCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("ik" + InputPlace, Value + GS + Text + (Checked ? GS + "1" : ""));
        public void InsertTitle(string InputPlace, string Title) => Add("il" + InputPlace, Title);
        public void InsertLabel(string InputPlace, string Label) => Add("iA" + InputPlace, Label);
        public void InsertText(string InputPlace, string Text) => Add("it" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void InsertAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => Add("ia" + InputPlace, Attribute + GS + ((Splitter != '\0') ? Splitter.ToString() : "") + (!string.IsNullOrEmpty(Value) ? GS + Value : ""));
        
        // Delete
        public void DeleteId(string InputPlace) => Add("di" + InputPlace);
        public void DeleteName(string InputPlace) => Add("dn" + InputPlace);
        public void DeleteValue(string InputPlace) => Add("dv" + InputPlace);
        public void DeleteClass(string InputPlace, string ClassName) => Add("dc" + InputPlace, ClassName);
        public void DeleteStyle(string InputPlace, string StyleName) => Add("ds" + InputPlace, StyleName);
        public void DeleteOptionTag(string InputPlace, string Value) => Add("do" + InputPlace, Value);
        public void DeleteAllOptionTag(string InputPlace) => Add("do" + InputPlace, "*");
        public void DeleteCheckBoxTag(string InputPlace, string Value) => Add("dk" + InputPlace, Value);
        public void DeleteAllCheckBoxTag(string InputPlace) => Add("dk" + InputPlace, "*");
        public void DeleteTitle(string InputPlace) => Add("dl" + InputPlace);
        public void DeleteLabel(string InputPlace) => Add("dA" + InputPlace);
        public void DeleteText(string InputPlace) => Add("dt" + InputPlace);
        public void DeleteAttribute(string InputPlace, string Attribute) => Add("da" + InputPlace, Attribute);
        public void Delete(string InputPlace) => Add("de" + InputPlace);
        public void DeleteParent(string InputPlace) => Add("dp" + InputPlace);

        // Tag
        public void SwapTag(string InputPlace, string OutputPlace) => Add("sp" + InputPlace, OutputPlace);
        public void SetReflection(string InputPlace, string Tag) => Add("sR" + InputPlace, Tag);
        public void SetReflectionByOutputPlace(string InputPlace, string OutputPlace) => Add("iR" + InputPlace, OutputPlace);
        public void SetMorph(string InputPlace, string Tag) => Add("sM" + InputPlace, Tag);
        public void SetMorphByOutputPlace(string InputPlace, string OutputPlace) => Add("iM" + InputPlace, OutputPlace);

        // Browser
        public void ChangeUrl(string Url) => Add("cu", Url);
        public void SetHeadTitle(string Title) => Add("ht", Title);
        public void ClipboardWriteText(string Text) => Add("nw", Text);
        public void ScrollTo(string X, string Y) => Add("ws", X + GS + Y);
        public void ScrollTo(int X, int Y) => ScrollTo(X.ToString(), Y.ToString());
        public void HistoryGo(string Steps) => Add("wg", Steps);
        public void HistoryGo(int Steps) => HistoryGo(Steps.ToString());
        public void ReloadPage() => Add("lr");
        public void Redirect(string Path) => Add("lh", Path);

        // Increase
        public void IncreaseMinLength(string InputPlace, string Value) => Add("+n" + InputPlace, Value);
        public void IncreaseMinLength(string InputPlace, int Value) => IncreaseMinLength(InputPlace, Value.ToString());
        public void IncreaseMaxLength(string InputPlace, string Value) => Add("+x" + InputPlace, Value);
        public void IncreaseMaxLength(string InputPlace, int Value) => IncreaseMaxLength(InputPlace, Value.ToString());
        public void IncreaseFontSize(string InputPlace, string Value) => Add("+f" + InputPlace, Value);
        public void IncreaseFontSize(string InputPlace, int Value) => IncreaseFontSize(InputPlace, Value.ToString());
        public void IncreaseWidth(string InputPlace, string Value) => Add("+w" + InputPlace, Value);
        public void IncreaseWidth(string InputPlace, int Value) => IncreaseWidth(InputPlace, Value.ToString());
        public void IncreaseHeight(string InputPlace, string Value) => Add("+h" + InputPlace, Value);
        public void IncreaseHeight(string InputPlace, int Value) => IncreaseHeight(InputPlace, Value.ToString());
        public void IncreaseValue(string InputPlace, string Value) => Add("+v" + InputPlace, Value);
        public void IncreaseValue(string InputPlace, int Value) => IncreaseValue(InputPlace, Value.ToString());

        // Decrease
        public void DecreaseMinLength(string InputPlace, string Value) => Add("-n" + InputPlace, Value);
        public void DecreaseMinLength(string InputPlace, int Value) => DecreaseMinLength(InputPlace, Value.ToString());
        public void DecreaseMaxLength(string InputPlace, string Value) => Add("-x" + InputPlace, Value);
        public void DecreaseMaxLength(string InputPlace, int Value) => DecreaseMaxLength(InputPlace, Value.ToString());
        public void DecreaseFontSize(string InputPlace, string Value) => Add("-f" + InputPlace, Value);
        public void DecreaseFontSize(string InputPlace, int Value) => DecreaseFontSize(InputPlace, Value.ToString());
        public void DecreaseWidth(string InputPlace, string Value) => Add("-w" + InputPlace, Value);
        public void DecreaseWidth(string InputPlace, int Value) => DecreaseWidth(InputPlace, Value.ToString());
        public void DecreaseHeight(string InputPlace, string Value) => Add("-h" + InputPlace, Value);
        public void DecreaseHeight(string InputPlace, int Value) => DecreaseHeight(InputPlace, Value.ToString());
        public void DecreaseValue(string InputPlace, string Value) => Add("-v" + InputPlace, Value);
        public void DecreaseValue(string InputPlace, int Value) => DecreaseValue(InputPlace, Value.ToString());

        // Event
        // ConstructorName: mouseevent, keyboardevent, uievent, focusevent, inputevent, event
        // All Method in "Event" Section Only Support Dynamic Args Once. To Support Invoking Dynamic Arguments on a Momentary Basis, Use "EventListener" Section Methods.
        public void TriggerEvent(string InputPlace, string HtmlEventListener, string ConstructorName = null) => Add("TE" + InputPlace, HtmlEventListener + (!string.IsNullOrEmpty(ConstructorName)? GS + ConstructorName : ""));
        public void SetPostEvent(string InputPlace, string HtmlEvent) => Add("Ep" + InputPlace, HtmlEvent);
        public void SetPostEvent(string InputPlace, string HtmlEvent, string OutputPlace) => Add("Ep" + InputPlace, HtmlEvent + GS + OutputPlace);
        public void SetPostEventAddView(string InputPlace, string HtmlEvent) => Add("Ep" + InputPlace, HtmlEvent + GS + "+");
        public void SetPostEventListener(string InputPlace, string HtmlEventListener) => Add("EP" + InputPlace, HtmlEventListener);
        public void SetPostEventListener(string InputPlace, string HtmlEventListener, string OutputPlace) => Add("EP" + InputPlace, HtmlEventListener + GS + OutputPlace);
        public void SetPostEventListenerAddView(string InputPlace, string HtmlEventListener) => Add("EP" + InputPlace, HtmlEventListener + GS + "+");
        public void SetGetEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eg" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Eg" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EG" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EG" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetPutEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Et" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPutEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Et" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetPutEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("ET" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPutEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("ET" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Ea" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Ea" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EA" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EA" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("El" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("El" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EL" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EL" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eo" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Eo" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EO" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EO" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + OutputPlace);
        public void SetHeadEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eh" + InputPlace, HtmlEvent + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetHeadEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EH" + InputPlace, HtmlEventListener + GS + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        // IsMultiPart: If this value is true, the data will be sent based on the Form and with the "content" key.
        public void SetSendEvent(string InputPlace, string HtmlEvent, string Data, string Path = null, string Method = "POST", bool IsMultiPart = false, string ContentType = "text/plain", string OutputPlace = null) => Add("En" + InputPlace, HtmlEvent + GS + Data.Replace('\n'.ToString(), "$[ln];").Replace("\"", "$[dq];").Replace("'", "$[sq];") + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + Method + GS + (IsMultiPart ? "1" : "0") + GS + ContentType + GS + OutputPlace);
        public void SetSendEventListener(string InputPlace, string HtmlEventListener, string Data, string Path = null, string Method = "POST", bool IsMultiPart = false, string ContentType = "text/plain", string OutputPlace = null) => Add("EN" + InputPlace, HtmlEventListener + GS + Data.Replace('\n'.ToString(), "$[ln];") + GS + (!string.IsNullOrEmpty(Path) ? Path : "#") + GS + Method + GS + (IsMultiPart ? "1" : "0") + GS + ContentType + GS + OutputPlace);
        public void SetCommentEvent(string InputPlace, string HtmlEvent, string Index = null, string OutputPlace = null) => Add("Eb" + InputPlace, HtmlEvent + GS + Index + GS + OutputPlace);
        public void SetCommentEvent(string InputPlace, string HtmlEvent, int Index, string OutputPlace = null) => SetCommentEvent(InputPlace, HtmlEvent, Index.ToString(), OutputPlace);
        public void SetCommentEventListener(string InputPlace, string HtmlEventListener, string Index = null, string OutputPlace = null) => Add("EB" + InputPlace, HtmlEventListener + GS + Index + GS + OutputPlace);
        public void SetCommentEventListener(string InputPlace, string HtmlEventListener, int Index, string OutputPlace = null) => SetCommentEventListener(InputPlace, HtmlEventListener, Index.ToString(), OutputPlace);
        public void SetWasmEvent(string InputPlace, string HtmlEvent, string WasmLanguage, string WasmUrl, string MethodName, object[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "[" + string.Join(US, Args) : "";

            Add("Ey" + InputPlace, HtmlEvent + GS + WasmLanguage + GS + WasmUrl + GS + MethodName + GS + ArgsJoin + GS + OutputPlace);
        }
        public void SetWasmEventListener(string InputPlace, string HtmlEventListener, string WasmLanguage, string WasmUrl, string MethodName, object[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "[" + string.Join(US, Args) : "";

            Add("EY" + InputPlace, HtmlEventListener + GS + WasmLanguage + GS + WasmUrl + GS + MethodName + GS + ArgsJoin + GS + OutputPlace);
        }
        public void SetWebSocketEvent(string InputPlace, string HtmlEvent, string Path) => Add("Ew" + InputPlace, HtmlEvent + GS + Path);
        public void SetWebSocketEventListener(string InputPlace, string HtmlEventListener, string Path) => Add("EW" + InputPlace, HtmlEventListener + GS + Path);
        public void SetSSEEvent(string InputPlace, string HtmlEvent, string Path, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("Ee" + InputPlace, HtmlEvent + GS + Path + GS + (ShouldReconnect? "1" : "0") + GS + ReconnectTryTimeout.ToString());
        public void SetSSEEvent(string InputPlace, string HtmlEvent, string Path, string OutputPlace, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("Ee" + InputPlace, HtmlEvent + GS + Path + GS + (ShouldReconnect ? "1" : "0") + GS + ReconnectTryTimeout.ToString() + GS + OutputPlace);
        public void SetSSEEventListener(string InputPlace, string HtmlEventListener, string Path, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("EE" + InputPlace, HtmlEventListener + GS + Path + GS + (ShouldReconnect? "1" : "0") + GS + ReconnectTryTimeout.ToString());
        public void SetSSEEventListener(string InputPlace, string HtmlEventListener, string Path, string OutputPlace, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("EE" + InputPlace, HtmlEventListener + GS + Path + GS + (ShouldReconnect ? "1" : "0") + GS + ReconnectTryTimeout.ToString() + GS + OutputPlace);
        public void SetFrontEvent(string InputPlace, string HtmlEvent, string ModulePath, object[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("Ej" + InputPlace, HtmlEvent + GS + ModulePath + GS + OutputPlace + ArgsJoin);
        }
        public void SetFrontEventListener(string InputPlace, string HtmlEventListener, string ModulePath, object[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("EJ" + InputPlace, HtmlEventListener + GS + ModulePath + GS + OutputPlace + ArgsJoin);
        }
        public void SetMasterPagesEvent(string InputPlace, string HtmlEvent, string OutputPlace = null) => Add("Eu" + InputPlace, HtmlEvent + GS + OutputPlace);
        public void SetMasterPagesEventListener(string InputPlace, string HtmlEventListener, string OutputPlace = null) => Add("EU" + InputPlace, HtmlEventListener + GS + OutputPlace);
        public void SetPreventDefaultEvent(string InputPlace, string HtmlEvent) => Add("Ed" + InputPlace, HtmlEvent);
        public void SetPreventDefaultEventListener(string InputPlace, string HtmlEventListener) => Add("ED" + InputPlace, HtmlEventListener);
        public void SetStopPropagationEvent(string InputPlace, string HtmlEvent) => Add("Es" + InputPlace, HtmlEvent);
        public void SetStopPropagationEventListener(string InputPlace, string HtmlEventListener) => Add("ES" + InputPlace, HtmlEventListener);
        public void SetMethodEvent(string InputPlace, string HtmlEvent, string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("Em" + InputPlace, HtmlEvent + GS + MethodName + ArgsJoin);
        }
        public void SetMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("EM" + InputPlace, HtmlEventListener + GS + MethodName + ArgsJoin);
        }
        public void SetModuleMethodEvent(string InputPlace, string HtmlEvent, string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("Ex" + InputPlace, HtmlEvent + GS + MethodName + ArgsJoin);
        }
        public void SetModuleMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("EX" + InputPlace, HtmlEventListener + GS + MethodName + ArgsJoin);
        }
        public void AssignConfirmEvent(string InputPlace, string HtmlEvent, string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel") => Add("Ef" + InputPlace, HtmlEvent + GS + (Text == "Are you sure you want to proceed?" ? "" : Text) + GS + (Type == "none"? "" : Type) + GS + (Title == "Confirm" ? "" : Title) + GS + (OkText == "OK" ? "" :  OkText) + GS + (CancelText == "Cancel" ? "" : CancelText));
        public void RemovePostEvent(string InputPlace, string HtmlEvent) => Add("Rp" + InputPlace, HtmlEvent);
        public void RemovePostEventListener(string InputPlace, string HtmlEventListener) => Add("RP" + InputPlace, HtmlEventListener);
        public void RemoveGetEvent(string InputPlace, string HtmlEvent) => Add("Rg" + InputPlace, HtmlEvent);
        public void RemoveGetEventListener(string InputPlace, string HtmlEventListener) => Add("RG" + InputPlace, HtmlEventListener);
        public void RemovePutEvent(string InputPlace, string HtmlEvent) => Add("Rt" + InputPlace, HtmlEvent);
        public void RemovePutEventListener(string InputPlace, string HtmlEventListener) => Add("RT" + InputPlace, HtmlEventListener);
        public void RemovePatchEvent(string InputPlace, string HtmlEvent) => Add("Ra" + InputPlace, HtmlEvent);
        public void RemovePatchEventListener(string InputPlace, string HtmlEventListener) => Add("RA" + InputPlace, HtmlEventListener);
        public void RemoveDeleteEvent(string InputPlace, string HtmlEvent) => Add("Rl" + InputPlace, HtmlEvent);
        public void RemoveDeleteEventListener(string InputPlace, string HtmlEventListener) => Add("RL" + InputPlace, HtmlEventListener);
        public void RemoveOptionsEvent(string InputPlace, string HtmlEvent) => Add("Ro" + InputPlace, HtmlEvent);
        public void RemoveOptionsEventListener(string InputPlace, string HtmlEventListener) => Add("RO" + InputPlace, HtmlEventListener);
        public void RemoveHeadEvent(string InputPlace, string HtmlEvent) => Add("Rh" + InputPlace, HtmlEvent);
        public void RemoveHeadEventListener(string InputPlace, string HtmlEventListener) => Add("RH" + InputPlace, HtmlEventListener);
        public void RemoveSendEvent(string InputPlace, string HtmlEvent) => Add("Rn" + InputPlace, HtmlEvent);
        public void RemoveSendEventListener(string InputPlace, string HtmlEventListener) => Add("RN" + InputPlace, HtmlEventListener);
        public void RemoveCommentEvent(string InputPlace, string HtmlEvent) => Add("Rb" + InputPlace, HtmlEvent);
        public void RemoveCommentEventListener(string InputPlace, string HtmlEventListener) => Add("RB" + InputPlace, HtmlEventListener);
        public void RemoveWasmEvent(string InputPlace, string HtmlEvent) => Add("Ry" + InputPlace, HtmlEvent);
        public void RemoveWasmEventListener(string InputPlace, string HtmlEventListener) => Add("RY" + InputPlace, HtmlEventListener);
        public void RemoveWebSocketEvent(string InputPlace, string HtmlEvent) => Add("Rw" + InputPlace, HtmlEvent);
        public void RemoveWebSocketEventListener(string InputPlace, string HtmlEventListener) => Add("RW" + InputPlace, HtmlEventListener);
        public void RemoveSSEEvent(string InputPlace, string HtmlEvent) => Add("Re" + InputPlace, HtmlEvent);
        public void RemoveSSEEventListener(string InputPlace, string HtmlEventListener) => Add("RE" + InputPlace, HtmlEventListener);
        public void RemoveFrontEvent(string InputPlace, string HtmlEvent) => Add("Rj" + InputPlace, HtmlEvent);
        public void RemoveFrontEventListener(string InputPlace, string HtmlEventListener) => Add("RJ" + InputPlace, HtmlEventListener);
        public void RemovePreventDefaultEvent(string InputPlace, string HtmlEvent) => Add("Rd" + InputPlace, HtmlEvent);
        public void RemovePreventDefaultEventListener(string InputPlace, string HtmlEventListener) => Add("RD" + InputPlace, HtmlEventListener);
        public void RemoveMasterPagesEvent(string InputPlace, string HtmlEvent) => Add("Ru" + InputPlace, HtmlEvent);
        public void RemoveMasterPagesEventListener(string InputPlace, string HtmlEventListener) => Add("RU" + InputPlace, HtmlEventListener);
        public void RemoveStopPropagationEvent(string InputPlace, string HtmlEvent) => Add("Rs" + InputPlace, HtmlEvent);
        public void RemoveStopPropagationEventListener(string InputPlace, string HtmlEventListener) => Add("RS" + InputPlace, HtmlEventListener);
        public void RemoveMethodEvent(string InputPlace, string HtmlEvent, string MethodName) => Add("Rm" + InputPlace, HtmlEvent + GS + MethodName);
        public void RemoveMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName) => Add("RM" + InputPlace, HtmlEventListener + GS + MethodName);
        public void RemoveModuleMethodEvent(string InputPlace, string HtmlEvent, string MethodName) => Add("Rx" + InputPlace, HtmlEvent + GS + MethodName);
        public void RemoveModuleMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName) => Add("RX" + InputPlace, HtmlEventListener + GS + MethodName);
        public void RemoveConfirmEvent(string InputPlace, string HtmlEvent) => Add("Rf" + InputPlace, HtmlEvent);

        // Custom Event
		// This Method Is Compatible With EventListener And May Not Be Compatible With Events Written As Attributes In Some Browsers.
        // Watch: attribute, style, text, children, value
        // Compare: greater, less, equal, notequal, includes, startswith, endswith, matches, changed, inrange, lengthgreater, lengthless, lengthequal
        // Range: Only Use For Compare With inrange Value. Split By Comma ","
        // Key: Only Use For Watch With attribute And style Value
        public void CreateCustomDOMEvent(string InputPlace, string EventName ,string Watch, string Key, string Compare, string Value, string Range, bool Immediate = false, string Delay = "0") => Add("eC" + InputPlace, EventName + GS + Watch + GS + Key + GS + Compare + GS + Value + GS + Range + GS + (Immediate?  "1" : "0") + GS + Delay);
        public void CreateCustomDOMEvent(string InputPlace, string EventName ,string Watch, string Key, string Compare, string Value, string Range, bool Immediate, int Delay) => CreateCustomDOMEvent(InputPlace, EventName, Watch, Key, Compare, Value, Range, Immediate, Delay.ToString());
        public void EnableScrollBottomEvent(bool Enable = true) => Add("eb", Enable? "1" : "0");
        public void EnableReachedElementEvent(string InputPlace, bool Once, bool Enable = true) => Add("er" + InputPlace, (Once ? "1" : "0") + GS + (Enable? "1" : "0"));

        // Module
        public void LoadModule(string ModulePath, string[] Methods = null)
        {
            Methods ??= System.Array.Empty<string>();
            Add("Ml", ModulePath + ((Methods.Length > 0) ? GS + "[" + string.Join(US, Methods) : ""));
        }
        public void UnloadModule(string ModulePath) => Add("Mu", ModulePath);
        public void DeleteModuleMethod(string MethodName) => Add("Md", MethodName);

        // Unit Testing
        // InputPlace Is Actual, Expected Is Tag/OutputPlace
        public void AssertEqual(string InputPlace, string Tag) => Add("At" + InputPlace, Tag.Replace('\n'.ToString(), "$[ln];"));
        public void AssertEqualByOutputPlace(string InputPlace, string OutputPlace) => Add("Ao" + InputPlace, OutputPlace);

        // Debug
        public void CreateDebugger(bool Pause = false) => Add("Dc", Pause? "1" : "0");

        // Service Worker
        // To Use Service Worker, You Need To Add The Elanat Dedicated Module (service-worker.js) On The Client Side
        public void ServiceWorkerRegister(string Path = null, string ScopePath = null) => Add("wR", Path + GS + ScopePath);
        public void ServiceWorkerPreCacheStatic(string[] PathList) => Add("wp",string.Join(GS, PathList));
        public void ServiceWorkerDynamicCache(string Path, string Seconds = "") => Add("wc", Path + (Seconds != "" ? GS + Seconds : ""));
        public void ServiceWorkerDynamicCache(string Path, int Seconds) => ServiceWorkerDynamicCache(Path, Seconds > 0 ? Seconds.ToString() : "");
        public void ServiceWorkerDeleteDynamicCache() => Add("wd");
        public void ServiceWorkerDeleteDynamicCache(string Path) => Add("wd", Path);
        public void ServiceWorkerDynamicCacheTTLUpdate(string Path, string Seconds = "") => Add("wt", Path + (Seconds != "" ? GS + Seconds : ""));
        public void ServiceWorkerDynamicCacheTTLUpdate(string Path, int Seconds) => ServiceWorkerDynamicCacheTTLUpdate(Path, Seconds > 0 ? Seconds.ToString() : "");
        // Path: Support Wildcard Automatically And Also Support Regex If Use "re:" Before Pattern
        // Type: Type Is Cache Strategy. cachefirst, networkfirst, cacheonly, networkonly, stalerevalidate (Fast From Cache, Updates Simultaneously From The Network)
        // CacheDynamic: If True, Any Successful Network Response For That Route Will Be Stored In The Dynamic Cache
        public void ServiceWorkerRouteSet(string Path, string Type, bool CacheDynamic = false) => Add("wr", Path + GS + Type + (CacheDynamic? GS + "1" : ""));
        public void ServiceWorkerRouteAlias(string Path, string To) => Add("wa", Path + GS + To);
        public void ServiceWorkerDeleteRouteAlias(string Path = null) => Add("wC", Path);
        // Delete All Route And Alias
        public void ServiceWorkerDeleteRoute() => Add("wD");
        public void ServiceWorkerDeleteRoute(string Path) => Add("wD", Path);

        // SSE
        public void DisconnectSSE(string Path) => Add("Ds", Path);
        public void DisconnectAllSSE() => Add("Ds");

        // State
        public void AddState(string Path = null, string Title = null) => Add("AS", Path + GS + Title);
        public void SaveState(string Path = null, string Title = null) => Add("As", Path + GS + Title);
        public void LoadState(string Path) => Add("ls", Path);
        public void DeleteState(string Path = null) => Add("DS", Path);
        public void DeleteAllState() => Add("DS", "*");

        // Cookie
        public void SetCookie(string Key, string Value, object Seconds, string Path = null) => Add("sC", Key + GS + Value + GS + Seconds.ToString() + (!string.IsNullOrEmpty(Path) ? GS + Path : ""));

        // Save (Session Cache)
        public void SaveId(string InputPlace, string Key = ".") => Add("@gi" + InputPlace, Key);
        public void SaveName(string InputPlace, string Key = ".") => Add("@gn" + InputPlace, Key);
        public void SaveValue(string InputPlace, string Key = ".") => Add("@gv" + InputPlace, Key);
        public void SaveValueLength(string InputPlace, string Key = ".") => Add("@ge" + InputPlace, Key);
        public void SaveClass(string InputPlace, string Key = ".") => Add("@gc" + InputPlace, Key);
        public void SaveStyle(string InputPlace, string Key = ".") => Add("@gs" + InputPlace, Key);
        public void SaveTitle(string InputPlace, string Key = ".") => Add("@gl" + InputPlace, Key);
        public void SaveLabel(string InputPlace, string Key = ".") => Add("@gA" + InputPlace, Key);
        public void SaveText(string InputPlace, string Key = ".") => Add("@gt" + InputPlace, Key);
        public void SaveOuterText(string InputPlace, string Key = ".") => Add("@go" + InputPlace, Key);
        public void SaveTextLength(string InputPlace, string Key = ".") => Add("@gg" + InputPlace, Key);
        public void SaveAttribute(string InputPlace, string Attribute, string Key = ".") => Add("@ga" + InputPlace, Key + GS + Attribute);
        public void SaveWidth(string InputPlace, string Key = ".") => Add("@gw" + InputPlace, Key);
        public void SaveHeight(string InputPlace, string Key = ".") => Add("@gh" + InputPlace, Key);
        public void SaveReadOnly(string InputPlace, string Key = ".") => Add("@gr" + InputPlace, Key);
        public void SaveSelectedIndex(string InputPlace, string Key = ".") => Add("@gx" + InputPlace, Key);
        public void SaveTextAlign(string InputPlace, string Key = ".") => Add("@gT" + InputPlace, Key);
        public void SaveNodeLength(string InputPlace, string Key = ".") => Add("@gL" + InputPlace, Key);
        public void SaveVisible(string InputPlace, string Key = ".") => Add("@gV" + InputPlace, Key);
        public void SaveUrl(string Url, bool FetchScript = false, string Key = ".") => Add("@gu", Key + GS + Url + (FetchScript ? GS + "1" : ""));
        public void SaveIndex(string InputPlace, string Key = ".") => Add("@gI" + InputPlace, Key);
        public void RemoveSave(string CacheKey) => Add("rs", CacheKey);
        public void RemoveAllSave() => Add("rs", "*");
        // Calling the SetSave Method Causes Action Control Requests Triggered by Events Using the GET, POST, PUT, PATCH, DELETE, and OPTIONS Methods, as well as Requests Triggered by the Send Event, to be Temporarily Saved on the Active Page, so the Request will not be Sent to the Server Again.
        public void SetSave() => Add("cs", "*");
        public void AddSaveValue(string CacheKey, string Value) => Add("SA", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void InsertSaveValue(string CacheKey, string Value) => Add("SI", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void AppendSaveValue(string CacheKey, string Value) => Add("SP", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void ReplaceSaveValue(string CacheKey, string SearchValue, string Value) => Add("SR", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];") + GS + SearchValue.Replace('\n'.ToString(), "$[ln];"));

        // Cache
        public void CacheId(string InputPlace, string Key = ".") => Add("@ci" + InputPlace, Key);
        public void CacheName(string InputPlace, string Key = ".") => Add("@cn" + InputPlace, Key);
        public void CacheValue(string InputPlace, string Key = ".") => Add("@cv" + InputPlace, Key);
        public void CacheValueLength(string InputPlace, string Key = ".") => Add("@ce" + InputPlace, Key);
        public void CacheClass(string InputPlace, string Key = ".") => Add("@cc" + InputPlace, Key);
        public void CacheStyle(string InputPlace, string Key = ".") => Add("@cs" + InputPlace, Key);
        public void CacheTitle(string InputPlace, string Key = ".") => Add("@cl" + InputPlace, Key);
        public void CacheLabel(string InputPlace, string Key = ".") => Add("@cA" + InputPlace, Key);
        public void CacheText(string InputPlace, string Key = ".") => Add("@ct" + InputPlace, Key);
        public void CacheOuterText(string InputPlace, string Key = ".") => Add("@co" + InputPlace, Key);
        public void CacheTextLength(string InputPlace, string Key = ".") => Add("@cg" + InputPlace, Key);
        public void CacheAttribute(string InputPlace, string Attribute, string Key = ".") => Add("@ca" + InputPlace, Key + GS + Attribute);
        public void CacheWidth(string InputPlace, string Key = ".") => Add("@cw" + InputPlace, Key);
        public void CacheHeight(string InputPlace, string Key = ".") => Add("@ch" + InputPlace, Key);
        public void CacheReadOnly(string InputPlace, string Key = ".") => Add("@cr" + InputPlace, Key);
        public void CacheSelectedIndex(string InputPlace, string Key = ".") => Add("@cx" + InputPlace, Key);
        public void CacheTextAlign(string InputPlace, string Key = ".") => Add("@cT" + InputPlace, Key);
        public void CacheNodeLength(string InputPlace, string Key = ".") => Add("@cL" + InputPlace, Key);
        public void CacheVisible(string InputPlace, string Key = ".") => Add("@cV" + InputPlace, Key);
        public void CacheUrl(string Url, bool FetchScript = false, string Key = ".") => Add("@cu", Key + GS + Url + (FetchScript ? GS + "1" : ""));
        public void CacheIndex(string InputPlace, string Key = ".") => Add("@cI" + InputPlace, Key);
        public void RemoveCache(string CacheKey) => Add("rd", CacheKey);
        public void RemoveAllCache() => Add("rd", "*");
        // Calling the SetCache Method Causes Action Control Requests Triggered by events using the GET, POST, PUT, PATCH, DELETE, and OPTIONS Methods, as well as Requests Triggered by the Send event, to be Cached, so the Request will not be Sent to the Server Again.
        public void SetCache(string Second) => Add("cd", Second);
        public void SetCache(int Second) => SetCache(Second.ToString());
        public void SetCache() => Add("cd", "*");
        public void AddCacheValue(string CacheKey, string Value) => Add("CA", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void InsertCacheValue(string CacheKey, string Value) => Add("CI", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void AppendCacheValue(string CacheKey, string Value) => Add("CP", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];"));
        public void ReplaceCacheValue(string CacheKey, string SearchValue, string Value) => Add("CR", CacheKey + GS + Value.Replace('\n'.ToString(), "$[ln];") +  GS + SearchValue.Replace('\n'.ToString(), "$[ln];"));

        // Call
        public void LoadUrl(string InputPlace, string Url) => Add("lu" + InputPlace, Url);
        public void RunActionControls(string ActionControls, bool WithoutWebFormsSection = true, string Index = null, bool UseCurrentEvent = true) => Add("lA", (UseCurrentEvent ? "1" : "0") + GS + (WithoutWebFormsSection ? "1" : "0") + GS + Index + GS + ActionControls);
        public void CallScript(string ScriptText) => Add("_", ScriptText.Replace('\n'.ToString(), "$[ln];"));
        public void CallMethod(string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("lm", MethodName + ArgsJoin);
        }
        public void CallModuleMethod(string MethodName, object[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("lM", MethodName + ArgsJoin);
        }
        public void CallPostBack(string FormInputPlace, string OutputPlace = null) => Add("Lp", "1" + GS + FormInputPlace + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallCommentBack(string Index = null, string InputPlace = null, bool UseCurrentEvent = true) => Add("LC", (UseCurrentEvent? "1": "0") + GS + Index + GS + InputPlace);
        public void CallCommentBack(int Index, string InputPlace = null, bool UseCurrentEvent = true) => CallCommentBack(Index.ToString(), InputPlace, UseCurrentEvent);
        public void CallWasmBack(string WasmLanguage, string WasmUrl, string MethodName, object[] Args = null, string OutputPlace = null, bool UseCurrentEvent = true)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "[" + string.Join(US, Args) : "";

            Add("Ly", (UseCurrentEvent ? "1" : "0") + GS + WasmLanguage + GS + WasmUrl + GS + MethodName + GS + ArgsJoin + GS + OutputPlace);
        }
        public void CallWebSocketBack(string Path, bool UseCurrentEvent = true) => Add("Lw", (UseCurrentEvent? "1": "0") + GS + Path);
        public void CallSSEBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true, bool ShouldReconnect = true, string ReconnectTryTimeout = "3000") => Add("Ls", (UseCurrentEvent ? "1" : "0") + GS + Path + GS + (ShouldReconnect ? "1" : "0") + GS + ReconnectTryTimeout + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallSSEBack(string Path, string OutputPlace, bool UseCurrentEvent, bool ShouldReconnect, int ReconnectTryTimeout) => CallSSEBack(Path, OutputPlace, UseCurrentEvent, ShouldReconnect, ReconnectTryTimeout.ToString());
        public void CallFront(string ModulePath, object[] Args = null, string OutputPlace = null, bool UseCurrentEvent = true)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? GS + "[" + string.Join(US, Args) : "";

            Add("Lj", (UseCurrentEvent ? "1" : "0") + GS + ModulePath + GS + OutputPlace + ArgsJoin);
        }
        public void CallGetBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lg", (UseCurrentEvent ? "1" : "0") + GS + Path + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallPutBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lt", (UseCurrentEvent ? "1" : "0") + GS + Path + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallPatchBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LP", (UseCurrentEvent ? "1" : "0") + GS + Path + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallDeleteBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Ld", (UseCurrentEvent ? "1" : "0") + GS + Path + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallHeadBack(string Path, bool UseCurrentEvent = true) => Add("Lh", (UseCurrentEvent ? "1" : "0") + GS + Path);
        public void CallOptionsBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lo", (UseCurrentEvent ? "1" : "0") + GS + Path + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));
        public void CallSendBack(string Path, string Method, bool IsMultiPart, string ContentType, string Data, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LS", (UseCurrentEvent ? "1" : "0") + GS + Path + GS + Method + GS + (IsMultiPart ? "1" : "0") + GS + ContentType + GS + Data.Replace('\n'.ToString(), "$[ln];") + (!string.IsNullOrEmpty(OutputPlace) ? GS + OutputPlace : ""));

        // Update
        public void Increase(string InputPlace, float Value) => Add("gt" + InputPlace, "i" + GS + Value.ToString());
        public void Decrease(string InputPlace, float Value) => Add("gt" + InputPlace, "i" + GS + (Value * -1).ToString());
        // If You Don't Use Deep Mode, Any Tags Inside The Current Tag Will Simply Be Treated As Strings. Deep Mode Does Not Remove Inner Elements.
        public void Replace(string InputPlace, string Value, string NewValue, bool AlsoStartTag = false, bool Deep = true)
        {
            if (!string.IsNullOrEmpty(Value))
                if (Value[0] == '@')
                {
                    Value = Value.Remove(0, 1);
                    Value = "$[at];" + Value;
                }

            if (!string.IsNullOrEmpty(NewValue))
                if (NewValue[0] == '@')
                {
                    NewValue = NewValue.Remove(0, 1);
                    NewValue = "$[at];" + NewValue;
                }

            Add("gt" + InputPlace, "r" + GS + Value + GS + NewValue + GS + (AlsoStartTag ? "1" : "0") + GS + (Deep ? "1" : "0"));
        }

        // HTML Converts Attribute Names To Lowercase, So They Need To Be Written In Lowercase.
        public void ReplaceStartTag(string InputPlace, string Value, string NewValue)
        {
            if (!string.IsNullOrEmpty(Value))
                if (Value[0] == '@')
                {
                    Value = Value.Remove(0, 1);
                    Value = "$[at];" + Value;
                }

            if (!string.IsNullOrEmpty(NewValue))
                if (NewValue[0] == '@')
                {
                    NewValue = NewValue.Remove(0, 1);
                    NewValue = "$[at];" + NewValue;
                }

            Add("gt" + InputPlace, "s" + GS + Value + GS + NewValue);
        }

        // Pre Runner
        public void AssignDelay(int MiliSecond, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string newName = ":" + MiliSecond + ")" + parts[0];
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void AssignDelayChange(int MiliSecond, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string currentName = parts[0];

            if (currentName.StartsWith(":") && currentName.Contains(")"))
            {
                int closingBracket = currentName.IndexOf(')');
                currentName = currentName.Substring(closingBracket + 1);
            }

            string newName = ":" + MiliSecond + ")" + currentName;
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void AssignInterval(int MiliSecond, string Id = null, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string newName = "(" + MiliSecond + (!string.IsNullOrEmpty(Id) ? "|" + Id : "") + ")" + parts[0];
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void AssignIntervalChange(float MiliSecond, string Id = null, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string currentName = parts[0];

            if (currentName.StartsWith("(") && currentName.Contains(")"))
            {
                int closingBracket = currentName.IndexOf(')');
                currentName = currentName.Substring(closingBracket + 1);
            }

            string newName = "(" + MiliSecond + (!string.IsNullOrEmpty(Id) ? "|" + Id : "") + ")" + currentName;
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void DeleteInterval(string Id) => Add("Di", Id);

        public void AssignRepeat(int Count, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string newName = "," + Count + ")" + parts[0];
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void AssignRepeatChange(int Count, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string currentName = parts[0];

            if (currentName.StartsWith(",") && currentName.Contains(")"))
            {
                int closingBracket = currentName.IndexOf(')');
                currentName = currentName.Substring(closingBracket + 1);
            }

            string newName = "," + Count + ")" + currentName;
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        // Index
        public void StartIndex(string Name) => Add("#", Name);
        public void StartIndex() => StartIndex("");
        // This Index Is Automatically Run After Changing The Browser History (Back And Forward Buttons)
        public void StartState() => StartIndex("$");
        public void GoTo(string Line, string Repeat = "1") => Add("&", Line + GS + Repeat);
        public void GoTo(int Line, int Repeat = 1) => GoTo(Line.ToString(), Repeat.ToString());
        public void GoTo(string Index, int Repeat) => Add("&", "#" + Index + GS + Repeat.ToString());
        
        // Start
        public void StartTransientDOM(string InputPlace) => Add("td", InputPlace);
        public void EndTransientDOM() => Add("td", ";");

        // Message
        // Type: warning, problem, help, success, none
        public void Alert(string Text, string Type = "none", string Title = "Alert", string OkText = "OK") => Add("Al", Text + GS + (Type == "none" ? "" : Type) + GS + (Title == "Alert" ? "" : Title) + GS + (OkText == "OK" ? "" : OkText));
        public void Message(string Text, string Type = "none", string Duration = "0") => Add("me", Text + GS + (Type == "none" ? "" : Type) + GS + (Duration == "0" ? "" : Duration));
        public void Message(string Text, string Type, int Duration) => Message(Text, Type, Duration.ToString());

        // Type: log, info, warn, error, debug, trace, group, groupend, table
        public void ConsoleMessage(string Text, string Type = "log") => Add("mc", Text.Replace('\n'.ToString(), "$[ln];") + (Type == "log" ? "" : GS + Type));
        public void ConsoleMessageAssert(string Text, string Condition) => Add("ma", Text.Replace('\n'.ToString(), "$[ln];") + GS + Condition);

        // Enable
        //Calling The EnableWebSocket Or EnableWebSocketOnce Or AddWebSocket Methods Will Cause Any Subsequent Requests (Under WebForms Core Technology) To Operate Under The WebSocket Protocol.
        public void EnableWebSocket(bool Enable = true) => Add("ew", Enable ? "1" : "0");
        public void EnableWebSocketOnce() => Add("ew", "$");
        public void AddWebSocket(string Path) => Add("aw" + Path);
        public void DeleteWebSocket(string Path) => Add("dw" + Path);

        // Use
        // InputPlace Using Only For form Element
        public void UseWebSocket(string InputPlace) => Add("uw" + InputPlace);
        public void UseOnlyChangeUpdate(string InputPlace) => Add("uo" + InputPlace);

        // Condition And Loop
        // Type: warning, problem, help, success, none
        // Interval: Value 0 is Await (if is not True, all Next Action Controls Waiting for it), Value -1 is Sync Check Once (is Support Bracket or Next Action Control), Value > 0 is Async and is Wait Based on Time Repetition Until it Becomes True (Is Support Bracket or Next Action Control, but is not Support Else).
        // Nested Conditions and Nested Loops are Possible.
        public void ConfirmIsTrueAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "ct", (Text == "Are you sure you want to proceed?" ? "" : Text) + GS + (Type == "none" ? "" : Type) + GS + (Title == "Confirm" ? "" : Title) + GS + (OkText == "OK" ? "" : OkText) + GS + (CancelText == "Cancel" ? "" : CancelText));
        public void ConfirmIsFalseAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "cf", (Text == "Are you sure you want to proceed?" ? "" : Text) + GS + (Type == "none" ? "" : Type) + GS + (Title == "Confirm" ? "" : Title) + GS + (OkText == "OK" ? "" : OkText) + GS + (CancelText == "Cancel" ? "" : CancelText));
        public void IsGreaterThan(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "gt", FirstValue + GS + SecondValue);
        public void IsLessThan(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "lt", FirstValue + GS + SecondValue);
        public void IsEqualTo(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "et", FirstValue + GS + SecondValue);
        public void IsNotEqualTo(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "Nt", FirstValue + GS + SecondValue);
        public void Exist(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "ex", Value);
        public void NotExist(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nx", Value);
        public void IsTrue(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "tr", Value);
        public void IsFalse(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "fa", Value);
        public void IsMatchMedia(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "mm", Value);
        public void IsNotMatchMedia(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nm", Value);
        public void Include(string Text, string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "In", Value + GS + Text);
        public void NotInclude(string Text, string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "Nn", Value + GS + Text);
        public void ElementExists(string InputPlace, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "eE", InputPlace);
        public void ElementNotExists(string InputPlace, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nE", InputPlace);
        public void IsRegexMatch(string Value, string Pattern, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "re", Value + GS + Pattern);
        public void IsRegexNotMatch(string Value, string Pattern, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "rn", Value + GS + Pattern);
        // In: Everything Becomes A JSON List.
        // Key: Creates A Temporary Data In The Browser IndexedDB.
        // Key + "i" Creates A Temporary Data To Maintain The Loop Counter In The Browser IndexedDB.
        public void ForEach(string Path, string In, string Key = ".") => Add( "{fe", Path + GS + In + GS + Key);
        public void Break() => Add(";");
        public void Else() => Add("}e");
        public void StartBracket() => Add("{");
        public void EndBracket() => Add("}");
        // Used Then In Condition And Loop Methods
        public WebForms Then(WebForms newForm)
        {
            string data = newForm?.GetWebFormsData();
            
            if (!string.IsNullOrEmpty(data))
            {
                if (data.Contains('\n'))
                {
                    newForm.AddToUp("{");
                    newForm.Add("}");
                }
            }
            
            AppendForm(newForm);
            return this;
        }

        public WebForms Then(System.Action<WebForms> configure)
        {
            var newForm = new WebForms();
            configure(newForm);
            
            string data = newForm?.GetWebFormsData();
            
            if (!string.IsNullOrEmpty(data))
            {
                if (data.Contains('\n'))
                {
                    newForm.AddToUp("{");
                    newForm.Add("}");
                }
            }
            
            AppendForm(newForm);
            return this;
        }

        public WebForms Repeat(WebForms newForm, int repeat)
        {
            if (newForm == null)
                return this;

            string bodyData = newForm.GetWebFormsData();

            if (string.IsNullOrEmpty(bodyData))
                return this;

            int startLine = GetWebFormsData().Split('\n').Length - 1;

            AppendForm(newForm);
            GoTo(startLine, repeat - 1);

            return this;
        }
        
        public WebForms Repeat(WebForms newForm, int repeat, string index)
        {
            if (newForm == null)
                return this;

            GoTo(index);
            StartIndex(index);

            string bodyData = newForm.GetWebFormsData();

            if (string.IsNullOrEmpty(bodyData))
                return this;

            AppendForm(newForm);

            if (string.IsNullOrEmpty(index))
            {
                int indexNumber = -1;

                foreach (string x in GetWebFormsData().Split('\n'))
                {
                    if (x.StartsWith("#"))
                        indexNumber++;
                }

                GoTo(indexNumber.ToString(), repeat - 1);
            }
            else
                GoTo(index, repeat - 1);

            return this;
        }
    
        public WebForms Repeat(System.Action<WebForms> configure, int repeat)
        {
            var newForm = new WebForms();
            configure(newForm);
            return Repeat(newForm, repeat);
        }
        
        public WebForms Repeat(System.Action<WebForms> configure, int repeat, string index)
        {
            var newForm = new WebForms();
            configure(newForm);
            return Repeat(newForm, repeat, index);
        }

        // Async
        public void Async() => Add("{(a)");
        public void Delay(string MiliSecond) => Add("De", MiliSecond);
        public void Delay(int MiliSecond) => Delay(MiliSecond.ToString());

        // Option
        public void ChangeOption(string Name, string Value) => Add("co", Name + GS + Value.JsonNormalize());
        public void ResetOption() => Add("ro");
        public void ResetOption(string Name) => Add("ro", Name);

        // Format Storage
        public void CreateFormatStorage(string Key, string Data) => Add(".C", Key + GS + Data);
        public void DeleteFormatStorage(string Key) => Add(".D", Key);
        public void AddJSON(string Key, string Path, string Value) => Add(".a", Key + GS + "j" + GS + Value + GS + Path);
        // Name: For Support Attribute, Set @ Before Name. Add Double @ (@@) For Support Dynamic Args In Attribute
        public void AddXML(string Key, string Path, string Name, string Value = null)
        {
            if (!string.IsNullOrEmpty(Name))
                if (Name[0] == '@')
                {
                    Name = Name.Remove(0);
                    Name = "$[at];" + Name;
                }

            Add(".a", Key + GS + "x" + GS + Name.Replace("@", "$[at];") + GS + Value + GS + Path);
        }
        public void AddINI(string Key, string Path, string Value, bool IsINILike = false) => Add(".a", Key + GS + "i" + GS + (IsINILike ? "1" : "0") + GS + Value + GS + Path);
        public void AddTextLine(string Key, string Line, string Text) => Add(".a", Key + GS + "t" + GS + Text + GS + Line);
        public void AddTextLine(string Key, int Line, string Text) => AddTextLine(Key, Line.ToString(), Text);
        public void AddVariable(string Key, string Value) => Add(".a", Key + GS + "v" + GS + Value);
        public void UpdateJSON(string Key, string Path, string Value) => Add(".u", Key + GS + "j" + GS + Value + GS + Path);
        public void UpdateXML(string Key, string Path, string Value) => Add(".u", Key + GS + "x" + GS + Value + GS + Path);
        public void UpdateINI(string Key, string Path, string Value, bool IsINILike = false) => Add(".u", Key + GS + "i" + GS + (IsINILike ? "1" : "0") + GS + Value + GS + Path);
        public void UpdateTexLine(string Key, string Line, string Text) => Add(".u", Key + GS + "t" + GS + Text + GS + Line);
        public void UpdateTexLine(string Key, int Line, string Text) => UpdateTexLine(Key, Line.ToString(), Text);
        public void UpdateVariable(string Key, string Value) => Add(".u", Key + GS + "v" + GS + Value);
        public void IncreaceVariable(string Key, string Value) => Add(".i", Key + GS + "v" + GS + Value);
        public void IncreaceVariable(string Key, int Value) => IncreaceVariable(Key, Value.ToString());
        public void DecreaseVariable(string Key, int Value) => IncreaceVariable(Key, Value * -1);
        public void DeleteJSON(string Key, string Path) => Add(".d", Key + GS + "j" + GS + Path);
        public void DeleteXML(string Key, string Path) => Add(".d", Key + GS + "x" + GS + Path);
        public void DeleteINI(string Key, string Path, bool IsINILike = false) => Add(".d", Key + GS + "i" + GS + IsINILike + GS + Path);
        public void DeleteTextLine(string Key, string Line) => Add(".d", Key + GS + "t" + GS + Line);
        public void DeleteTextLine(string Key, int Line) => DeleteTextLine(Key, Line.ToString());
        public void DeleteVariable(string Key) => Add(".d", Key + GS + "v");

        // Template Engine
        // Pattern Example: {{value}}, ((value)), *value*, $value;
        public void BindJSONToTemplate(string InputPlace, string JSONText, string Path, string Pattern, bool AlsoStartTag = true) =>  Add("Tj" + InputPlace, JSONText + GS + Path + GS + Pattern + GS + (AlsoStartTag ? "1" : "0"));
        public void BindXMLToTemplate(string InputPlace, string XMLText, string Path, string Pattern, bool AlsoStartTag = true) =>  Add("Tx" + InputPlace, XMLText + GS + Path + GS + Pattern + GS + (AlsoStartTag ? "1" : "0"));
        public void BindINIToTemplate(string InputPlace, string INIText, string Path, string Pattern, bool AlsoStartTag = true) =>  Add("Ti" + InputPlace, INIText + GS + Path + GS + Pattern + GS + (AlsoStartTag ? "1" : "0"));

        // Inject
        // Need Add @: To First Of String
        public string Inject(string Value) => "$[" + Value + "];";

        // Action Control
        public void ReplaceActionControl(string SearchValue, string Value, bool AddingToUp = false)
        {
            if (AddingToUp)
                AddToUp("rE", SearchValue + GS + Value);
            else
                Add("rE", SearchValue + GS + Value);
        }
        
        public void AssignReplace(string SearchValue, string Value, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string newName = ";" + SearchValue + GS + Value + GS + parts[0];
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        // Hash And Checksum
        public void SetHash() => Add("SH");
        public void SetChecksum() => Add("CS");

        public string ChecksumCalculation(string Text)
        {
            int sum = 0;
            int mod = 65536;
            int shift = 5;

            foreach (char c in Text)
            {
                sum = ((sum << shift) | (sum >> (16 - shift))) ^ c;
                sum %= mod;
            }

            return sum.ToString();
        }

        public string GetChecksum() => ChecksumCalculation(GetWebFormsData());

        // Get
        public string GetFormsActionData()
        {
            if (WebFormsData.Length == 0)
                return "";

            return WebFormsData.ToString();
        }

        public string Response()
        {
            return "[web-forms]\n" + GetFormsActionData();
        }

        public string GetFormsActionDataLineBreak()
        {
            if (WebFormsData.Length == 0)
                return "";

            string data = WebFormsData.ToString();
            string processedData = data.Replace("\"", "$[dq];");
            return processedData.Replace('\n'.ToString(), "$[sln];");
        }

        // Export
        public string ExportToHtmlComment(bool AddLine = false)
        {
            string response = Response().Replace("--", "$[dd];");
            if (response[^1] == '-')
                response = response.Substring(0, response.Length - 1) + "$[da];";

            return (AddLine ? "\n" : "") + "<!--" + response + "-->";
        }

        // Using it for SSE Response
        public string ExportToLineBreak(string src = null)
        {
            return "[web-forms]$[sln];" + GetFormsActionDataLineBreak();
        }

        public string GetWebFormsData()
        {
            return WebFormsData.ToString();
        }

        public void AppendForm(WebForms form)
        {
            if (form == null)
                return;

            string otherData = form.GetWebFormsData();
            if (!string.IsNullOrEmpty(otherData))
            {
                if (WebFormsData.Length > 0)
                    WebFormsData.Append('\n');
                WebFormsData.Append(otherData);
            }
        }

        public void Clean()
        {
            WebFormsData.Clear();
        }
    }

    public class Security
    {
        public string SafeValue(string Value)
        {
            if (Value.Length < 1)
                return Value;

            if (Value[0] == '@')
            {
                Value = Value.Remove(0, 1);
                Value = "$[at];" + Value;
            }

            Value = Value
            .Replace('\n'.ToString(), "$[ln];")
            .Replace(",@", "$[co];@")
            .Replace((char)28, '\0')
            .Replace((char)29, '\0')
            .Replace((char)30, '\0')
            .Replace((char)31, '\0');

            return Value;
        }
    }

    // WebForms Place Criteria (WPC) DSL
    public class InputPlace
    {
        public const string Document = ",";
        public const string Window = "`";
        // When Calling TransientDOM, Using Root will Result in the Selection of the Transient Tag.
        public const string Root = "~";
        public const string HTML = ".";
        public const string Head = "^";
        public const string ScreenOrientation = "%";
        public const string All = "*";
        public const string Parent = "/";
        public const string Current = "$";
        public const string Target = "!";
        public const string Upper = "-";

        public static string Id(string Id) => Id;
        public static string Name(string Name) => '(' + Name + ')';
        public static string Name(string Name, int Index) => '(' + Name + ')' + Index;
        public static string AllNames(string Name) => "(" + Name + ")*";
        public static string Tag(string Tag) => '<' + Tag + '>';
        public static string Tag(string Tag, int Index) => '<' + Tag + '>' + Index;
        public static string AllTags(string Tag) => "<" + Tag + ">*";
        public static string Child() => "<>";
        public static string Child(int Index) => "<>" + Index;
        public static string AllChild() => "<>*";
        public static string Class(string Class) => '{' + Class + '}';
        public static string Class(string Class, int Index) => '{' + Class + '}' + Index;
        public static string AllClasses(string Class) => "{" + Class + "}*";
        public static string Attribute(string Name) => '"' + Name + '"';
        public static string Attribute(string Name, int Index) => '"' + Name + '"' + Index;
        public static string AllAttributes(string Name) => "\"" + Name + "\"*";
        // Operator: '^', '$', '*', '~'
        public static string Attribute(string Name, string Value, char Operator = '\0') => '"' + Name + ((Operator != '\0') ? Operator.ToString() : "") + "'" + Value + '"';
        public static string Attribute(string Name, string Value, int Index, char Operator = '\0') => '"' + Name + ((Operator != '\0') ? Operator.ToString() : "") + "'" + Value + '"' + Index;
        public static string AllAttributes(string Name, string Value, char Operator = '\0') => "\"" + Name + ((Operator != '\0') ? Operator.ToString() : "") + "'" + Value + "\"*";
        public static string Query(string Query) => "*" + Query.Replace("=", "$[eq];").Replace("|", "$[vb];").Replace("?", "$[qu];");
        public static string QueryAll(string Query) => "[" + Query.Replace("=", "$[eq];").Replace("|", "$[vb];").Replace("?", "$[qu];");
    }

    public class OutputPlace : InputPlace { }

    // Do not Add any Data Before or After it
    public class Fetch
    {
        private const char RS = (char)30;
        private const char US = (char)31;

        // Method
        public static string Random(int MaxValue) => "@mr" + MaxValue;
        public static string Random(int MinValue, int MaxValue) => "@mr" + MaxValue.ToString() + RS + MinValue.ToString();
        public static string SpaceToChar(string Text, string Character = "-") => "@sc" + Character + RS + Text;
        public static string EncodeURI(string Text) => "@ue" + Text;
        public static string DecodeURI(string Text) => "@ud" + Text;

        public static string Method(string MethodName, object[] Args = null)
        {
            string ReturnValue = "@cm" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? RS + string.Join(US, Args) : "";

            return ReturnValue;
        }

        public static string ModuleMethod(string MethodName, object[] Args = null)
        {
            string ReturnValue = "@cM" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? RS + string.Join(US, Args) : "";

            return ReturnValue;
        }

        // MethodName: The Method Name May Need to Include the Class Name, Separated by a Period. Example: MyClassName.MyMethodName
        public static string WasmMethod(string WasmLanguage, string WasmUrl, string MethodName, object[] Args = null, string Key = ".")
        {
            string ReturnValue = "@wA" + WasmLanguage + RS + WasmUrl + RS + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? RS + string.Join(US, Args) : "";

            return ReturnValue;
        }

        public static string Script(string ScriptText) => "@_" + ScriptText.Replace('\n'.ToString(), "$[ln];");
        public static string LoadUrl(string Url, bool FetchScript = false) => "@lu" + Url + (FetchScript ? RS + "1" : "");
        public static string LoadHtml(string Url, string FetchInputPlace = "", bool FetchScript = false) => "@lh" + Url + RS + (FetchScript ? "1" : "0") + (!string.IsNullOrEmpty(FetchInputPlace) ? RS + FetchInputPlace : "");
        public static string LoadLine(string Url, int Line) => "@ll" + Url + RS + Line.ToString();
        public static string LoadINI(string Url, string Name, bool IsINILike = false) => "@li" + Url + RS + Name + (IsINILike? RS + "1" : "");
        // Name: Name Or Nested Paths. Is Supprt Index (Student[8].Name). Nested Paths Index Starts At 0
        public static string LoadJSON(string Url, string Name) => "@lj" + Url + RS + Name;
        // Name: Name Or XPath; XPath Index Starts At 1
        public static string LoadXML(string Url, string Name) => "@lx" + Url + RS + Name;
        // MethodName: It's Check Function Or Variable
        public static string HasMethod(string MethodName) => "@hm" + MethodName;
        public static string HasModuleMethod(string MethodName) => "@hM" + MethodName;
        // This Method Return True Or False If Key Pressed
        // Modifier: Alt, AltGraph, Control, Meta, Shift, CapsLock, NumLock, ScrollLock
        public static string GetModifierState(string Modifier) => "@ms" + Modifier;

        // Math
        public static string Math(string MethodName, object[] Args = null)
        {
            string ReturnValue = "@M#" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? RS + string.Join(US, Args) : "";

            return ReturnValue;
        }

        // Data
        public const string DateYear = "@dy";
        // Month In JavaScript Is Start From Index 0, Month In WebForms Core Is Start From Index 1 
        public const string DateMonth = "@dm";
        public const string DateDay = "@dd";
        public const string DateDate = "@dD";
        public const string DateHours = "@dh";
        public const string DateMinutes = "@di";
        public const string DateSeconds = "@ds";
        public const string DateMilliseconds = "@dl";

        // String
        public const string Space = "@sp";
        public const string AtSign = "@sa";

        // Tag
        public static string GetId(string InputPlace) => "@$i" + InputPlace;
        public static string GetName(string InputPlace) => "@$n" + InputPlace;
        public static string GetValue(string InputPlace) => "@$v" + InputPlace;
        public static string GetValueLength(string InputPlace) => "@$e" + InputPlace;
        public static string GetClass(string InputPlace) => "@$c" + InputPlace;
        public static string GetStyle(string InputPlace) => "@$s" + InputPlace;
        public static string GetTitle(string InputPlace) => "@$l" + InputPlace;
        public static string GetLabel(string InputPlace) => "@$A" + InputPlace;
        public static string GetText(string InputPlace) => "@$t" + InputPlace;
        public static string GetOuterText(string InputPlace) => "@$o" + InputPlace;
        public static string GetTextLength(string InputPlace) => "@$g" + InputPlace;
        public static string GetAttribute(string InputPlace, string Attribute) => "@$a" + InputPlace + RS + Attribute;
        public static string GetWidth(string InputPlace) => "@$w" + InputPlace;
        public static string GetHeight(string InputPlace) => "@$h" + InputPlace;
        public static string GetIsReadOnly(string InputPlace) => "@$r" + InputPlace;
        public static string GetSelectedIndex(string InputPlace) => "@$x" + InputPlace;
        public static string GetIndex(string InputPlace) => "@$I" + InputPlace;
        public static string GetTextAlign(string InputPlace) => "@$T" + InputPlace;
        public static string GetNodeLength(string InputPlace) => "@$L" + InputPlace;
        public static string GetIsVisible(string InputPlace) => "@$V" + InputPlace;

        // Save
        public static string HasHash(string Hash) => "@HH" + Hash;
        public static string Cookie(string Key) => "@co" + Key;
        public static string Save(string Key = ".") => "@cs" + Key;
        public static string Save(string Key, string ReplaceValue) => "@cs" + Key + RS + ReplaceValue;
        public static string SaveThenRemove(string Key) => "@cl" + Key;
        public static string Cache(string Key = ".") => "@cd" + Key;
        public static string Cache(string Key, string ReplaceValue) => "@cd" + Key + RS + ReplaceValue;
        public static string CacheThenRemove(string Key) => "@ct" + Key;
        public static string SavedLine(string Key = ".", int Line = 0) => "@lL" + Key + "[" + Line;
        public static string SavedLineConsume(string Key = ".") => "@lL" + Key;
        // INIKey: Only Direct Key is Supported
        public static string SavedINI(string Key, string INIKey) => "@lI" + Key + "[" + INIKey;
        public static string CacheLine(string Key = ".", int Line = 0) => "@dL" + Key + "[" + Line;
        public static string CacheLineConsume(string Key = ".") => "@dL" + Key;
        // INIKey: Only Direct Key is Supported
        public static string CacheINI(string Key, string INIKey) => "@dI" + Key + "[" + INIKey;

        // Format Storage
        public static string FormatStore(string Key) => "@fr" + Key;
        public static string FormatStoreByXMLQuery(string Key, string XPath) => "@fx" + Key + RS + XPath;
        public static string FormatStoreByJSONQuery(string Key, string Query) => "@fj" + Key + RS + Query;
        public static string FormatStoreByINI(string Key, string Name) => "@fi" + Key + RS + Name;
        public static string FormatStoreByText(string Key, int Line) => "@ft" + Key + RS + Line.ToString();
        public static string FormatStoreByVariable(string Key) => "@fv" + Key;

        // State
        public static string HasState(string Path) => "@hs" + Path;

        // SSE
        public static string SSEIsConnected(string Path) => "@Sc" + Path;

        // WebSockets
        public static string WebSocketsIsConnected(string Path = "") => "@Wc" + Path;

        // Document
        public const string TabIsActive = "@da";

        // Window
        public const string Href = "@wf";
        public const string PathName = "@wP";
        public static string Query(string Name = "*") => "@wq" + Name;
        public const string Hash = "@wh";
        public const string Host = "@wH";
        public const string HostName = "@wn";
        public const string Port = "@wT";
        public const string Origin = "@wo";
        public const string GetSelection = "@ws";
        public const string ScrollX = "@wx";
        public const string ScrollY = "@wy";
        public static string Segment(int Index) => "@wS" + Index;
        // It Only Works when the String Starts with the Tilde Character (~). The Path is Also Separated by the Slash Character (/). #~/Segment1/Segment2/Segment3
        public static string HashSegment(int Index) => "@wt" + Index;

        // Navigator
        public const string ClipboardText = "@nC";
        public const string GeoLatitude = "@nW";
        public const string GeoLongitude = "@nO";
        public const string Language = "@nL";
        public const string IsOnLine = "@no";
        public const string UserAgent = "@na";

        // Screen
        public const string ScreenWidth = "@sw";
        public const string ScreenHeight = "@sh";
        public const string ScreenOrientationType = "@so";
        public const string ScreenOrientationAngle = "@sr";

        // Performance
        public const string TimeOrigin = "@pt";
        public const string PerformanceNow = "@pn";

        // Event
        public const string Event = "@EV";
        public const string EventSerialize = "@Es";
        public const string EventKey = "@ek";
        public const string EventWhich = "@ew";
        public const string EventClientX = "@ex";
        public const string EventClientY = "@ey";
        public const string EventPageX = "@eX";
        public const string EventPageY = "@eY";
        public const string EventOffsetX = "@Ex";
        public const string EventOffsetY = "@Ey";
        public const string EventDeltaY = "@ed";
    }

    public class WasmLanguage
    {
        // The Suffix "Mediator" Means You Must Call the JavaScript Interface. In Other Cases, the WASM File Should Be Called Directly.
        public const string C = "c";
        public const string CPP = "c";
        public const string Rust = "rust";
        public const string CSharp = "csharp";
        // .NET WebCIL Container. The "dotnet.js" File Should Be Invoked.
        public const string CSharpMediator = "csharp-m";
        public const string GO = "go";
        public const string JAVA = "java";
        public const string AssemblyScript = "as";
    }

    public class HtmlEvent
    {
        public const string OnAbort = "onabort";
        public const string OnAfterPrint = "onafterprint";
        public const string OnBeforePrint = "onbeforeprint";
        public const string OnBeforeUnload = "onbeforeunload";
        public const string OnBlur = "onblur";
        public const string OnCanPlay = "oncanplay";
        public const string OnCanPlayThrough = "oncanplaythrough";
        public const string OnChange = "onchange";
        public const string OnClick = "onclick";
        public const string OnCopy = "oncopy";
        public const string OnCut = "oncut";
        public const string OnDoubleClick = "ondblclick";
        public const string OnDrag = "ondrag";
        public const string OnDragEnd = "ondragend";
        public const string OnDragEnter = "ondragenter";
        public const string OnDragLeave = "ondragleave";
        public const string OnDragOver = "ondragover";
        public const string OnDragStart = "ondragstart";
        public const string OnDrop = "ondrop";
        public const string OnDurationChange = "ondurationchange";
        public const string OnEnded = "onended";
        public const string OnError = "onerror";
        public const string OnFocus = "onfocus";
        public const string OnFocusin = "onfocusin";
        public const string OnFocusOut = "onfocusout";
        public const string OnHashChange = "onhashchange";
        public const string OnInput = "oninput";
        public const string OnInvalid = "oninvalid";
        public const string OnKeyDown = "onkeydown";
        public const string OnKeyPress = "onkeypress";
        public const string OnKeyUp = "onkeyup";
        public const string OnLoad = "onload";
        public const string OnLoadedData = "onloadeddata";
        public const string OnLoadedMetaData = "onloadedmetadata";
        public const string OnLoadStart = "onloadstart";
        public const string OnMouseDown = "onmousedown";
        public const string OnMouseEnter = "onmouseenter";
        public const string OnMouseLeave = "onmouseleave";
        public const string OnMouseMove = "onmousemove";
        public const string OnMouseOver = "onmouseover";
        public const string OnMouseOut = "onmouseout";
        public const string OnMouseUp = "onmouseup";
        public const string OnOffline = "onoffline";
        public const string OnOnline = "ononline";
        public const string OnPageHide = "onpagehide";
        public const string OnPageShow = "onpageshow";
        public const string OnPaste = "onpaste";
        public const string OnPause = "onpause";
        public const string OnPlay = "onplay";
        public const string OnPlaying = "onplaying";
        public const string OnProgress = "onprogress";
        public const string OnRateChange = "onratechange";
        public const string OnResize = "onresize";
        public const string OnReset = "onreset";
        public const string OnScroll = "onscroll";
        public const string OnSearch = "onsearch";
        public const string OnSeeked = "onseeked";
        public const string OnSeeking = "onseeking";
        public const string OnSelect = "onselect";
        public const string OnStalled = "onstalled";
        public const string OnSubmit = "onsubmit";
        public const string OnSuspend = "onsuspend";
        public const string OnTimeUpdate = "ontimeupdate";
        public const string OnToggle = "ontoggle";
        public const string OnTouchCancel = "ontouchcancel";
        public const string OnTouchend = "ontouchend";
        public const string OnTouchMove = "ontouchmove";
        public const string OnTouchStart = "ontouchstart";
        public const string OnUnload = "onunload";
        public const string OnVolumeChange = "onvolumechange";
        public const string OnWaiting = "onwaiting";
        public const string OnWheel = "onwheel";
    }

    public class HtmlEventListener
    {
        public const string Abort = "abort";
        public const string AfterPrint = "afterprint";
        public const string BeforePrint = "beforeprint";
        public const string BeforeUnload = "beforeunload";
        public const string Blur = "blur";
        public const string CanPlay = "canplay";
        public const string CanPlayThrough = "canplaythrough";
        public const string Change = "change";
        public const string Click = "click";
        public const string Copy = "copy";
        public const string Cut = "cut";
        public const string DoubleClick = "dblclick";
        public const string Drag = "drag";
        public const string DragEnd = "dragend";
        public const string DragEnter = "dragenter";
        public const string DragLeave = "dragleave";
        public const string DragOver = "dragover";
        public const string DragStart = "dragstart";
        public const string Drop = "drop";
        public const string DurationChange = "durationchange";
        public const string Ended = "ended";
        public const string Error = "error";
        public const string Focus = "focus";
        public const string Focusin = "focusin";
        public const string FocusOut = "focusout";
        public const string HashChange = "hashchange";
        public const string Input = "input";
        public const string Invalid = "invalid";
        public const string KeyDown = "keydown";
        public const string KeyPress = "keypress";
        public const string KeyUp = "keyup";
        public const string Load = "load";
        public const string LoadedData = "loadeddata";
        public const string LoadedMetaData = "loadedmetadata";
        public const string LoadStart = "loadstart";
        public const string MouseDown = "mousedown";
        public const string MouseEnter = "mouseenter";
        public const string MouseLeave = "mouseleave";
        public const string MouseMove = "mousemove";
        public const string MouseOver = "mouseover";
        public const string MouseOut = "mouseout";
        public const string MouseUp = "mouseup";
        public const string Offline = "offline";
        public const string Online = "online";
        public const string PageHide = "pagehide";
        public const string PageShow = "pageshow";
        public const string Paste = "paste";
        public const string Pause = "pause";
        public const string Play = "play";
        public const string Playing = "playing";
        public const string Progress = "progress";
        public const string RateChange = "ratechange";
        public const string Resize = "resize";
        public const string Reset = "reset";
        public const string Scroll = "scroll";
        public const string Search = "search";
        public const string Seeked = "seeked";
        public const string Seeking = "seeking";
        public const string Select = "select";
        public const string Stalled = "stalled";
        public const string Submit = "submit";
        public const string Suspend = "suspend";
        public const string TimeUpdate = "timeupdate";
        public const string Toggle = "toggle";
        public const string TouchCancel = "touchcancel";
        public const string Touchend = "touchend";
        public const string TouchMove = "touchmove";
        public const string TouchStart = "touchstart";
        public const string Unload = "unload";
        public const string VolumeChange = "volumechange";
        public const string Waiting = "waiting";
        public const string Wheel = "wheel";

        public const string AnimationEnd = "animationend";
        public const string AnimationIteration = "animationiteration";
        public const string AnimationStart = "animationstart";
        public const string ContextMenu = "contextmenu";
        public const string FullScreenChange = "fullscreenchange";
        public const string FullScreenError = "fullscreenerror";
        public const string PopState = "popstate";
        public const string TransitionEnd = "transitionend";
        public const string Storage = "storage";

        // Custom
        public const string ScrollBottom = "scrollbottom"; // Need Call EnableScrollBottomEvent Method Before
        public const string ElementReached = "elementreached"; // Need Call EnableReachedElementEvent Method Before
    }

    public static class ExtensionWebFormsMethods
    {
        public static string Child(this string Text, string Value)
        {
            if (Text.Length < 1)
                return Value;

            return Text + "|" + Value;
        }

        public static string Parent(this string Text)
        {
            if (Text.Length < 1)
                return Text;

            if (Text.EndsWith("|/") || Text.EndsWith("//"))
                return Text + '/';

            return Text + "|/";
        }

        public static string Criteria(this string Text, string Value)
        {
            if (Text.Length < 1)
                return Value;

            return Text + "?" + Value.Replace("|", "$[vb];").Replace("?", "$[qu];");
        }

        public static string AppendFetchReplace(this string Text, string SearchValue, string Value)
        {
            const char FS = (char)28;

            Text = Text.Remove(0, 1);
            return "@;" + SearchValue + FS + Value + FS + Text;
        }

        public static string LineBreak(this string Text, bool EncodeLine = false)
        {
            string encode = EncodeLine ? "$[sln];" : "";
            return Text.Replace("\r\n", encode).Replace("\n", encode).Replace("\r", encode);
        }

        public static string AddDblQuote(this string Text)
        {
            return "\"" + Text + "\"";
        }
        
        public static string JsonNormalize(this string Text)
        {
            if (Text.Length == 0)
                return Text;

            return "\"" + Text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\b", "\\b").Replace("\f", "\\f") + "\"";
        }
    }
}
