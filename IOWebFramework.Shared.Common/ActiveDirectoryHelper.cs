using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Utilclass;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace IOWebFramework.Shared.Common
{
    public static class ActiveDirectoryHelper
    {
        //TODO SS extract constants in DB config or in App config
        private static string _ldapHost = "is-bg.net";
        private static string _ldapDomain = "is-bg";
        private static string _stringAttributes = "SAMAccountName,name,givenName,sn,title,department,l,mail,telephoneNumber,mobile,streetAddress,thumbnailPhoto";
        private static string _binaryAttributes = "thumbnailPhoto";

        /// <summary>
        /// Задава стойности на параметрите на хелпъра
        /// </summary>
        /// <param name="ldapHost">Хост на АД</param>
        /// <param name="ldapDomain">Домейн на АД</param>
        /// <param name="stringAttributes">Атрибути на текстоови данни от АД</param>
        /// <param name="binaryAttributes">Атрибути на бинарни данни от АД</param>
        public static void Initialize(string ldapHost, string ldapDomain, string stringAttributes, string binaryAttributes)
        {
            _ldapHost = ldapHost;
            _ldapDomain = ldapDomain;
            _stringAttributes = stringAttributes;
            _binaryAttributes = binaryAttributes;
        }

        public static bool LdapLogin(string username, string password)
        {
            CheckIfInitialised();
            bool success = false;

            int ldapPort = LdapConnection.DefaultPort;
            LdapConnection conn = new LdapConnection();

            try
            {
                // connect to the server
                conn.Connect(_ldapHost, ldapPort);

                // authenticate to the server
                conn.Bind(String.Format("{0}\\{1}", _ldapDomain, username), password);

                success = true;
                // disconnect with the server
                conn.Disconnect();
            }
            catch (LdapException e)
            {
                if (e.ResultCode == LdapException.InvalidCredentials)
                {
                    success = false;
                }
                else if (e.ResultCode == LdapException.ConnectError)
                {
                    throw new TimeoutException("Can't connect to AD", e);
                }
                else
                {
                    //ErrorHelper.WriteToLog("LdapLogin", e);
                }
            }

            return success;
        }

        public static Dictionary<string, string> GetUserInfo(string username, string password)
        {
            CheckIfInitialised();
            int ldapPort = LdapConnection.DefaultPort;
            string[] ldapAttributes = _stringAttributes != null ?
                _stringAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            string[] ldapBinaryAttributes = _binaryAttributes != null ?
                _binaryAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { };
            string ldapBase = String.Empty;
            string filter = string.Format("(&(SAMAccountName={0}))", username);
            LdapConnection conn = new LdapConnection();
            Dictionary<string, string> userInfo = new Dictionary<string, string>();

            string[] hostParts = _ldapHost.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in hostParts)
            {
                ldapBase += String.Format("dc={0},", part);
            }

            if (!String.IsNullOrEmpty(ldapBase))
            {
                ldapBase = ldapBase.Substring(0, ldapBase.Length - 1);
            }

            try
            {
                // connect to the server
                conn.Connect(_ldapHost, ldapPort);

                // authenticate to the server
                conn.Bind(String.Format("{0}\\{1}", _ldapDomain, username), password);

                var result = conn.Search(ldapBase, LdapConnection.ScopeSub, filter, ldapAttributes, false);
                ParseResult(ldapBinaryAttributes, userInfo, (LdapSearchResults)result);

                // disconnect with the server
                conn.Disconnect();
            }
            catch (LdapException e)
            {
                if (e.ResultCode == LdapException.InvalidCredentials)
                {
                    throw new UnauthorizedAccessException("Invalid credentials", e);
                }
                else if (e.ResultCode == LdapException.ConnectError)
                {
                    throw new TimeoutException("Can't connect to AD", e);
                }
                else
                {
                    //ErrorHelper.WriteToLog("GetUserInfo", e);
                }
            }

            return userInfo;
        }

        public static UserDetailDTO GetUserInfoByEmail(string username, string password, string email)
        {
            CheckIfInitialised();
            int ldapPort = LdapConnection.DefaultPort;
            string[] ldapAttributes = _stringAttributes != null ?
                _stringAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : null;
            string[] ldapBinaryAttributes = _binaryAttributes != null ?
                _binaryAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : new string[] { };
            string ldapBase = String.Empty;
            string filter = string.Format("(&(mail={0}))", email);
            LdapConnection conn = new LdapConnection();
            Dictionary<string, string> userInfo = new Dictionary<string, string>();

            string[] hostParts = _ldapHost.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in hostParts)
            {
                ldapBase += String.Format("dc={0},", part);
            }

            if (!String.IsNullOrEmpty(ldapBase))
            {
                ldapBase = ldapBase.Substring(0, ldapBase.Length - 1);
            }

            try
            {
                // connect to the server
                conn.Connect(_ldapHost, ldapPort);

                // authenticate to the server
                conn.Bind(String.Format("{0}\\{1}", _ldapDomain, username), password);

                var result = conn.Search(ldapBase, LdapConnection.ScopeSub, filter, ldapAttributes, false);
                ParseResult(ldapBinaryAttributes, userInfo, (LdapSearchResults)result);

                // disconnect with the server
                conn.Disconnect();
            }
            catch (LdapException e)
            {
                if (e.ResultCode == LdapException.InvalidCredentials)
                {
                    throw new UnauthorizedAccessException("Invalid credentials", e);
                }
                else if (e.ResultCode == LdapException.ConnectError)
                {
                    throw new TimeoutException("Can't connect to AD", e);
                }
                else
                {
                    //ErrorHelper.WriteToLog("GetUserInfo", e);
                }
            }
            var userDetailEntry = new UserDetailDTO()
            {
                Photo = Convert.FromBase64String((userInfo["thumbnailPhoto"]) ?? string.Empty),
                FullName = userInfo["name"],
                Position = userInfo["title"],
                Branch = userInfo["l"],
                Department = userInfo["department"],
                Email = userInfo["mail"],
                Telephone = userInfo["telephoneNumber"],
                Address = userInfo["streetAddress"],
            };
            return userDetailEntry;
        }

        public static List<UserDetailDTO> ImportAllUserDetails()
        {
            List<UserDetailDTO> userDetaislDTO = new List<UserDetailDTO>();
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                            //TODO SS data is Madness, inconsistent ... ! 
                            if ((string)de.Properties["l"].Value == "София"
                                && (string)de.Properties["department"].Value == "Отдел Microsoft технологии"
                                && (string)de.Properties["title"].Value == "Старши разработчик, софтуер")
                            {

                                var userDetailEntry = new UserDetailDTO()
                                {
                                    Photo = (byte[])de.Properties["thumbnailPhoto"].Value,
                                    FullName = (string)de.Properties["name"].Value,
                                    Position = (string)de.Properties["title"].Value,
                                    Branch = (string)de.Properties["l"].Value,
                                    Department = (string)de.Properties["department"].Value,
                                    Email = (string)de.Properties["mail"].Value,
                                    Telephone = (string)de.Properties["telephoneNumber"].Value,
                                    Address = (string)de.Properties["streetAddress"].Value,
                                };
                                userDetaislDTO.Add(userDetailEntry);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO SS log error
                //Console.WriteLine("Network error occured.");
            }
            return userDetaislDTO;
        }

        public static List<UserDetailDTO> ImportUsersDetailsByEmail(string emailInAD = "*@*is-bg.net", bool enabeledActiveAccountInAD = true)
        {
            //empty if not found employee
            var resultDTOs = new List<UserDetailDTO>();
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, Environment.UserDomainName))
                {
                    //https://philipm.at/2018/searching_users_in_active_directory.html
                    var principal = new UserPrincipal(context)
                    {
                        //apply filter
                        //SamAccountName = "john*",
                        EmailAddress = emailInAD,
                        Enabled = enabeledActiveAccountInAD
                    };
                    using (var searcher = new PrincipalSearcher(principal))
                    {
                        foreach (var result in searcher.FindAll())
                        {
                            DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                            var userDetailEntry = new UserDetailDTO()
                            {
                                Photo = (byte[])de.Properties["thumbnailPhoto"].Value,
                                FullName = (string)de.Properties["name"].Value,
                                Position = (string)de.Properties["title"].Value,
                                Branch = (string)de.Properties["l"].Value,
                                Department = (string)de.Properties["department"].Value,
                                Email = (string)de.Properties["mail"].Value,
                                Telephone = (string)de.Properties["telephoneNumber"].Value,
                                Address = (string)de.Properties["streetAddress"].Value,
                            };

                            resultDTOs.Add(userDetailEntry);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO SS log error
                //Console.WriteLine("Network error occured.");
            }
            return resultDTOs;
        }

        private static void ParseResult(string[] ldapBinaryAttributes, Dictionary<string, string> userInfo, LdapSearchResults result)
        {
            while (result.HasMore())
            {
                LdapEntry nextEntry = null;
                try
                {
                    nextEntry = result.Next();
                }
                catch (LdapException ex)
                {
                    //TODO SS
                    //ErrorHelper.WriteToLog("GetUserInfo/ParseResult", e);
                    // Exception is thrown, go for next entry
                    continue;
                }
                Console.WriteLine("\n" + nextEntry.Dn);
                LdapAttributeSet attributeSet = nextEntry.GetAttributeSet();
                System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
                while (ienum.MoveNext())
                {
                    LdapAttribute attribute = (LdapAttribute)ienum.Current;
                    string attributeName = attribute.Name;
                    string attributeVal = String.Empty;

                    if (ldapBinaryAttributes.Contains(attributeName))
                    {
                        attributeVal = Base64.Encode(attribute.ByteValue);
                    }
                    else
                    {
                        attributeVal = attribute.StringValue;
                    }

                    userInfo.Add(attributeName, attributeVal);
                }
            }
        }

        private static void CheckIfInitialised()
        {
            if (_ldapDomain == null || _ldapHost == null || _stringAttributes == null)
            {
                throw new InvalidOperationException("Active Directory Helper is not initialised. Please, call Initialise method");
            }
        }
    }
}
