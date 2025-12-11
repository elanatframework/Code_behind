using Microsoft.AspNetCore.Http;
using System.Text;

namespace CodeBehind
{
    public class WebForms
    {
        private StringBuilder WebFormsData = new StringBuilder();

        private void Add(string Name, string Value)
        {
            if (WebFormsData.Length > 0)
                WebFormsData.Append('\n');

            WebFormsData.Append(Name);
            WebFormsData.Append('=');
            WebFormsData.Append(Value);
        }

        private void Add(string Name)
        {
            if (WebFormsData.Length > 0)
                WebFormsData.Append('\n');

            WebFormsData.Append(Name);
        }

        private string GetLineByIndex(int Index)
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

        private void UpdateLineByIndex(int Index, string Name, string Value)
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
        public void AddId(string InputPlace, string Id) => Add("ai" + InputPlace, Id);
        public void AddName(string InputPlace, string Name) => Add("an" + InputPlace, Name);
        public void AddValue(string InputPlace, string Value) => Add("av" + InputPlace, Value);
        public void AddClass(string InputPlace, string Class) => Add("ac" + InputPlace, Class);
        public void AddStyle(string InputPlace, string Style) => Add("as" + InputPlace, Style);
        public void AddStyle(string InputPlace, string Name, string Value) => Add("as" + InputPlace, Name + ':' + Value);
        public void AddOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("ao" + InputPlace, Value + '|' + Text + (Selected ? "|1" : ""));
        public void AddCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("ak" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void AddTitle(string InputPlace, string Title) => Add("al" + InputPlace, Title);
        public void AddLabel(string InputPlace, string Label) => Add("aA" + InputPlace, Label);
        public void AddText(string InputPlace, string Text) => Add("at" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddTextToUp(string InputPlace, string Text) => Add("pt" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => Add("aa" + InputPlace, Attribute + '|' + ((Splitter != '\0') ? Splitter : "") + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));
        public void AddTag(string InputPlace, string TagName, string Id = "") => Add("nt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagToUp(string InputPlace, string TagName, string Id = "") => Add("ut" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagBefore(string InputPlace, string TagName, string Id = "") => Add("bt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagAfter(string InputPlace, string TagName, string Id = "") => Add("ft" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddHidden(string InputPlace, string Value, string Id = "") => Add("ah" + InputPlace, Value + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));

        // Set
        public void SetId(string InputPlace, string Id) => Add("si" + InputPlace, Id);
        public void SetName(string InputPlace, string Name) => Add("sn" + InputPlace, Name);
        public void SetValue(string InputPlace, string Value) => Add("sv" + InputPlace, Value);
        public void SetClass(string InputPlace, string Class) => Add("sc" + InputPlace, Class);
        public void SetStyle(string InputPlace, string Style) => Add("ss" + InputPlace, Style);
        public void SetStyle(string InputPlace, string Name, string Value) => Add("ss" + InputPlace, Name + ':' + Value);
        public void SetOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("so" + InputPlace, Value + '|' + Text + (Selected ? "|1" : ""));
        public void SetChecked(string InputPlace, bool Checked = false) => Add("sk" + InputPlace, Checked ? "1" : "0");
        public void SetCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("sk" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void SetTitle(string InputPlace, string Title) => Add("sl" + InputPlace, Title);
        public void SetLabel(string InputPlace, string Label) => Add("sA" + InputPlace, Label);
        public void SetText(string InputPlace, string Text) => Add("st" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void SetAttribute(string InputPlace, string Attribute, string Value = "") => Add("sa" + InputPlace, Attribute + '|' + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));
        public void SetWidth(string InputPlace, string Width) => Add("sw" + InputPlace, Width);
        public void SetWidth(string InputPlace, int Width) => SetWidth(InputPlace, Width.ToString() + "px");
        public void SetHeight(string InputPlace, string Height) => Add("sh" + InputPlace, Height);
        public void SetHeight(string InputPlace, int Height) => SetHeight(InputPlace, Height.ToString() + "px");
        public void SetBackgroundColor(string InputPlace, string Color) => Add("bc" + InputPlace, Color);
        public void SetTextColor(string InputPlace, string Color) => Add("tc" + InputPlace, Color);
        public void SetFontName(string InputPlace, string Name) => Add("fn" + InputPlace, Name);
        public void SetFontSize(string InputPlace, string Size) => Add("fs" + InputPlace, Size);
        public void SetFontSize(string InputPlace, int Size) => Add("fs" + InputPlace, Size + "px");
        public void SetFontBold(string InputPlace, bool Bold) => Add("fb" + InputPlace, Bold ? "1" : "0");
        public void SetVisible(string InputPlace, bool Visible) => Add("vi" + InputPlace, Visible ? "1" : "0");
        public void SetTextAlign(string InputPlace, string Align) => Add("ta" + InputPlace, Align);
        public void SetReadOnly(string InputPlace, bool ReadOnly) => Add("sr" + InputPlace, ReadOnly ? "1" : "0");
        public void SetDisabled(string InputPlace, bool Disabled) => Add("sd" + InputPlace, Disabled ? "1" : "0");
        public void SetFocus(string InputPlace, bool Focus) => Add("sf" + InputPlace, Focus ? "1" : "0");
        public void SetMinLength(string InputPlace, int Length) => Add("mn" + InputPlace, Length.ToString());
        public void SetMaxLength(string InputPlace, int Length) => Add("mx" + InputPlace, Length.ToString());
        public void SetSelectedValue(string InputPlace, string Value) => Add("ts" + InputPlace, Value);
        public void SetSelectedIndex(string InputPlace, int Index) => Add("ti" + InputPlace, Index.ToString());
        public void SetCheckedValue(string InputPlace, string Value, bool Selected) => Add("ks" + InputPlace, Value + "|" + (Selected ? "1" : "0"));
        public void SetCheckedIndex(string InputPlace, int Index, bool Selected) => Add("ki" + InputPlace, Index + "|" + (Selected ? "1" : "0"));

        // Insert
        public void InsertId(string InputPlace, string Id) => Add("ii" + InputPlace, Id);
        public void InsertName(string InputPlace, string Name) => Add("in" + InputPlace, Name);
        public void InsertValue(string InputPlace, string Value) => Add("iv" + InputPlace, Value);
        public void InsertClass(string InputPlace, string Class) => Add("ic" + InputPlace, Class);
        public void InsertStyle(string InputPlace, string Style) => Add("is" + InputPlace, Style);
        public void InsertStyle(string InputPlace, string Name, string Value) => Add("is" + InputPlace, Name + ':' + Value);
        public void InsertOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => Add("io" + InputPlace, Value + '|' + Text + (Selected ? "|1" : ""));
        public void InsertCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => Add("ik" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void InsertTitle(string InputPlace, string Title) => Add("il" + InputPlace, Title);
        public void InsertLabel(string InputPlace, string Label) => Add("iA" + InputPlace, Label);
        public void InsertText(string InputPlace, string Text) => Add("it" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void InsertAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => Add("ia" + InputPlace, Attribute + '|' + ((Splitter != '\0') ? Splitter : "") + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));

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

        // Browser
        public void ChangeUrl(string Url) => Add("cu", Url);
        public void SetHeadTitle(string Title) => Add("ht", Title);
        public void ClipboardWriteText(string Text) => Add("nw", Text);
        public void ScrollTo(int X, int Y) => Add("ws", X + "|" + Y);
        public void HistoryGo(int Steps) => Add("wg", Steps.ToString());
        public void ReloadPage() => Add("lr");
        public void Redirect(string Path) => Add("lh", Path);

        // Increase
        public void IncreaseMinLength(string InputPlace, int Value) => Add("+n" + InputPlace, Value.ToString());
        public void IncreaseMaxLength(string InputPlace, int Value) => Add("+x" + InputPlace, Value.ToString());
        public void IncreaseFontSize(string InputPlace, int Value) => Add("+f" + InputPlace, Value.ToString());
        public void IncreaseWidth(string InputPlace, int Value) => Add("+w" + InputPlace, Value.ToString());
        public void IncreaseHeight(string InputPlace, int Value) => Add("+h" + InputPlace, Value.ToString());
        public void IncreaseValue(string InputPlace, int Value) => Add("+v" + InputPlace, Value.ToString());

        // Decrease
        public void DecreaseMinLength(string InputPlace, int Value) => Add("-n" + InputPlace, Value.ToString());
        public void DecreaseMaxLength(string InputPlace, int Value) => Add("-x" + InputPlace, Value.ToString());
        public void DecreaseFontSize(string InputPlace, int Value) => Add("-f" + InputPlace, Value.ToString());
        public void DecreaseWidth(string InputPlace, int Value) => Add("-w" + InputPlace, Value.ToString());
        public void DecreaseHeight(string InputPlace, int Value) => Add("-h" + InputPlace, Value.ToString());
        public void DecreaseValue(string InputPlace, int Value) => Add("-v" + InputPlace, Value.ToString());

        // Event
        // ConstructorName: mouseevent, keyboardevent, uievent, focusevent, inputevent, event
        // All Method In Event Section Only Support Dynamic Args Once
        public void TriggerEvent(string InputPlace, string HtmlEventListener, string ConstructorName = null) => Add("TE" + InputPlace, HtmlEventListener + (!string.IsNullOrEmpty(ConstructorName)? "|" + ConstructorName : ""));
        public void SetPostEvent(string InputPlace, string HtmlEvent) => Add("Ep" + InputPlace, HtmlEvent);
        public void SetPostEventView(string InputPlace, string HtmlEvent) => Add("Ep" + InputPlace, HtmlEvent + "|+");
        public void SetPostEventTo(string InputPlace, string HtmlEvent, string OutputPlace) => Add("Ep" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetPostEventListener(string InputPlace, string HtmlEventListener) => Add("EP" + InputPlace, HtmlEventListener);
        public void SetPostEventListenerView(string InputPlace, string HtmlEventListener) => Add("EP" + InputPlace, HtmlEventListener + "|+");
        public void SetPostEventListenerTo(string InputPlace, string HtmlEventListener, string OutputPlace) => Add("EP" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetGetEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eg" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Eg" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EG" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EG" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Ea" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Ea" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EA" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EA" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("El" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("El" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EL" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EL" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eo" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Eo" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EO" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EO" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetTraceEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Er" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetTraceEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Er" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetTraceEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("ER" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetTraceEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("ER" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetConnectEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Ec" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetConnectEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => Add("Ec" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetConnectEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EC" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetConnectEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => Add("EC" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetHeadEvent(string InputPlace, string HtmlEvent, string Path = null) => Add("Eh" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetHeadEventListener(string InputPlace, string HtmlEventListener, string Path = null) => Add("EH" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetTagEvent(string InputPlace, string HtmlEvent, string OutputPlace) => Add("Et" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetTagEventListener(string InputPlace, string HtmlEventListener, string OutputPlace) => Add("ET" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetCommentEvent(string InputPlace, string HtmlEvent, string Index = null, string OutputPlace = null) => Add("Eb" + InputPlace, HtmlEvent + "|" + Index + "|" + OutputPlace);
        public void SetCommentEvent(string InputPlace, string HtmlEvent, int Index, string OutputPlace = null) => SetCommentEvent(InputPlace, HtmlEvent, Index.ToString(), OutputPlace = null);
        public void SetCommentEventListener(string InputPlace, string HtmlEventListener, string Index = null, string OutputPlace = null) => Add("EB" + InputPlace, HtmlEventListener + "|" + Index + "|" + OutputPlace);
        public void SetCommentEventListener(string InputPlace, string HtmlEventListener, int Index, string OutputPlace = null) => SetCommentEventListener(InputPlace, HtmlEventListener, Index.ToString(), OutputPlace);
        public void SetWasmEvent(string InputPlace, string HtmlEvent, string WasmLanguage, string WasmUrl, string MethodName, string[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? string.Join(",", Args) : "";

            Add("Ey" + InputPlace, HtmlEvent + "|" + WasmLanguage + "|" + WasmUrl + "|" + MethodName + "|" + ArgsJoin + "|" + OutputPlace);
        }
        public void SetWasmEventListener(string InputPlace, string HtmlEventListener, string WasmLanguage, string WasmUrl, string MethodName, string[] Args = null, string OutputPlace = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? string.Join(",", Args) : "";

            Add("EY" + InputPlace, HtmlEventListener + "|" + WasmLanguage + "|" + WasmUrl + "|" + MethodName + "|" + ArgsJoin + "|" + OutputPlace);
        }
        public void SetWebSocketEvent(string InputPlace, string HtmlEvent, string Path) => Add("Ew" + InputPlace, HtmlEvent + "|" + Path);
        public void SetWebSocketEventListener(string InputPlace, string HtmlEventListener, string Path) => Add("EW" + InputPlace, HtmlEventListener + "|" + Path);
        public void SetSSEEvent(string InputPlace, string HtmlEvent, string Path, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("Ee" + InputPlace, HtmlEvent + "|" + Path + "|" + (ShouldReconnect? "1" : "0") + "|" + ReconnectTryTimeout);
        public void SetSSEEvent(string InputPlace, string HtmlEvent, string Path, string OutputPlace, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("Ee" + InputPlace, HtmlEvent + "|" + Path + "|" + (ShouldReconnect ? "1" : "0") + "|" + ReconnectTryTimeout + "|" + OutputPlace);
        public void SetSSEEventListener(string InputPlace, string HtmlEventListener, string Path, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("EE" + InputPlace, HtmlEventListener + "|" + Path + "|" + (ShouldReconnect? "1" : "0") + "|" + ReconnectTryTimeout);
        public void SetSSEEventListener(string InputPlace, string HtmlEventListener, string Path, string OutputPlace, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("EE" + InputPlace, HtmlEventListener + "|" + Path + "|" + (ShouldReconnect ? "1" : "0") + "|" + ReconnectTryTimeout + "|" + OutputPlace);
        public void SetSendEvent(string InputPlace, string HtmlEvent, string Data, string Path = null, string Method = "POST", bool IsMultiPart = false, string ContentType = "text/plain", string OutputPlace = null) => Add("En" + InputPlace, HtmlEvent + "|" + Data.Replace('\n'.ToString(), "$[ln];").Replace("\"", "$[dq];").Replace("'", "$[sq];") + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + Method + "|" + (IsMultiPart ? "1" : "0") + "|" + ContentType + "|" + OutputPlace);
        public void SetSendEventListener(string InputPlace, string HtmlEventListener, string Data, string Path = null, string Method = "POST", bool IsMultiPart = false, string ContentType = "text/plain", string OutputPlace = null) => Add("EN" + InputPlace, HtmlEventListener + "|" + Data.Replace('\n'.ToString(), "$[ln];") + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + Method + "|" + (IsMultiPart ? "1" : "0") + "|" + ContentType + "|" + OutputPlace);
        public void SetMasterPagesEvent(string InputPlace, string HtmlEvent, string OutputPlace = null) => Add("Eu" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetMasterPagesEventListener(string InputPlace, string HtmlEventListener, string OutputPlace = null) => Add("EU" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetPreventDefaultEvent(string InputPlace, string HtmlEvent) => Add("Ed" + InputPlace, HtmlEvent);
        public void SetPreventDefaultEventListener(string InputPlace, string HtmlEventListener) => Add("ED" + InputPlace, HtmlEventListener);
        public void SetStopPropagationEvent(string InputPlace, string HtmlEvent) => Add("Es" + InputPlace, HtmlEvent);
        public void SetStopPropagationEventListener(string InputPlace, string HtmlEventListener) => Add("ES" + InputPlace, HtmlEventListener);
        public void SetMethodEvent(string InputPlace, string HtmlEvent, string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("Em" + InputPlace, HtmlEvent + "|" + MethodName + ArgsJoin);
        }
        public void SetMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("EM" + InputPlace, HtmlEventListener + "|" + MethodName + ArgsJoin);
        }
        public void SetModuleMethodEvent(string InputPlace, string HtmlEvent, string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("Ex" + InputPlace, HtmlEvent + "|" + MethodName + ArgsJoin);
        }
        public void SetModuleMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("EX" + InputPlace, HtmlEventListener + "|" + MethodName + ArgsJoin);
        }
        public void AssignConfirmEvent(string InputPlace, string HtmlEvent, string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel") => Add("Ef" + InputPlace, HtmlEvent + "|" + (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none"? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" :  OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void RemovePostEvent(string InputPlace, string HtmlEvent) => Add("Rp" + InputPlace, HtmlEvent);
        public void RemovePostEventListener(string InputPlace, string HtmlEventListener) => Add("RP" + InputPlace, HtmlEventListener);
        public void RemoveGetEvent(string InputPlace, string HtmlEvent) => Add("Rg" + InputPlace, HtmlEvent);
        public void RemoveGetEventListener(string InputPlace, string HtmlEventListener) => Add("RG" + InputPlace, HtmlEventListener);
        public void RemovePatchEvent(string InputPlace, string HtmlEvent) => Add("Ra" + InputPlace, HtmlEvent);
        public void RemovePatchEventListener(string InputPlace, string HtmlEventListener) => Add("RA" + InputPlace, HtmlEventListener);
        public void RemoveDeleteEvent(string InputPlace, string HtmlEvent) => Add("Rl" + InputPlace, HtmlEvent);
        public void RemoveDeleteEventListener(string InputPlace, string HtmlEventListener) => Add("RL" + InputPlace, HtmlEventListener);
        public void RemoveHeadEvent(string InputPlace, string HtmlEvent) => Add("Rh" + InputPlace, HtmlEvent);
        public void RemoveHeadEventListener(string InputPlace, string HtmlEventListener) => Add("RH" + InputPlace, HtmlEventListener);
        public void RemoveOptionsEvent(string InputPlace, string HtmlEvent) => Add("Ro" + InputPlace, HtmlEvent);
        public void RemoveOptionsEventListener(string InputPlace, string HtmlEventListener) => Add("RO" + InputPlace, HtmlEventListener);
        public void RemoveTraceEvent(string InputPlace, string HtmlEvent) => Add("Rr" + InputPlace, HtmlEvent);
        public void RemoveTraceEventListener(string InputPlace, string HtmlEventListener) => Add("RR" + InputPlace, HtmlEventListener);
        public void RemoveConnectEvent(string InputPlace, string HtmlEvent) => Add("Rc" + InputPlace, HtmlEvent);
        public void RemoveConnectEventListener(string InputPlace, string HtmlEventListener) => Add("RC" + InputPlace, HtmlEventListener);
        public void RemoveTagEvent(string InputPlace, string HtmlEvent) => Add("Rt" + InputPlace, HtmlEvent);
        public void RemoveTagEventListener(string InputPlace, string HtmlEventListener) => Add("RT" + InputPlace, HtmlEventListener);
        public void RemoveCommentEvent(string InputPlace, string HtmlEvent) => Add("Rb" + InputPlace, HtmlEvent);
        public void RemoveCommentEventListener(string InputPlace, string HtmlEventListener) => Add("RB" + InputPlace, HtmlEventListener);
        public void RemoveWasmEvent(string InputPlace, string HtmlEvent) => Add("Ry" + InputPlace, HtmlEvent);
        public void RemoveWasmEventListener(string InputPlace, string HtmlEventListener) => Add("RY" + InputPlace, HtmlEventListener);
        public void RemoveWebSocketEvent(string InputPlace, string HtmlEvent) => Add("Rw" + InputPlace, HtmlEvent);
        public void RemoveWebSocketEventListener(string InputPlace, string HtmlEventListener) => Add("RW" + InputPlace, HtmlEventListener);
        public void RemoveSSEEvent(string InputPlace, string HtmlEvent) => Add("Re" + InputPlace, HtmlEvent);
        public void RemoveSSEEventListener(string InputPlace, string HtmlEventListener) => Add("RE" + InputPlace, HtmlEventListener);
        public void RemoveSendEvent(string InputPlace, string HtmlEvent) => Add("Rn" + InputPlace, HtmlEvent);
        public void RemoveSendEventListener(string InputPlace, string HtmlEventListener) => Add("RN" + InputPlace, HtmlEventListener);
        public void RemovePreventDefaultEvent(string InputPlace, string HtmlEvent) => Add("Rd" + InputPlace, HtmlEvent);
        public void RemovePreventDefaultEventListener(string InputPlace, string HtmlEventListener) => Add("RD" + InputPlace, HtmlEventListener);
        public void RemoveMasterPagesEvent(string InputPlace, string HtmlEvent) => Add("Ru" + InputPlace, HtmlEvent);
        public void RemoveMasterPagesEventListener(string InputPlace, string HtmlEventListener) => Add("RU" + InputPlace, HtmlEventListener);
        public void RemoveStopPropagationEvent(string InputPlace, string HtmlEvent) => Add("Rs" + InputPlace, HtmlEvent);
        public void RemoveStopPropagationEventListener(string InputPlace, string HtmlEventListener) => Add("RS" + InputPlace, HtmlEventListener);
        public void RemoveMethodEvent(string InputPlace, string HtmlEvent, string MethodName) => Add("Rm" + InputPlace, HtmlEvent + "|" + MethodName);
        public void RemoveMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName) => Add("RM" + InputPlace, HtmlEventListener + "|" + MethodName);
        public void RemoveModuleMethodEvent(string InputPlace, string HtmlEvent, string MethodName) => Add("Rx" + InputPlace, HtmlEvent + "|" + MethodName);
        public void RemoveModuleMethodEventListener(string InputPlace, string HtmlEventListener, string MethodName) => Add("RX" + InputPlace, HtmlEventListener + "|" + MethodName);
        public void RemoveConfirmEvent(string InputPlace, string HtmlEvent) => Add("Rf" + InputPlace, HtmlEvent);

        // Custom Event
        // Watch: attribute, style, text, children, value
        // Compare: greater, less, equal, notequal, includes, startswith, endswith, matches, changed, inrange, lengthgreater, lengthless, lengthequal
        // Range: Only Use For Compare With inrange Value. Split By Comma ","
        // Key: Only Use For Watch With attribute And style Value
        public void CreateCustomDOMEvent(string InputPlace, string EventName ,string Watch, string Key, string Compare, string Value, string Range, bool Immediate = false, int Delay = 0) => Add("eC" + InputPlace, EventName + "|" + Watch + "|" + Key + "|" + Compare + "|" + Value + "|" + Range + "|" + (Immediate?  "1" : "0") + "|" + Delay.ToString());
        public void EnableScrollBottomEvent(bool Enable = true) => Add("eb", Enable? "1" : "0");
        public void EnableReachedElementEvent(string InputPlace, bool Once, bool Enable = true) => Add("er" + InputPlace, (Once ? "1" : "0") + "|" + (Enable? "1" : "0"));

        // Module
        public void LoadModule(string ModulePath, string[] Methods) => Add("Ml", ModulePath + ((Methods.Length > 0) ? "|" + string.Join("|", Methods) : ""));
        public void UnloadModule(string ModulePath) => Add("Mu", ModulePath);
        public void DeleteModuleMethod(string MethodName) => Add("Md", MethodName);

        // Unit Testing
        // InputPlace Is Actual, Expected is Tag/OutputPlace
        public void AssertEqual(string InputPlace, string Tag) => Add("At" + InputPlace, Tag.Replace('\n'.ToString(), "$[ln];"));
        public void AssertEqualByOutputPlace(string InputPlace, string OutputPlace) => Add("Ao" + InputPlace, OutputPlace);

        // Service Worker
        public void ServiceWorkerRegister() => Add("wR");
        public void ServiceWorkerPreCacheStatic(string[] PathList) => Add("wp",string.Join("|", PathList));
        public void ServiceWorkerDynamicCache(string Path, int Seconds = 0) => Add("wc", Path + (Seconds > 0 ? "|" + Seconds : ""));
        public void ServiceWorkerDeleteDynamicCache() => Add("wd");
        public void ServiceWorkerDeleteDynamicCache(string Path) => Add("wd", Path);
        public void ServiceWorkerDynamicCacheTTLUpdate(string Path, int Seconds = 0) => Add("wt", Path + (Seconds > 0 ? "|" + Seconds : ""));
        // Path: Support Wildcard Automatically And Also Support Regex If Use "re:" Before Pattern
        // Type: Type Is Cache Strategy. cachefirst, networkfirst, cacheonly, networkonly, stalerevalidate (Fast From Cache, Updates Simultaneously From The Network)
        // CacheDynamic: If True, Any Successful Network Response For That Route Will Be Stored In The Dynamic Cache.
        public void ServiceWorkerRouteSet(string Path, string Type, bool CacheDynamic = false) => Add("wr", Path + "|" + Type + (CacheDynamic? "|1" : ""));
        public void ServiceWorkerRouteAlias(string Path, string To) => Add("wa", Path + "|" + To);
        // Delete All Route And Alias
        public void ServiceWorkerDeleteRoute() => Add("wD");
        public void ServiceWorkerDeleteRoute(string Path) => Add("wD", Path);

        // Cookie
        public void SetCookie(string Key, string Value, int Seconds, string Path = null) => Add("sC", Key + "|" + Value + "|" + Seconds + (!string.IsNullOrEmpty(Path) ? "|" + Path : ""));

        // Save/Session Cache
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
        public void SaveAttribute(string InputPlace, string Attribute, string Key = ".") => Add("@ga" + InputPlace, Key + '|' + Attribute);
        public void SaveWidth(string InputPlace, string Key = ".") => Add("@gw" + InputPlace, Key);
        public void SaveHeight(string InputPlace, string Key = ".") => Add("@gh" + InputPlace, Key);
        public void SaveReadOnly(string InputPlace, string Key = ".") => Add("@gr" + InputPlace, Key);
        public void SaveSelectedIndex(string InputPlace, string Key = ".") => Add("@gx" + InputPlace, Key);
        public void SaveTextAlign(string InputPlace, string Key = ".") => Add("@gT" + InputPlace, Key);
        public void SaveNodeLength(string InputPlace, string Key = ".") => Add("@gL" + InputPlace, Key);
        public void SaveVisible(string InputPlace, string Key = ".") => Add("@gV" + InputPlace, Key);
        public void SaveUrl(string Url, bool FetchScript = false, string Key = ".") => Add("@gu", Key + "|" + Url + (FetchScript ? "|1" : ""));
        public void SaveIndex(string InputPlace, string Key = ".") => Add("@gI" + InputPlace, Key);
        public void RemoveSessionCache(string CacheKey) => Add("rs", CacheKey);
        public void RemoveAllSessionCache() => Add("rs", "*");
        public void SetSessionCache() => Add("cs", "*");
        public void AddSessionCacheValue(string CacheKey, string Value) => Add("SA", CacheKey + "|" + Value.Replace('\n'.ToString(), "$[ln];"));
        public void InsertSessionCacheValue(string CacheKey, string Value) => Add("SI", CacheKey + "|" + Value.Replace('\n'.ToString(), "$[ln];"));

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
        public void CacheAttribute(string InputPlace, string Attribute, string Key = ".") => Add("@ca" + InputPlace, Key + '|' + Attribute);
        public void CacheWidth(string InputPlace, string Key = ".") => Add("@cw" + InputPlace, Key);
        public void CacheHeight(string InputPlace, string Key = ".") => Add("@ch" + InputPlace, Key);
        public void CacheReadOnly(string InputPlace, string Key = ".") => Add("@cr" + InputPlace, Key);
        public void CacheSelectedIndex(string InputPlace, string Key = ".") => Add("@cx" + InputPlace, Key);
        public void CacheTextAlign(string InputPlace, string Key = ".") => Add("@cT" + InputPlace, Key);
        public void CacheNodeLength(string InputPlace, string Key = ".") => Add("@cL" + InputPlace, Key);
        public void CacheVisible(string InputPlace, string Key = ".") => Add("@cV" + InputPlace, Key);
        public void CacheUrl(string Url, bool FetchScript = false, string Key = ".") => Add("@cu", Key + "|" + Url + (FetchScript ? "|1" : ""));
        public void CacheIndex(string InputPlace, string Key = ".") => Add("@cI" + InputPlace, Key);
        public void RemoveCache(string CacheKey) => Add("rd", CacheKey);
        public void RemoveAllCache() => Add("rd", "*");
        public void SetCache(int Second) => Add("cd", Second.ToString());
        public void SetCache() => Add("cd", "*");
        public void AddCacheValue(string CacheKey, string Value) => Add("CA", CacheKey + "|" + Value.Replace('\n'.ToString(), "$[ln];"));
        public void InsertCacheValue(string CacheKey, string Value) => Add("CI", CacheKey + "|" + Value.Replace('\n'.ToString(), "$[ln];"));

        // Call
        public void LoadUrl(string InputPlace, string Url) => Add("lu" + InputPlace, Url);
        public void RunActionControls(string ActionControls, string Index = null, bool WithoutWebFormsSection = false, bool UseCurrentEvent = true) => Add("lA", (UseCurrentEvent ? "1" : "0") + "|" + (WithoutWebFormsSection ? "1" : "0") + "|" + Index + "|" + ActionControls);
        public void RunActionControls(string ActionControls, int Index, bool WithoutWebFormsSection = false, bool UseCurrentEvent = true) => RunActionControls(ActionControls, Index.ToString(), WithoutWebFormsSection, UseCurrentEvent);
        public void CallScript(string ScriptText) => Add("_", ScriptText.Replace('\n'.ToString(), "$[ln];"));
        public void CallMethod(string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("lm", MethodName + ArgsJoin);
        }
        public void CallModuleMethod(string MethodName, string[] Args = null)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? "|" + string.Join("|", Args) : "";

            Add("lM", MethodName + ArgsJoin);
        }
        public void CallPostBack(string FormInputPlace, string OutputPlace = null) => Add("Lp", "1" + "|" + FormInputPlace + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallTagBack(string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lt", (UseCurrentEvent? "1": "0") + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallCommentBack(string Index = null, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LC", (UseCurrentEvent? "1": "0") + "|" + Index + "|" + OutputPlace);
        public void CallCommentBack(int Index, string OutputPlace = null, bool UseCurrentEvent = true) => CallCommentBack(Index.ToString(), OutputPlace, UseCurrentEvent);
        public void CallWasmBack(string WasmLanguage, string WasmUrl, string MethodName, string[] Args = null, string OutputPlace = null, bool UseCurrentEvent = true)
        {
            string ArgsJoin = "";

            if (Args != null)
                ArgsJoin = (Args.Length > 0) ? string.Join(",", Args) : "";

            Add("Ly", (UseCurrentEvent ? "1" : "0") + "|" + WasmLanguage + "|" + WasmUrl + "|" + MethodName + "|" + ArgsJoin + "|" + OutputPlace);
        }
        public void CallWebSocketBack(string Path, bool UseCurrentEvent = true) => Add("Lw", (UseCurrentEvent? "1": "0") + "|" + Path);
        public void CallSSEBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true, bool ShouldReconnect = true, int ReconnectTryTimeout = 3000) => Add("Ls", (UseCurrentEvent? "1": "0") + "|" + Path + "|" + (ShouldReconnect ? "1" : "0") + "|" + ReconnectTryTimeout + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallGetBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lg", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallPutBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lu", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallPatchBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LP", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallDeleteBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Ld", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallHeadBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lh", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallOptionsBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lo", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallTraceBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LT", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallConnectBack(string Path, string OutputPlace = null, bool UseCurrentEvent = true) => Add("Lc", (UseCurrentEvent ? "1" : "0") + "|" + Path + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));
        public void CallSendBack(string Path, string Method, bool IsMultiPart, string ContentType, string Data, string OutputPlace = null, bool UseCurrentEvent = true) => Add("LS", (UseCurrentEvent ? "1" : "0") + "|" + Path + "|" + Method + "|" + (IsMultiPart ? "1" : "0") + "|" + ContentType + "|" + Data.Replace('\n'.ToString(), "$[ln];").Replace('|'.ToString(), "$[vb];") + (!string.IsNullOrEmpty(OutputPlace) ? "|" + OutputPlace : ""));

        // Update
        public void Increase(string InputPlace, float Value) => Add("gt" + InputPlace, "i|" + Value.ToString());
        public void Decrease(string InputPlace, float Value) => Add("gt" + InputPlace, "i|" + (Value * -1).ToString());
        // If You Don't Use Deep Mode, Any Tags Inside The Current Tag Will Simply Be Treated As Strings. Deep Mode Does Not Remove Inner Elements.
        public void Replace(string InputPlace, string Value, string NewValue, bool AlsoStartTag = false, bool Deep = false)
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

            Add("gt" + InputPlace, "r|" + Value + "|" + NewValue + "|" + (AlsoStartTag ? "1" : "0") + "|" + (Deep ? "1" : "0"));
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

            Add("gt" + InputPlace, "s|" + Value + "|" + NewValue);
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

        public void AssignInterval(int MiliSecond, int Index = -1)
        {
            string currentLine = GetLineByIndex(Index);
            if (string.IsNullOrEmpty(currentLine))
                return;

            string[] parts = currentLine.Split('=', 2);
            string newName = "(" + MiliSecond + ")" + parts[0];
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

        public void AssignIntervalChange(float MiliSecond, int Index = -1)
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

            string newName = "(" + MiliSecond + ")" + currentName;
            string newValue = parts.Length > 1 ? parts[1] : "";

            UpdateLineByIndex(Index, newName, newValue);
        }

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
        public void GoTo(int Line, int Repeat = 1) => Add("&", Line + "|" + Repeat.ToString());
        public void GoTo(string Index, int Repeat = 1) => Add("&", "#" + Index + "|" + Repeat.ToString());
        
        // Start
        public void StartTransientDOM(string InputPlace) => Add("td", InputPlace);
        public void EndTransientDOM() => Add("td", ";");

        // Message
        // Type: warning, problem, help, success, none
        public void Alert(string Text, string Type = "none", string Title = "Alert", string OkText = "OK") => Add("Al", Text + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Alert" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText));
        public void Message(string Text, string Type = "none", int Duration = 0) => Add("me", Text + "|" + (Type == "none" ? "" : Type) + "|" + (Duration == 0 ? "" : Duration));

        // Type: log, info, warn, error, debug, trace, group, groupend, table
        public void ConsoleMessage(string Text, string Type = "log") => Add("mc", Text.Replace('\n'.ToString(), "$[ln];") + (Type == "log" ? "" : "|" + Type));
        public void ConsoleMessageAssert(string Text, string Condition) => Add("ma", Text.Replace('\n'.ToString(), "$[ln];") + "|" + Condition);

        // Enable
        public void EnableWebSocket(bool Enable = true) => Add("ew", Enable ? "1" : "0");
        public void EnableWebSocketOnce() => Add("ew", "@");

        // Use
        public void UseWebSocket(string Path) => Add("uw", Path);
        public void UseOnlyChangeUpdate(string InputPlace) => Add("uo" + InputPlace);

        // Condition
        // Type: warning, problem, help, success, none
        // Interval: Value 0 Is Await (If Is True All Next Action Controls Waiting For It), Value -1 Is Sync Check Once (Is Support Bracket Or Next Action Control), Value > 0 Is Async And Is Wait Based On Time Repetition Until It Becomes True (Is Support Bracket Or Next Action Control)
        public void ConfirmIsTrueAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "ct", (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void ConfirmIsFalseAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "cf", (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void IsGreaterThan(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "gt", FirstValue + "|" + SecondValue);
        public void IsLessThan(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "lt", FirstValue + "|" + SecondValue);
        public void IsEqualTo(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "et", FirstValue + "|" + SecondValue);
        public void IsNotEqualTo(string FirstValue, string SecondValue, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "Nt", FirstValue + "|" + SecondValue);
        public void Exist(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "ex", Value);
        public void NotExist(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nx", Value);
        public void IsTrue(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "tr", Value);
        public void IsFalse(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "fa", Value);
        public void IsMatchMedia(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "mm", Value);
        public void IsNotMatchMedia(string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nm", Value);
        public void Include(string Text, string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "In", Value + "|" + Text);
        public void NotInclude(string Text, string Value, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "Nn", Value + "|" + Text);
        public void ElementExists(string InputPlace, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "eE", InputPlace);
        public void ElementNotExists(string InputPlace, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "nE", InputPlace);
        public void IsRegexMatch(string Value, string Pattern, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "re", Value + "|" + Pattern);
        public void IsRegexNotMatch(string Value, string Pattern, int Interval = -1) => Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "rn", Value + "|" + Pattern);
        public void Break() => Add(";");
        public void StartBracket() => Add("{");
        public void EndBracket() => Add("}");

        // Async
        public void Async() => Add("{(a)");
        public void Delay(int MiliSecond) => Add("De", MiliSecond.ToString());

        // Format Storage
        public void CreateFormatStorage(string Key, string Data) => Add(".C", Key + "|" + Data);
        public void DeleteFormatStorage(string Key) => Add(".D", Key);
        public void AddJSON(string Key, string Path, string Value) => Add(".a", Key + "|j|" + Value + "|" + Path);
        // Name: For Support Attribute, Set @ Before Name. Add Double @ (@@) For Support Dynamic Args In Attribute
        public void AddXML(string Key, string Path, string Name, string Value = null)
        {
            if (!string.IsNullOrEmpty(Name))
                if (Name[0] == '@')
                {
                    Name = Name.Remove(0);
                    Name = "$[at];" + Name;
                }

            Add(".a", Key + "|x|" + Name.Replace("@", "$[at];") + "|" + Value + "|" + Path);
        }
        public void AddINI(string Key, string Path, string Value, bool IsINILike = false) => Add(".a", Key + "|i|" + (IsINILike ? "1" : "0") + "|" + Value + "|" + Path);
        public void AddTextLine(string Key, int Line, string Text) => Add(".a", Key + "|t|" + Text + "|" + Line);
        public void AddVariable(string Key, string Value) => Add(".a", Key + "|v|" + Value);
        public void UpdateJSON(string Key, string Path, string Value) => Add(".u", Key + "|j|" + Value + "|" + Path);
        public void UpdateXML(string Key, string Path, string Value) => Add(".u", Key + "|x|" + Value + "|" + Path);
        public void UpdateINI(string Key, string Path, string Value, bool IsINILike = false) => Add(".u", Key + "|i|" + (IsINILike ? "1" : "0") + "|" + Value + "|" + Path);
        public void UpdateTexLine(string Key, int Line, string Text) => Add(".u", Key + "|t|" + Text + "|" + Line);
        public void UpdateVariable(string Key, string Value) => Add(".u", Key + "|v|" + Value);
        public void IncreaceVariable(string Key, int Value) => Add(".i", Key + "|v|" + Value);
        public void DecreaseVariable(string Key, int Value) => IncreaceVariable(Key, Value * -1);
        public void DeleteJSON(string Key, string Path) => Add(".d", Key + "|j|" + Path);
        public void DeleteXML(string Key, string Path) => Add(".d", Key + "|x|" + Path);
        public void DeleteINI(string Key, string Path, bool IsINILike = false) => Add(".d", Key + "|i|" + IsINILike + "|" + Path);
        public void DeleteTextLine(string Key, int Line) => Add(".d", Key + "|t|" + Line);
        public void DeleteVariable(string Key) => Add(".d", Key + "|v");

        // Inject
        // Need Add @: To First Of String
        public string Inject(string Value) => "$[" + Value + "];";

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

        // Overload
        public string Response(HttpContext context)
        {
            SetHeaders(context);
            return Response();
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
        public string ExportToWebFormsTag(string src = null)
        {
            return "<web-forms ac=\"" + GetFormsActionDataLineBreak() + "\"" + (!string.IsNullOrEmpty(src) ? " src=\"" + src + "\"" : "") + "></web-forms>";
        }

        public string ExportToLineBreak(string src = null)
        {
            return "[web-forms]$[sln];" + GetFormsActionDataLineBreak();
        }

        // Overload
        public string ExportToWebFormsTag(string Width, string Height, string src = null)
        {
            return "<web-forms ac=\"" + GetFormsActionDataLineBreak() + "\" width=\"" + Width + "\" height=\"" + Height + "\"" + (!string.IsNullOrEmpty(src) ? " src=\"" + src + "\"" : "") + "></web-forms>";
        }

        // Overload
        public string ExportToWebFormsTag(int Width, int Height, string src = null)
        {
            return ExportToWebFormsTag(Width.ToString() + "px", Height.ToString() + "px", src);
        }

        public string DoneToWebFormsTag(string Id = null)
        {
            return "<web-forms ac=\"" + GetFormsActionDataLineBreak() + "\"" + (!string.IsNullOrEmpty(Id) ? " id=\"" + Id + "\" done=\"true\"" : "") + "></web-forms>";
        }
        public string ExportToHtmlComment(bool AddLine = false)
        {
            return (AddLine ? "\n" : "") + "<!--" + Response() + "-->";
        }

        public string GetWebFormsData()
        {
            return WebFormsData.ToString();
        }

        public void AppendForm(WebForms form)
        {
            if (form == null) return;

            string otherData = form.GetWebFormsData();
            if (!string.IsNullOrEmpty(otherData))
            {
                if (WebFormsData.Length > 0)
                    WebFormsData.Append('\n');
                WebFormsData.Append(otherData);
            }
        }

        public void SetHeaders(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "text/plain");
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

            Value = Value.Replace('\n'.ToString(), "$[ln];");
            Value = Value.Replace("|", "$[vb];");
            Value = Value.Replace(",@", "$[co];@");

            return Value;
        }
    }

    public class InputPlace
    {
        public const string Window = "`";
        public const string Root = "~";
        public const string Current = "$";
        public const string Target = "!";
        public const string Upper = "-";
        public const string Head = "^";
        public const string ScreenOrientation = "%";

        public static string Id(string Id) => Id;
        public static string Name(string Name) => '(' + Name + ')';
        public static string Name(string Name, int Index) => '(' + Name + ')' + Index;
        public static string Tag(string Tag) => '<' + Tag + '>';
        public static string Tag(string Tag, int Index) => '<' + Tag + '>' + Index;
        public static string Class(string Class) => '{' + Class + '}';
        public static string Class(string Class, int Index) => '{' + Class + '}' + Index;
        public static string Query(string Query) => "*" + Query.Replace("=", "$[eq];");
        public static string QueryAll(string Query) => "[" + Query.Replace("=", "$[eq];");
    }

    public class OutputPlace : InputPlace { }

    /// <summary>
    /// Do Not Add Any Data Before Or After It
    /// </summary>
    public class Fetch
    {
        // Method
        public static string Random(int MaxValue) => "@mr" + MaxValue;
        public static string Random(int MinValue, int MaxValue) => "@mr" + MaxValue + "," + MinValue;
        public static string SpaceToChar(string Text, string Character = "-") => "@sc" + Character + "," + Text;
        public static string EncodeURI(string Text) => "@ue" + Text;
        public static string DecodeURI(string Text) => "@ud" + Text;

        public static string Method(string MethodName, string[] Args = null)
        {
            string ReturnValue = "@cm" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? "," + string.Join(",", Args) : "";

            return ReturnValue;
        }

        public static string ModuleMethod(string MethodName, string[] Args = null)
        {
            string ReturnValue = "@cM" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? "," + string.Join(",", Args) : "";

            return ReturnValue;
        }

        public static string WasmMethod(string WasmLanguage, string WasmUrl, string MethodName, string[] Args = null, string Key = ".")
        {
            string ReturnValue = "@wA" + WasmLanguage + "," + WasmUrl + "," + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? "," + string.Join(",", Args) : "";

            return ReturnValue;
        }

        public static string Script(string ScriptText) => "@_" + ScriptText.Replace('\n'.ToString(), "$[ln];");
        public static string LoadUrl(string Url, bool FetchScript = false) => "@lu" + Url + (FetchScript ? ",1" : "");
        public static string LoadHtml(string Url, string FetchInputPlace, bool FetchScript = false) => "@lh" + Url + "," + (FetchScript ? "1" : "0") + (!string.IsNullOrEmpty(FetchInputPlace) ? "," + FetchInputPlace : "");
        public static string LoadLine(string Url, int Line) => "@ll" + Url + "," + Line;
        public static string LoadINI(string Url, string Name, bool IsINILike = false) => "@li" + Url + "," + Name + (IsINILike? ",1" : "");
        // Name: Name Or Nested Paths. Is Supprt Index (Student[8].Name). Nested Paths Index Starts At 0
        public static string LoadJSON(string Url, string Name) => "@lj" + Url + "," + Name;
        // Name: Name Or XPath; XPath Index Starts At 1
        public static string LoadXML(string Url, string Name) => "@lx" + Url + "," + Name;
        // MethodName: It's Check Function Or Variable
        public static string HasMethod(string MethodName) => "@hm" + MethodName;
        public static string HasModuleMethod(string MethodName) => "@hM" + MethodName;
        // This Method Return True Or False If Key Pressed
        // Modifier: Alt, AltGraph, Control, Meta, Shift, CapsLock, NumLock, ScrollLock
        public static string GetModifierState(string Modifier) => "@ms" + Modifier;

        // Math
        public static string Math(string MethodName, string[] Args = null)
        {
            string ReturnValue = "@M#" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? "," + string.Join(",", Args) : "";

            return ReturnValue;
        }

        // Data
        public const string DateYear = "@dy";
        // Month In JavaScript Is Start From Index 0, Month In WebForms Core Is Start From Index 1 
        public const string DateMonth = "@dm";
        public const string DateDay = "@dd";
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
        public static string GetAttribute(string InputPlace, string Attribute) => "@$a" + InputPlace + "," + Attribute;
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
        public static string Session(string Key) => "@cs" + Key;
        public static string Session(string Key, string ReplaceValue) => "@cs" + Key + "," + ReplaceValue;
        public static string SessionAndRemove(string Key) => "@cl" + Key;
        public static string Saved(string Key = ".") => Session(Key);
        public static string Cache(string Key = ".") => "@cd" + Key;
        public static string Cache(string Key, string ReplaceValue) => "@cd" + Key + "," + ReplaceValue;
        public static string CacheAndRemove(string Key) => "@ct" + Key;
        public static string SavedLine(string Key = ".", int Line = 0) => "@lL" + Key + "[" + Line;
        public static string SavedLineConsume(string Key = ".") => "@lL" + Key;
        public static string SavedINI(string Key, string INIKey) => "@lI" + Key + "[" + INIKey;
        public static string CacheLine(string Key = ".", int Line = 0) => "@dL" + Key + "[" + Line;
        public static string CacheLineConsume(string Key = ".") => "@dL" + Key;
        public static string CacheINI(string Key, string INIKey) => "@dI" + Key + "[" + INIKey;

        // Format Storage
        public static string FormatStore(string Key) => "@fr" + Key;
        public static string FormatStoreByXMLQuery(string Key, string XPath) => "@fx" + Key + "," + XPath;
        public static string FormatStoreByJSONQuery(string Key, string Query) => "@fj" + Key + "," + Query;
        public static string FormatStoreByINI(string Key, string Name) => "@fi" + Key + "," + Name;
        public static string FormatStoreByText(string Key, int Line) => "@ft" + Key + "," + Line;
        public static string FormatStoreByVariable(string Key) => "@fv" + Key;

        // Document
        public const string TabIsActive = "@da";

        // Window
        public const string GetSelection = "@ws";
        public const string ScrollX = "@wx";
        public const string ScrollY = "@wy";

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
        public const string C = "c";
        public const string CPP = "c";
        public const string Rust = "rust";
        public const string CSharp = "csharp";
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
        /// <summary>
        /// This Method Does Not Support QueryAll
        /// </summary>
        public static string AppendPlace(this string Text, string Value)
        {
            if (Text.Length < 1)
                return Value;

            return Text + "|" + Value;
        }

        public static string AppendParrent(this string Text)
        {
            return "/" + Text;
        }

        public static string ExportActionControlsToWebFormsTag(this string ActionControls, bool AddLine = false)
        {
            return (AddLine? "\n" : "") + "<web-forms ac=\"" + ActionControls + "\"></web-forms>";
        }

        public static string ExportActionControlsToHtmlComment(this string ActionControls, bool AddLine = false)
        {
            return (AddLine ? "\n" : "") + "<!--[web-forms]\n" + ActionControls + "-->";
        }

        public static string ExportActionControlsToResponse(this string ActionControls)
        {
            return "[web-forms]\n" + ActionControls;
        }

        public static string RemoveOuter(this string Text, string StartString, string EndString)
        {
            int Start = Text.IndexOf(StartString);
            if (Start == -1)
                return Text;

            int End = Text.IndexOf(EndString, Start);
            if (End == -1)
                return Text;

            int lengthToRemove = (End - Start) + EndString.Length;

            return Text.Remove(Start, lengthToRemove);
        }

        public static string LineBreak(this string Text)
        {
            return Text.Replace("\n", "$[sln]");
        }
    }
}
