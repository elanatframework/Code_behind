using Microsoft.AspNetCore.Http;
using System.Text;

namespace CodeBehind
{
    public class WebForms
    {
        private NameAndValueCollection WebFormsData = new NameAndValueCollection();

        // For Extension
        public void AddLine(string Name, string Value) => WebFormsData.Add(Name, Value);

        // Add
        public void AddId(string InputPlace, string Id) => WebFormsData.Add("ai" + InputPlace, Id);
        public void AddName(string InputPlace, string Name) => WebFormsData.Add("an" + InputPlace, Name);
        public void AddValue(string InputPlace, string Value) => WebFormsData.Add("av" + InputPlace, Value);
        public void AddClass(string InputPlace, string Class) => WebFormsData.Add("ac" + InputPlace, Class);
        public void AddStyle(string InputPlace, string Style) => WebFormsData.Add("as" + InputPlace, Style);
        public void AddStyle(string InputPlace, string Name, string Value) => WebFormsData.Add("as" + InputPlace, Name + ':' + Value);
        public void AddOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => WebFormsData.Add("ao" + InputPlace, Value + '|' + Text + (Selected ? "|1" : "")); public void AddCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => WebFormsData.Add("ak" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void AddTitle(string InputPlace, string Title) => WebFormsData.Add("al" + InputPlace, Title);
        public void AddText(string InputPlace, string Text) => WebFormsData.Add("at" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddTextToUp(string InputPlace, string Text) => WebFormsData.Add("pt" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void AddAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => WebFormsData.Add("aa" + InputPlace, Attribute + '|' + ((Splitter != '\0') ? Splitter : "") + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));
        public void AddTag(string InputPlace, string TagName, string Id = "") => WebFormsData.Add("nt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagToUp(string InputPlace, string TagName, string Id = "") => WebFormsData.Add("ut" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagBefore(string InputPlace, string TagName, string Id = "") => WebFormsData.Add("bt" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddTagAfter(string InputPlace, string TagName, string Id = "") => WebFormsData.Add("ft" + InputPlace, TagName + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));
        public void AddHidden(string InputPlace, string Value, string Id = "") => WebFormsData.Add("ah" + InputPlace, Value + (!string.IsNullOrEmpty(Id) ? '|' + Id : ""));

        // Set
        public void SetId(string InputPlace, string Id) => WebFormsData.Add("si" + InputPlace, Id);
        public void SetName(string InputPlace, string Name) => WebFormsData.Add("sn" + InputPlace, Name);
        public void SetValue(string InputPlace, string Value) => WebFormsData.Add("sv" + InputPlace, Value);
        public void SetClass(string InputPlace, string Class) => WebFormsData.Add("sc" + InputPlace, Class);
        public void SetStyle(string InputPlace, string Style) => WebFormsData.Add("ss" + InputPlace, Style);
        public void SetStyle(string InputPlace, string Name, string Value) => WebFormsData.Add("ss" + InputPlace, Name + ':' + Value);
        public void SetOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => WebFormsData.Add("so" + InputPlace, Value + '|' + Text + (Selected ? "|1" : ""));
        public void SetChecked(string InputPlace, bool Checked = false) => WebFormsData.Add("sk" + InputPlace, Checked ? "1" : "0");
        public void SetCheckBoxTagToList(string InputPlace, string Text, string Value, bool Checked = false) => WebFormsData.Add("sk" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void SetTitle(string InputPlace, string Title) => WebFormsData.Add("sl" + InputPlace, Title);
        public void SetText(string InputPlace, string Text) => WebFormsData.Add("st" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void SetAttribute(string InputPlace, string Attribute, string Value = "") => WebFormsData.Add("sa" + InputPlace, Attribute + '|' + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));
        public void SetWidth(string InputPlace, string Width) => WebFormsData.Add("sw" + InputPlace, Width);
        public void SetWidth(string InputPlace, int Width) => SetWidth(InputPlace, Width.ToString() + "px");
        public void SetHeight(string InputPlace, string Height) => WebFormsData.Add("sh" + InputPlace, Height);
        public void SetHeight(string InputPlace, int Height) => SetHeight(InputPlace, Height.ToString() + "px");

        // Insert
        public void InsertId(string InputPlace, string Id) => WebFormsData.Add("ii" + InputPlace, Id);
        public void InsertName(string InputPlace, string Name) => WebFormsData.Add("in" + InputPlace, Name);
        public void InsertValue(string InputPlace, string Value) => WebFormsData.Add("iv" + InputPlace, Value);
        public void InsertClass(string InputPlace, string Class) => WebFormsData.Add("ic" + InputPlace, Class);
        public void InsertStyle(string InputPlace, string Style) => WebFormsData.Add("is" + InputPlace, Style);
        public void InsertStyle(string InputPlace, string Name, string Value) => WebFormsData.Add("is" + InputPlace, Name + ':' + Value);
        public void InsertOptionTag(string InputPlace, string Text, string Value, bool Selected = false) => WebFormsData.Add("io" + InputPlace, Value + '|' + Text + (Selected ? "|1" : ""));
        public void InsertCheckBoxTag(string InputPlace, string Text, string Value, bool Checked = false) => WebFormsData.Add("ik" + InputPlace, Value + '|' + Text + (Checked ? "|1" : ""));
        public void InsertTitle(string InputPlace, string Title) => WebFormsData.Add("il" + InputPlace, Title);
        public void InsertText(string InputPlace, string Text) => WebFormsData.Add("it" + InputPlace, Text.Replace('\n'.ToString(), "$[ln];"));
        public void InsertAttribute(string InputPlace, string Attribute, string Value = "", char Splitter = '\0') => WebFormsData.Add("ia" + InputPlace, Attribute + '|' + ((Splitter != '\0') ? Splitter : "") + (!string.IsNullOrEmpty(Value) ? '|' + Value : ""));

        // Delete
        public void DeleteId(string InputPlace) => WebFormsData.Add("di" + InputPlace, "1");
        public void DeleteName(string InputPlace) => WebFormsData.Add("dn" + InputPlace, "1");
        public void DeleteValue(string InputPlace) => WebFormsData.Add("dv" + InputPlace, "1");
        public void DeleteClass(string InputPlace, string ClassName) => WebFormsData.Add("dc" + InputPlace, ClassName);
        public void DeleteStyle(string InputPlace, string StyleName) => WebFormsData.Add("ds" + InputPlace, StyleName);
        public void DeleteOptionTag(string InputPlace, string Value) => WebFormsData.Add("do" + InputPlace, Value);
        public void DeleteAllOptionTag(string InputPlace) => WebFormsData.Add("do" + InputPlace, "*");
        public void DeleteCheckBoxTag(string InputPlace, string Value) => WebFormsData.Add("dk" + InputPlace, Value);
        public void DeleteAllCheckBoxTag(string InputPlace) => WebFormsData.Add("dk" + InputPlace, "*");
        public void DeleteTitle(string InputPlace) => WebFormsData.Add("dl" + InputPlace, "1");
        public void DeleteText(string InputPlace) => WebFormsData.Add("dt" + InputPlace, "1");
        public void DeleteAttribute(string InputPlace, string Attribute) => WebFormsData.Add("da" + InputPlace, Attribute);
        public void Delete(string InputPlace) => WebFormsData.Add("de" + InputPlace, "1");
        public void DeleteParent(string InputPlace) => WebFormsData.Add("dp" + InputPlace, "1");

        // Other
        public void SetBackgroundColor(string InputPlace, string Color) => WebFormsData.Add("bc" + InputPlace, Color);
        public void SetTextColor(string InputPlace, string Color) => WebFormsData.Add("tc" + InputPlace, Color);
        public void SetFontName(string InputPlace, string Name) => WebFormsData.Add("fn" + InputPlace, Name);
        public void SetFontSize(string InputPlace, string Size) => WebFormsData.Add("fs" + InputPlace, Size);
        public void SetFontSize(string InputPlace, int Size) => WebFormsData.Add("fs" + InputPlace, Size + "px");
        public void SetFontBold(string InputPlace, bool Bold) => WebFormsData.Add("fb" + InputPlace, Bold ? "1" : "0");
        public void SetVisible(string InputPlace, bool Visible) => WebFormsData.Add("vi" + InputPlace, Visible ? "1" : "0");
        public void SetTextAlign(string InputPlace, string Align) => WebFormsData.Add("ta" + InputPlace, Align);
        public void SetReadOnly(string InputPlace, bool ReadOnly) => WebFormsData.Add("sr" + InputPlace, ReadOnly ? "1" : "0");
        public void SetDisabled(string InputPlace, bool Disabled) => WebFormsData.Add("sd" + InputPlace, Disabled ? "1" : "0");
        public void SetFocus(string InputPlace, bool Focus) => WebFormsData.Add("sf" + InputPlace, Focus ? "1" : "0");
        public void SetMinLength(string InputPlace, int Length) => WebFormsData.Add("mn" + InputPlace, Length.ToString());
        public void SetMaxLength(string InputPlace, int Length) => WebFormsData.Add("mx" + InputPlace, Length.ToString());
        public void SetSelectedValue(string InputPlace, string Value) => WebFormsData.Add("ts" + InputPlace, Value);
        public void SetSelectedIndex(string InputPlace, int Index) => WebFormsData.Add("ti" + InputPlace, Index.ToString());
        public void SetCheckedValue(string InputPlace, string Value, bool Selected) => WebFormsData.Add("ks" + InputPlace, Value + "|" + (Selected ? "1" : "0"));
        public void SetCheckedIndex(string InputPlace, int Index, bool Selected) => WebFormsData.Add("ki" + InputPlace, Index.ToString() + "|" + (Selected ? "1" : "0"));
        public void CallScript(string ScriptText) => WebFormsData.Add("_", ScriptText.Replace('\n'.ToString(), "$[ln];"));
        public void LoadUrl(string InputPlace, string Url, bool FetchScript = false) => WebFormsData.Add("lu" + InputPlace, Url + (FetchScript? "|1" : ""));
        public void ChangeUrl(string Url) => WebFormsData.Add("cu", Url);
        public void SetHeadTitle(string Title) => WebFormsData.Add("ht", Title);
        public void RemoveSessionCache(string CacheKey) => WebFormsData.Add("rs", CacheKey);
        public void RemoveAllSessionCache() => WebFormsData.Add("rs", "*");
        public void RemoveCache(string CacheKey) => WebFormsData.Add("rd", CacheKey);
        public void RemoveAllCache() => WebFormsData.Add("rd", "*");
        public void SetSessionCache() => WebFormsData.Add("cs", "1");
        public void SetCache(int Second) => WebFormsData.Add("cd", Second.ToString());
        public void SetCache() => WebFormsData.Add("cd", "*");
        public void SwapTag(string InputPlace, string OutputPlace) => WebFormsData.Add("sp" + InputPlace, OutputPlace);

        // Increase
        public void IncreaseMinLength(string InputPlace, int Value) => WebFormsData.Add("+n" + InputPlace, Value.ToString());
        public void IncreaseMaxLength(string InputPlace, int Value) => WebFormsData.Add("+x" + InputPlace, Value.ToString());
        public void IncreaseFontSize(string InputPlace, int Value) => WebFormsData.Add("+f" + InputPlace, Value.ToString());
        public void IncreaseWidth(string InputPlace, int Value) => WebFormsData.Add("+w" + InputPlace, Value.ToString());
        public void IncreaseHeight(string InputPlace, int Value) => WebFormsData.Add("+h" + InputPlace, Value.ToString());
        public void IncreaseValue(string InputPlace, int Value) => WebFormsData.Add("+v" + InputPlace, Value.ToString());

        // Decrease
        public void DecreaseMinLength(string InputPlace, int Value) => WebFormsData.Add("-n" + InputPlace, Value.ToString());
        public void DecreaseMaxLength(string InputPlace, int Value) => WebFormsData.Add("-x" + InputPlace, Value.ToString());
        public void DecreaseFontSize(string InputPlace, int Value) => WebFormsData.Add("-f" + InputPlace, Value.ToString());
        public void DecreaseWidth(string InputPlace, int Value) => WebFormsData.Add("-w" + InputPlace, Value.ToString());
        public void DecreaseHeight(string InputPlace, int Value) => WebFormsData.Add("-h" + InputPlace, Value.ToString());
        public void DecreaseValue(string InputPlace, int Value) => WebFormsData.Add("-v" + InputPlace, Value.ToString());

        // Event
        public void SetPostEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ep" + InputPlace, HtmlEvent);
        public void SetPostEventAdding(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ep" + InputPlace, HtmlEvent + "|+");
        public void SetPostEventTo(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Ep" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetPostEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EP" + InputPlace, HtmlEventListener);
        public void SetPostEventListenerAdding(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EP" + InputPlace, HtmlEventListener + "|+");
        public void SetPostEventListenerTo(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EP" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetGetEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Eg" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Eg" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetGetEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Eg" + InputPlace, HtmlEvent);
        public void SetGetEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Eg" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EG" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetGetEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EG" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetGetEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EG" + InputPlace, HtmlEventListener);
        public void SetGetEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EG" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetPutEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Eu" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPutEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Eu" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPutEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Eu" + InputPlace, HtmlEvent);
        public void SetPutEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Eu" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetPutEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EU" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPutEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EU" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPutEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EU" + InputPlace, HtmlEventListener);
        public void SetPutEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EU" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Ea" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Ea" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPatchEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ea" + InputPlace, HtmlEvent);
        public void SetPatchEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Ea" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EA" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetPatchEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EA" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetPatchEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EA" + InputPlace, HtmlEventListener);
        public void SetPatchEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EA" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("El" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("El" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetDeleteEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("El" + InputPlace, HtmlEvent);
        public void SetDeleteEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("El" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EL" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetDeleteEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EL" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetDeleteEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EL" + InputPlace, HtmlEventListener);
        public void SetDeleteEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EL" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetHeadEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Eh" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetHeadEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Eh" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetHeadEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Eh" + InputPlace, HtmlEvent);
        public void SetHeadEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Eh" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetHeadEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EH" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetHeadEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EH" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetHeadEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EH" + InputPlace, HtmlEventListener);
        public void SetHeadEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EH" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Eo" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Eo" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetOptionsEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Eo" + InputPlace, HtmlEvent);
        public void SetOptionsEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Eo" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EO" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetOptionsEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EO" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetOptionsEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EO" + InputPlace, HtmlEventListener);
        public void SetOptionsEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EO" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetTraceEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Er" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetTraceEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Er" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetTraceEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Er" + InputPlace, HtmlEvent);
        public void SetTraceEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Er" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetTraceEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("ER" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetTraceEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("ER" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetTraceEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("ER" + InputPlace, HtmlEventListener);
        public void SetTraceEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("ER" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetConnectEvent(string InputPlace, string HtmlEvent, string Path = null) => WebFormsData.Add("Ec" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetConnectEvent(string InputPlace, string HtmlEvent, string OutputPlace, string Path = null) => WebFormsData.Add("Ec" + InputPlace, HtmlEvent + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetConnectEventInForm(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ec" + InputPlace, HtmlEvent);
        public void SetConnectEventInForm(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Ec" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetConnectEventListener(string InputPlace, string HtmlEventListener, string Path = null) => WebFormsData.Add("EC" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#"));
        public void SetConnectEventListener(string InputPlace, string HtmlEventListener, string OutputPlace, string Path = null) => WebFormsData.Add("EC" + InputPlace, HtmlEventListener + "|" + (!string.IsNullOrEmpty(Path) ? Path : "#") + "|" + OutputPlace);
        public void SetConnectEventInFormListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("EC" + InputPlace, HtmlEventListener);
        public void SetConnectEventInFormListener(string InputPlace, string HtmlEventListener, string OutputPlace) => WebFormsData.Add("EC" + InputPlace, HtmlEventListener + "|" + OutputPlace);
        public void SetTagEvent(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("Et" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetTagEventListener(string InputPlace, string HtmlEvent, string OutputPlace) => WebFormsData.Add("ET" + InputPlace, HtmlEvent + "|" + OutputPlace);
        public void SetWebSocketEvent(string InputPlace, string HtmlEvent, string Path) => WebFormsData.Add("Ew" + InputPlace, HtmlEvent + "|" + Path);
        public void SetWebSocketEventListener(string InputPlace, string HtmlEvent, string Path) => WebFormsData.Add("EW" + InputPlace, HtmlEvent + "|" + Path);
        public void SetPreventDefaultEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ed" + InputPlace, HtmlEvent);
        public void SetPreventDefaultEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("ED" + InputPlace, HtmlEventListener);
        public void SetStopPropagationEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Es" + InputPlace, HtmlEvent);
        public void SetStopPropagationEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("ES" + InputPlace, HtmlEventListener);
        public void AssignConfirmEvent(string InputPlace, string HtmlEvent, string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel") => WebFormsData.Add("Em" + InputPlace, HtmlEvent + "|" + (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none"? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" :  OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void RemovePostEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rp" + InputPlace, HtmlEvent);
        public void RemovePostEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RP" + InputPlace, HtmlEventListener);
        public void RemoveGetEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rg" + InputPlace, HtmlEvent);
        public void RemoveGetEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RG" + InputPlace, HtmlEventListener);
        public void RemovePutEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ru" + InputPlace, HtmlEvent);
        public void RemovePutEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RU" + InputPlace, HtmlEventListener);
        public void RemovePatchEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ra" + InputPlace, HtmlEvent);
        public void RemovePatchEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RA" + InputPlace, HtmlEventListener);
        public void RemoveDeleteEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rl" + InputPlace, HtmlEvent);
        public void RemoveDeleteEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RL" + InputPlace, HtmlEventListener);
        public void RemoveHeadEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rh" + InputPlace, HtmlEvent);
        public void RemoveHeadEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RH" + InputPlace, HtmlEventListener);
        public void RemoveOptionsEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Ro" + InputPlace, HtmlEvent);
        public void RemoveOptionsEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RO" + InputPlace, HtmlEventListener);
        public void RemoveTraceEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rr" + InputPlace, HtmlEvent);
        public void RemoveTraceEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RR" + InputPlace, HtmlEventListener);
        public void RemoveConnectEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rc" + InputPlace, HtmlEvent);
        public void RemoveConnectEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RC" + InputPlace, HtmlEventListener);
        public void RemoveTagEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rt" + InputPlace, HtmlEvent);
        public void RemoveTagEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RT" + InputPlace, HtmlEventListener);
        public void RemoveWebSocketEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rw" + InputPlace, HtmlEvent);
        public void RemoveWebSocketEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RW" + InputPlace, HtmlEventListener);
        public void RemovePreventDefaultEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rd" + InputPlace, HtmlEvent);
        public void RemovePreventDefaultEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RD" + InputPlace, HtmlEventListener);
        public void RemoveStopPropagationEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rs" + InputPlace, HtmlEvent);
        public void RemoveStopPropagationEventListener(string InputPlace, string HtmlEventListener) => WebFormsData.Add("RS" + InputPlace, HtmlEventListener);
        public void RemoveConfirmEvent(string InputPlace, string HtmlEvent) => WebFormsData.Add("Rm" + InputPlace, HtmlEvent);

        // Save
        public void SaveId(string InputPlace, string Key = ".") => WebFormsData.Add("@gi" + InputPlace, Key);
        public void SaveName(string InputPlace, string Key = ".") => WebFormsData.Add("@gn" + InputPlace, Key);
        public void SaveValue(string InputPlace, string Key = ".") => WebFormsData.Add("@gv" + InputPlace, Key);
        public void SaveValueLength(string InputPlace, string Key = ".") => WebFormsData.Add("@ge" + InputPlace, Key);
        public void SaveClass(string InputPlace, string Key = ".") => WebFormsData.Add("@gc" + InputPlace, Key);
        public void SaveStyle(string InputPlace, string Key = ".") => WebFormsData.Add("@gs" + InputPlace, Key);
        public void SaveTitle(string InputPlace, string Key = ".") => WebFormsData.Add("@gl" + InputPlace, Key);
        public void SaveText(string InputPlace, string Key = ".") => WebFormsData.Add("@gt" + InputPlace, Key);
        public void SaveOuterText(string InputPlace, string Key = ".") => WebFormsData.Add("@go" + InputPlace, Key);
        public void SaveTextLength(string InputPlace, string Key = ".") => WebFormsData.Add("@gg" + InputPlace, Key);
        public void SaveAttribute(string InputPlace, string Attribute, string Key = ".") => WebFormsData.Add("@ga" + InputPlace, Key + '|' + Attribute);
        public void SaveWidth(string InputPlace, string Key = ".") => WebFormsData.Add("@gw" + InputPlace, Key);
        public void SaveHeight(string InputPlace, string Key = ".") => WebFormsData.Add("@gh" + InputPlace, Key);
        public void SaveReadOnly(string InputPlace, string Key = ".") => WebFormsData.Add("@gr" + InputPlace, Key);
        public void SaveSelectedIndex(string InputPlace, string Key = ".") => WebFormsData.Add("@gx" + InputPlace, Key);
        public void SaveTextAlign(string InputPlace, string Key = ".") => WebFormsData.Add("@ta" + InputPlace, Key);
        public void SaveNodeLength(string InputPlace, string Key = ".") => WebFormsData.Add("@nl" + InputPlace, Key);
        public void SaveVisible(string InputPlace, string Key = ".") => WebFormsData.Add("@vi" + InputPlace, Key);
        public void SaveUrl(string Url, bool FetchScript = false, string Key = ".") => WebFormsData.Add("@gu", Key + "|" + Url + (FetchScript ? "|1" : ""));
        public void SaveIndex(string InputPlace, string Key = ".") => WebFormsData.Add("@gI" + InputPlace, Key);
        public void SaveWasmMethodResult(string WasmLanguage, string WasmUrl, string MethodName, string[] Args, string Key = ".") => WebFormsData.Add("@gA", Key + "|" + WasmLanguage + "," + WasmUrl + "," + MethodName + ((Args.Length > 0) ? "," + string.Join(",", Args) : ""));

        // Cache
        public void CacheId(string InputPlace, string Key = ".") => WebFormsData.Add("@ci" + InputPlace, Key);
        public void CacheName(string InputPlace, string Key = ".") => WebFormsData.Add("@cn" + InputPlace, Key);
        public void CacheValue(string InputPlace, string Key = ".") => WebFormsData.Add("@cv" + InputPlace, Key);
        public void CacheValueLength(string InputPlace, string Key = ".") => WebFormsData.Add("@ce" + InputPlace, Key);
        public void CacheClass(string InputPlace, string Key = ".") => WebFormsData.Add("@cc" + InputPlace, Key);
        public void CacheStyle(string InputPlace, string Key = ".") => WebFormsData.Add("@cs" + InputPlace, Key);
        public void CacheTitle(string InputPlace, string Key = ".") => WebFormsData.Add("@cl" + InputPlace, Key);
        public void CacheText(string InputPlace, string Key = ".") => WebFormsData.Add("@ct" + InputPlace, Key);
        public void CacheOuterText(string InputPlace, string Key = ".") => WebFormsData.Add("@co" + InputPlace, Key);
        public void CacheTextLength(string InputPlace, string Key = ".") => WebFormsData.Add("@cg" + InputPlace, Key);
        public void CacheAttribute(string InputPlace, string Attribute, string Key = ".") => WebFormsData.Add("@ca" + InputPlace, Key + '|' + Attribute);
        public void CacheWidth(string InputPlace, string Key = ".") => WebFormsData.Add("@cw" + InputPlace, Key);
        public void CacheHeight(string InputPlace, string Key = ".") => WebFormsData.Add("@ch" + InputPlace, Key);
        public void CacheReadOnly(string InputPlace, string Key = ".") => WebFormsData.Add("@cr" + InputPlace, Key);
        public void CacheSelectedIndex(string InputPlace, string Key = ".") => WebFormsData.Add("@cx" + InputPlace, Key);
        public void CacheTextAlign(string InputPlace, string Key = ".") => WebFormsData.Add("@Ta" + InputPlace, Key);
        public void CacheNodeLength(string InputPlace, string Key = ".") => WebFormsData.Add("@Nl" + InputPlace, Key);
        public void CacheVisible(string InputPlace, string Key = ".") => WebFormsData.Add("@Vi" + InputPlace, Key);
        public void CacheUrl(string Url, bool FetchScript = false, string Key = ".") => WebFormsData.Add("@cu", Key + "|" + Url + (FetchScript ? "|1" : ""));
        public void CacheIndex(string InputPlace, string Key = ".") => WebFormsData.Add("@cI" + InputPlace, Key);
        public void CacheWasmMethodResult(string WasmLanguage, string WasmUrl, string MethodName, string[] Args, string Key = ".") => WebFormsData.Add("@cA", Key + "|" + WasmLanguage + "," + WasmUrl + "," + MethodName + ((Args.Length > 0) ? "," + string.Join(",", Args) : ""));

        // Pre Runner
        public void AssignDelay(float Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            WebFormsData.ChangeNameByIndex(Index, ":" + Second + ")" + CurrentName);
        }

        public void AssignDelayChange(float Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            CurrentName = CurrentName.RemoveOuter(":", ")");

            WebFormsData.ChangeNameByIndex(Index, ":" + Second + ")" + CurrentName);
        }

        public void AssignInterval(float Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            WebFormsData.ChangeNameByIndex(Index, "(" + Second + ")" + CurrentName);
        }

        public void AssignIntervalChange(float Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            CurrentName = CurrentName.RemoveOuter("(", ")");

            WebFormsData.ChangeNameByIndex(Index, "(" + Second + ")" + CurrentName);
        }

        public void AssignRepeat(int Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            WebFormsData.ChangeNameByIndex(Index, "," + Second + ")" + CurrentName);
        }

        public void AssignRepeatChange(int Second, int Index = -1)
        {
            string CurrentName = WebFormsData.GetNameByIndex(Index);

            if (string.IsNullOrEmpty(CurrentName))
                return;

            CurrentName = CurrentName.RemoveOuter(",", ")");

            WebFormsData.ChangeNameByIndex(Index, "," + Second + ")" + CurrentName);
        }

        // Index
        public void StartIndex(string Name) => WebFormsData.Add("#", Name);
        public void StartIndex() => StartIndex("");
        public void GoTo(int Line, int Repeat = 1) => WebFormsData.Add("&", Line + "|" + Repeat.ToString());
        public void GoTo(string Index, int Repeat = 1) => WebFormsData.Add("&", "#" + Index + "|" + Repeat.ToString());

        // Start
        public void StartTransientDOM(string InputPlace) => WebFormsData.Add("td", InputPlace);
        public void EndTransientDOM(string InputPlace) => WebFormsData.Add("td", ";");

        // Message
        // Type: warning, problem, help, success, none
        public void Alert(string Text, string Type = "none", string Title = "Alert", string OkText = "OK") => WebFormsData.Add("al", Text + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Alert" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText));
        public void Message(string Text, string Type = "none", int Duration = 0) => WebFormsData.Add("me", Text + "|" + (Type == "none" ? "" : Type) + "|" + (Duration == 0 ? "" : Duration));

        // Enable
        public void EnableWebSocket(bool Enable = true) => WebFormsData.Add("ew", Enable ? "1" : "0");
        public void EnableWebSocketOnce() => WebFormsData.Add("ew", "@");

        // Use
        public void UseWebSocket(string Path) => WebFormsData.Add("uw", Path);

        // Condition
        // Type: warning, problem, help, success, none
        public void ConfirmIsTrueAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "ct", (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void ConfirmIsFalseAccept(string Text = "Are you sure you want to proceed?", string Type = "none", string Title = "Confirm", string OkText = "OK", string CancelText = "Cancel", float Interval = 100) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "cf", (Text == "Are you sure you want to proceed?" ? "" : Text) + "|" + (Type == "none" ? "" : Type) + "|" + (Title == "Confirm" ? "" : Title) + "|" + (OkText == "OK" ? "" : OkText) + "|" + (CancelText == "Cancel" ? "" : CancelText));
        public void IsGreaterThan(string FirstValue, string SecondValue, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "gt", FirstValue + "|" + SecondValue);
        public void IsLessThan(string FirstValue, string SecondValue, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "lt", FirstValue + "|" + SecondValue);
        public void IsEqualTo(string FirstValue, string SecondValue, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "et", FirstValue + "|" + SecondValue);
        public void IsTrue(string Value, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "tr", Value);
        public void IsFalse(string Value, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "fa", Value);
        public void IsRegexMatch(string Value, string Pattern, float Interval = -1) => WebFormsData.Add(((Interval >= 0) ? "{(" + Interval + ")" : "{") + "re", Value + "|" + Pattern);
        public void Break() => WebFormsData.Add(";", "");
        public void StartBracket() => WebFormsData.Add("{", "");
        public void EndBracket() => WebFormsData.Add("}", "");

        // Get
        public string GetFormsActionData()
        {
            StringBuilder returnValueBuilder = new StringBuilder();

            foreach (NameAndValue nv in WebFormsData.GetList())
            {
                returnValueBuilder.Append(Environment.NewLine);
                returnValueBuilder.Append(nv.Name);

                if (!string.IsNullOrEmpty(nv.Value))
                {
                    returnValueBuilder.Append("=");
                    returnValueBuilder.Append(nv.Value);
                }
            }

            return returnValueBuilder.ToString();
        }

        public string Response()
        {
            return "[web-forms]" + GetFormsActionData();
        }

        // Overload
        public string Response(HttpContext context)
        {
            SetHeaders(context);
            return Response();
        }

        public string GetFormsActionDataLineBreak()
        {
            StringBuilder returnValueBuilder = new StringBuilder();

            List<NameAndValue> WebFormsDataList = WebFormsData.GetList();

            for (int i = 0; i < WebFormsDataList.Count; i++)
            {
                NameAndValue nv = WebFormsDataList[i];

                returnValueBuilder.Append(nv.Name);

                if (!string.IsNullOrEmpty(nv.Value))
                {
                    returnValueBuilder.Append("=");
                    returnValueBuilder.Append(nv.Value.Replace("\"", "$[dq];"));
                }

                if (i < WebFormsDataList.Count - 1)
                {
                    returnValueBuilder.Append("$[sln];");
                }
            }

            return returnValueBuilder.ToString();
        }

        // Export
        public string ExportToWebFormsTag(string src = null)
        {
            return "<web-forms ac=\"" + GetFormsActionDataLineBreak() + "\"" + (!string.IsNullOrEmpty(src) ? " src=\"" + src + "\"" : "") + "></web-forms>";
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

        public NameAndValueCollection ExportToNameValue()
        {
            return WebFormsData;
        }

        public void AppendForm(WebForms form)
        {
            WebFormsData.AddList(form.ExportToNameValue().GetList());
        }

        public void SetHeaders(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "text/plain");
        }

        public void Clean()
        {
            WebFormsData = new NameAndValueCollection();
        }
    }

    public class InputPlace
    {
        public const string Root = "~";
        public const string Current = "$";
        public const string Target = "!";
        public const string Upper = "-";
        public const string Head = "^";

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
        public static string Random(int MaxValue) => "@mr" + MaxValue;
        public static string Random(int MinValue, int MaxValue) => "@mr" + MaxValue + "," + MinValue;
        public const string DateYear = "@dy";
        public const string DateMonth = "@dm";
        public const string DateDay = "@dd";
        public const string DateHours = "@dh";
        public const string Dateinutes = "@di";
        public const string DateSeconds = "@ds";
        public const string DateMilliseconds = "@dl";
        public static string Cookie(string Key) => "@co" + Key;
        public static string Session(string Key) => "@cs" + Key;
        public static string Session(string Key, string ReplaceValue) => "@cs" + Key + "," + ReplaceValue;
        public static string SessionAndRemove(string Key) => "@cl" + Key;
        public static string SessionAndRemove(string Key, string ReplaceValue) => "@cl" + Key + "," + ReplaceValue;
        public static string Saved(string Key = ".") => "@cl" + Key;
        public static string Cache(string Key = ".") => "@cd" + Key;
        public static string Cache(string Key, string ReplaceValue) => "@cd" + Key + "," + ReplaceValue;
        public static string CacheAndRemove(string Key) => "@ct" + Key;
        public static string CacheAndRemove(string Key, string ReplaceValue) => "@ct" + Key + "," + ReplaceValue;
        public static string Script(string ScriptText) => "@_" + ScriptText.Replace('\n'.ToString(), "$[ln];");
        public static string LoadUrl(string Url) => "@lu" + Url;
        public static string SavedLine(string Key = ".", int Line = 0) => "@lL" + Key + "[" + Line;
        public static string SavedLineConsume(string Key = ".") => "@lL" + Key;
        public static string SavedINI(string Key, string INIKey) => "@lI" + Key + "[" + INIKey;
        public static string CacheLine(string Key = ".", int Line = 0) => "@dL" + Key + "[" + Line;
        public static string CacheLineConsume(string Key = ".") => "@dL" + Key;
        public static string CacheINI(string Key, string INIKey) => "@dI" + Key + "[" + INIKey;
        public static string SpaceToChar(string Text, string Character = "-") => "@sc" + Character + "," + Text;
        public static string CallMethod(string MethodName, string[] Args = null)
        {
            string ReturnValue = "@cm" + MethodName;

            if (Args != null)
                ReturnValue += (Args.Length > 0) ? "|" + string.Join(",", Args) : "";

            return ReturnValue;
        }
        public const string EventKey = "@ek";
        public const string EventWhich = "@ew";
        public const string EventClientX = "@ex";
        public const string EventClientY = "@ey";
        public const string EventPageX = "@eX";
        public const string EventPageY = "@eY";
        public const string EventOffsetX = "@Ex";
        public const string EventOffsetY = "@Ey";
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

        public const string AnimationEnd = "animationend";
        public const string AnimationIteration = "animationiteration";
        public const string AnimationStart = "animationstart";
        public const string ContextMenu = "contextmenu";
        public const string FullScreenChange = "fullscreenchange";
        public const string FullScreenError = "fullscreenerror";
        public const string PopState = "popstate";
        public const string TransitionEnd = "transitionend";
        public const string Storage = "storage";
        public const string Wheel = "wheel";
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

        public static string ExportToWebFormsTag(this string src)
        {
            return "<web-forms src=\"" + src + "\"></web-forms>";
        }

        // Overload
        public static string ExportToWebFormsTag(this string src, int Width, int Height)
        {
            return "<web-forms src=\"" + src + "\" width=\"" + Width + "\" height=\"" + Height + "\"></web-forms>";
        }

        public static string ExportActionControlsToWebFormsTag(this string ActionControls)
        {
            return "<web-forms ac=\"" + ActionControls + "\"></web-forms>";
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
    }

    public class NameAndValue
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public NameAndValue()
        {

        }

        public NameAndValue(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
    public class NameAndValueCollection
    {
        private List<NameAndValue> NameValueList = new List<NameAndValue>();

        public void Add(string Name, string Value)
        {
            NameValueList.Add(new NameAndValue(Name, Value));
        }

        public void Set(string Name, string Value)
        {
            if (!Exist(Name))
                Add(Name, Value);
            else
                ChangeValue(Name, Value);
        }

        public void Delete(string Name)
        {
            List<NameAndValue> TmpNameValueList = new List<NameAndValue>();

            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name != Name)
                    TmpNameValueList.Add(nv);
            }

            NameValueList = TmpNameValueList;
        }

        public void DeleteByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : NameValueList.Count + Index;
            NameValueList.RemoveAt(TmpIndex);
        }

        public void Empty()
        {
            NameValueList = new List<NameAndValue>();
        }

        public bool Exist(string Name)
        {
            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name == Name)
                    return true;
            }

            return false;
        }

        public void ChangeValue(string Name, string Value)
        {
            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name == Name)
                {
                    nv.Value = Value;
                    break;
                }
            }
        }

        public void ChangeName(string Name, string NewName)
        {
            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name == Name)
                {
                    nv.Name = NewName;
                    break;
                }
            }
        }

        // Overload
        public void ChangeValue(string Name, string NewName, string Value)
        {
            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name == Name)
                {
                    nv.Name = NewName;
                    nv.Value = Value;
                    break;
                }
            }
        }

        public void ChangeValueByIndex(int Index, string Value)
        {
            if (Index >= 0)
                NameValueList[Index].Value = Value;
            else
                NameValueList[NameValueList.Count + Index].Value = Value;
        }

        public void ChangeNameByIndex(int Index, string Name)
        {
            if (Index >= 0)
                NameValueList[Index].Name = Name;
            else
                NameValueList[NameValueList.Count + Index].Name = Name;
        }

        public void ChangeNameValueByIndex(int Index, string Name, string Value)
        {
            if (Index >= 0)
            {
                NameValueList[Index].Name = Name;
                NameValueList[Index].Value = Value;
            }
            else
            {
                NameValueList[NameValueList.Count + Index].Name = Name;
                NameValueList[NameValueList.Count + Index].Value = Value;
            }
        }

        public void AddList(List<NameAndValue> NameValueList)
        {
            foreach (NameAndValue nv in NameValueList)
                this.NameValueList.Add(nv);
        }

        public string GetValue(string Name)
        {
            foreach (NameAndValue nv in NameValueList)
            {
                if (nv.Name == Name)
                    return nv.Value;
            }

            return "";
        }

        public string GetNameByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : NameValueList.Count + Index;
            return NameValueList[TmpIndex].Name;
        }

        public string GetValueByIndex(int Index)
        {
            int TmpIndex = (Index >= 0) ? Index : NameValueList.Count + Index;
            return NameValueList[TmpIndex].Value; ;
        }

        public List<NameAndValue> GetList()
        {
            return NameValueList;
        }
    }
}
