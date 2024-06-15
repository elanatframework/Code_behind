using Microsoft.AspNetCore.Http;
using System.Data;
using System.Xml;

namespace CodeBehind
{
    public class RoleAccess
    {
        private readonly ISession _Session;

        public RoleAccess(ISession Session)
        {
            _Session = Session;

            if (GetUserRole() == null)
                SetUserRole(StaticObject.DefaultRole);
        }

        private void SetUserRole(string RoleName)
        {
            _Session.SetString("code_behind_role_name", RoleName);

            SetRoleSessionAction();
        }

        public void SetUserNewRole(string RoleName)
        {
            ExitRole();

            SetUserRole(RoleName);
        }

        public void ExitRoleToDefault()
        {
            ExitRole();

            SetUserRole(StaticObject.DefaultRole);
        }

        public void ExitRole()
        {
            string RoleName = GetUserRole();

            foreach (Role role in RoleList.Roles)
            {
                if (role.Name == RoleName)
                {
                    foreach (RoleSessionAction action in role.RoleSessionActions)
                        _Session.Remove("code_behind_role_action_" + action.Name);

                    break;
                }
            }
        }

        public string GetUserRole()
        {
            return _Session.GetString("code_behind_role_name");
        }

        private void SetRoleSessionAction()
        {
            string RoleName = GetUserRole();

            foreach (Role role in RoleList.Roles)
            {
                if (role.Name == RoleName)
                {
                    foreach (RoleSessionAction action in role.RoleSessionActions)
                        _Session.SetString("code_behind_role_action_" + action.Name, action.Value);

                    break;
                }
            }
        }

        public string GetSessionAction(string name)
        {
            if (_Session.GetString("code_behind_role_action_" + name) != null)
                return _Session.GetString("code_behind_role_action_" + name);

            return null;
        }

        public void SetSessionAction(string name, string value)
        {
            _Session.SetString("code_behind_role_action_" + name, value);
        }

        // Overload
        public void SetSessionAction(string name, int number)
        {
            SetSessionAction(name, number.ToString());
        }

        public string GetStaticAction(string name)
        {
            string RoleName = GetUserRole();

            foreach (Role role in RoleList.Roles)
                if (role.Name == RoleName)
                    foreach (RoleStaticAction action in role.RoleStaticActions)
                        if (action.Name == name)
                            return action.Value;

            return null;
        }

        public bool HasAccess(HttpRequest request)
        {
            string RoleName = GetUserRole();

            if (string.IsNullOrEmpty(RoleName))
                return false;

            string Path = request.Path;
            string QueryString = request.QueryString.ToString();
            string FormData = "";

            try
            {
                FormData = request.Form.ToString();
            }
            catch (Exception)
            {
            }

            foreach (Role role in RoleList.Roles)
            {
                if (role.Name == RoleName)
                {
                    foreach (Roleِeny deny in role.RoleDenials)
                    {
                        if (!string.IsNullOrEmpty(deny.Path))
                            if (!Path.HasMatching(deny.PathMatchType, deny.Path))
                                continue;

                        if (!string.IsNullOrEmpty(deny.Query))
                            if (!QueryString.HasMatching(deny.QueryMatchType, deny.Query))
                                continue;

                        if (!string.IsNullOrEmpty(deny.FormData))
                            if (!FormData.HasMatching(deny.FormDataMatchType, deny.FormData))
                                continue;

                        return false;
                    }

                    break;
                }
            }

            return true;
        }
    }

    public class FillRoleList
    {
        public void Set()
        {
            RoleList.Roles = new List<Role>();

            XmlDocument doc = new XmlDocument();
            doc.Load("code_behind/role.xml");

            XmlNodeList NodeList = doc.SelectSingleNode("role_list").ChildNodes;

            foreach (XmlNode node in NodeList)
            {
                bool RoleIsActive = node.Attributes["active"] == null;

                if (!RoleIsActive)
                    RoleIsActive = node.Attributes["active"].Value == "true";

                if (RoleIsActive)
                {
                    Role role = new Role();
                    role.Name = node.Attributes["name"].Value;


                    XmlNodeList RoleDenyNode = node.SelectNodes("deny");

                    foreach (XmlNode DenyNode in RoleDenyNode)
                    {
                        bool DenyIsActive = DenyNode.Attributes["active"] == null;

                        if (!DenyIsActive)
                            DenyIsActive = DenyNode.Attributes["active"].Value == "true";

                        if (DenyIsActive)
                        {
                            Roleِeny deny = new Roleِeny();

                            if (DenyNode.SelectSingleNode("path") != null)
                            {
                                deny.Path = DenyNode.SelectNodes("path")[0].InnerText;
                                deny.PathMatchType = DenyNode.SelectNodes("path")[0].Attributes["match_type"].Value;
                            }

                            if (DenyNode.SelectSingleNode("query") != null)
                            {
                                deny.Query = DenyNode.SelectNodes("query")[0].InnerText;
                                deny.QueryMatchType = DenyNode.SelectNodes("query")[0].Attributes["match_type"].Value;
                            }

                            if (DenyNode.SelectSingleNode("form") != null)
                            {
                                deny.FormData = DenyNode.SelectNodes("form")[0].InnerText;
                                deny.FormDataMatchType = DenyNode.SelectNodes("form")[0].Attributes["match_type"].Value;
                            }

                            role.RoleDenials.Add(deny);
                        }
                    }


                    XmlNodeList RoleStaticActionNode = node.SelectNodes("action");

                    foreach (XmlNode ActionNode in RoleStaticActionNode)
                    {
                        bool ActionIsActive = ActionNode.Attributes["active"] == null;

                        if (!ActionIsActive)
                            ActionIsActive = ActionNode.Attributes["active"].Value == "true";

                        if (ActionIsActive)
                        {
                            if (ActionNode.Attributes["type"].Value == "static")
                            {
                                RoleStaticAction TmpRoleStaticAction = new RoleStaticAction();
                                TmpRoleStaticAction.Name = ActionNode.Attributes["name"].Value;
                                TmpRoleStaticAction.Value = ActionNode.Attributes["value"].Value;

                                role.RoleStaticActions.Add(TmpRoleStaticAction);
                            }
                            else if (ActionNode.Attributes["type"].Value == "session")
                            {
                                RoleSessionAction TmpRoleSessionAction = new RoleSessionAction();
                                TmpRoleSessionAction.Name = ActionNode.Attributes["name"].Value;
                                TmpRoleSessionAction.Value = ActionNode.Attributes["value"].Value;

                                role.RoleSessionActions.Add(TmpRoleSessionAction);
                            }
                        }
                    }

                    RoleList.Roles.Add(role);
                }
            }
        }
    }

    public static class RoleList
    {
        public static List<Role> Roles = new List<Role>();
    }

    public class Role
    {
        public string Name { get; set; }
        public List<Roleِeny> RoleDenials = new List<Roleِeny>();
        public List<RoleStaticAction> RoleStaticActions = new List<RoleStaticAction>();
        public List<RoleSessionAction> RoleSessionActions = new List<RoleSessionAction>();
    }

    public class Roleِeny
    {
        public string Path { get; set; }
        public string Query { get; set; }
        public string FormData { get; set; }

        // Accept Values: regex, exist, start, end, full_match
        public string PathMatchType { get; set; }
        public string QueryMatchType { get; set; }
        public string FormDataMatchType { get; set; }
    }

    public class RoleStaticAction
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class RoleSessionAction
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
