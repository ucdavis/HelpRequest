﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
//For now, this is just a cut and paste from Catbert. I'm not sure how it is working - Jason
namespace HelpRequest.Controllers.Helpers
{
    public class DirectoryServices
    {
        private const string STR_CN = "cn";
        private const string STR_DisplayName = "displayName";
        private const string STR_EmployeeNumber = "employeeNumber";
        private const string STR_GivenName = "givenName";
        private const string STR_Mail = "mail";
        private const string STR_SearchBase = "ou=People,dc=ucdavis,dc=edu";
        private const string STR_SN = "sn";
        private const string STR_Telephone = "telephoneNumber";
        private const string STR_UID = "uid";
        private const string STR_PIDM = "ucdPersonPIDM";
        private const string STR_StudentId = "ucdStudentSID";
  
        private static readonly string LDAPPassword = ConfigurationManager.AppSettings["LDAPPassword"];
        private static readonly string LDAPUser = ConfigurationManager.AppSettings["LDAPUser"];
        private static readonly int STR_LDAPPort = 636;
        private static readonly string STR_LDAPURL = "ldap.ucdavis.edu";

        public static SearchResponse GetSearchResponse(string searchFilter, string searchBase)
        {
            //Establishing a Connection to the LDAP Server
            var ldapident = new LdapDirectoryIdentifier(STR_LDAPURL, STR_LDAPPort);
            //LdapConnection lc = new LdapConnection(ldapident, null, AuthType.Basic);
            var lc = new LdapConnection(ldapident, new NetworkCredential(LDAPUser, LDAPPassword), AuthType.Basic);
            lc.Bind();
            lc.SessionOptions.ProtocolVersion = 3;
            lc.SessionOptions.SecureSocketLayer = true;

            //Configure the Search Request to Query the UCD OpenLDAP Server's People Search Base for a Specific User ID or Mail ID and Return the Requested Attributes 
            var attributesToReturn = new string[]
                                         {
                                             STR_UID, STR_EmployeeNumber, STR_Mail, STR_Telephone, STR_DisplayName, STR_CN,
                                             STR_SN, STR_GivenName, STR_PIDM
                                         };

            var sRequest = new SearchRequest(searchBase, searchFilter, SearchScope.Subtree, attributesToReturn);
            
            //Send the Request and Load the Response
            var sResponse = (SearchResponse) lc.SendRequest(sRequest);

            return sResponse;
        }

        public static List<DirectoryUser> GetUsersFromResponse(SearchResponse sResponse)
        {
            var users = new List<DirectoryUser>();

            foreach (SearchResultEntry result in sResponse.Entries)
            {
                var user = new DirectoryUser();

                //Grab out the first response entry

                foreach (DirectoryAttribute attr in result.Attributes.Values)
                {
                    switch (attr.Name)
                    {
                        case STR_UID:
                            user.LoginID = attr[0].ToString();
                            break;
                        case STR_GivenName:
                            user.FirstName = attr[0].ToString();
                            break;
                        case STR_SN:
                            user.LastName = attr[0].ToString();
                            break;
                        case STR_Mail:
                            user.EmailAddress = attr[0].ToString();
                            break;
                        case STR_EmployeeNumber:
                            user.EmployeeID = attr[0].ToString();
                            break;
                        case STR_CN:
                            user.FullName = attr[0].ToString();
                            break;
                        case STR_Telephone:
                            user.PhoneNumber = attr[0].ToString();
                            break;
                        default:
                            break;
                    }
                }

                users.Add(user);
            }

            return users;
        }

        public static List<DirectoryUser> LDAPSearchUsers(string employeeID, string firstName, string lastName,
                                                          string loginID, string email)
        {
            if (employeeID == null && firstName == null && lastName == null && loginID == null)
                return new List<DirectoryUser>();

            var searchFilter = new StringBuilder("(&");

            if (!string.IsNullOrEmpty(employeeID))
            {
                searchFilter.AppendFormat("({0}={1})", STR_EmployeeNumber, employeeID);
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                searchFilter.AppendFormat("({0}={1})", STR_GivenName, firstName);
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                searchFilter.AppendFormat("({0}={1})", STR_SN, lastName);
            }

            if (!string.IsNullOrEmpty(loginID))
            {
                searchFilter.AppendFormat("({0}={1})", STR_UID, loginID);
            }

            if (!string.IsNullOrEmpty(email))
            {
                searchFilter.AppendFormat("({0}={1})", STR_Mail, email);
            }

            searchFilter.Append(")");

            string strSearchFilter = searchFilter.ToString();
                //"(&(uid=" + (loginID ?? string.Empty) + ")(sn=" + (lastName ?? "Kirkland") + "))";
            string strSearchBase = STR_SearchBase;

            SearchResponse sResponse = GetSearchResponse(strSearchFilter, strSearchBase);

            return GetUsersFromResponse(sResponse);
        }

        /// <summary>
        /// Builds the ldap search filter and then gets out the first returned user
        /// </summary>
        public static DirectoryUser LDAPFindUser(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return null;

            var searchFilter = new StringBuilder("(|");

            //Append the login search
            searchFilter.AppendFormat("({0}={1})", STR_UID, searchTerm);

            //Append the email search
            searchFilter.AppendFormat("({0}={1})", STR_Mail, searchTerm);

            searchFilter.Append(")");

            SearchResponse sResponse = GetSearchResponse(searchFilter.ToString(), STR_SearchBase);

            List<DirectoryUser> foundUsers = GetUsersFromResponse(sResponse);

            if (foundUsers.Count == 0)
            {
                return null;
            }
            else
            {
                return foundUsers.First(); //Get the first returned user
            }
        }

        /// <summary>
        /// Builds a ldap search for student PIDM and then gets out the first returned user
        /// </summary>
        public static DirectoryUser LDAPFindStudent(string studentId)
        {
            if (string.IsNullOrEmpty(studentId)) return null;

            var searchFilter = string.Format("(&({0}={1}))", STR_StudentId, studentId);
            
            SearchResponse sResponse = GetSearchResponse(searchFilter, STR_SearchBase);

            List<DirectoryUser> foundUsers = GetUsersFromResponse(sResponse);

            if (foundUsers.Count == 0)
            {
                return null;
            }
            else
            {
                return foundUsers.First(); //Get the first returned user
            }
        }

        /// <summary>
        /// Prepare the 
        /// </summary>
        public static List<DirectoryUser> SearchUsers(string employeeID, string firstName, string lastName,
                                                      string loginID, string email)
        {
            return LDAPSearchUsers(employeeID, firstName, lastName, loginID, email);
        }

        /// <summary>
        /// Returns the single user that matches the search term -- either loginID or email
        /// </summary>
        public static DirectoryUser FindUser(string searchTerm)
        {
            return LDAPFindUser(searchTerm);
        }

        public static DirectoryUser FindStudent(string studentId)
        {
            return LDAPFindStudent(studentId);
        }
    }

    public class DirectoryUser
    {
        public DirectoryUser()
        {
        }

        public string EmployeeID { get; set; }
        public string LoginID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}