using BuildingCompany.Models;
using BuildingCompany.Models.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany
{
    public enum Role
    {
        Accountant,
        Admin,
        Default_User,
        Foreman,
        Workers,
        Director
    };

    public class DbConnection
    {
        public static string[] Args { get; set; }
        //public static Members currentMember { get; set; }

        public static string[] defaultUser = new string[2] { "Default_user", "1234" }; // TODO just for start time admin role
        // TODO make default role in BD and chage upper login and password

        public static DB_ContextFactory db_ContextFactory = new DB_ContextFactory();
        public static DB_Context dB_Context;

        public static DB_User user;
        public static bool login = false;
        public static Role UserRole;
        public static Members CurrentMember;
        public static Works createWork;

        public static DB_Context SetDefaultInstanse()
        {
            user = null;
            login = false;
            Args = null;
            UserRole = Role.Default_User;

            dB_Context = db_ContextFactory.CreateDbContext(defaultUser);
            return dB_Context;
        }

        public static DB_Context GetInstance()
        {
            if (dB_Context == null)
            {
                if (Args != null)
                {
                    dB_Context = db_ContextFactory.CreateDbContext(Args);
                }
                else
                {
                    SetDefaultInstanse();
                    //UserRole = Role.Default_User;
                    //dB_Context = db_ContextFactory.CreateDbContext(defaultUser);
                }
            }
            return dB_Context;
        }

        public static DB_Context ResetUser(String[] argss)
        {
            user = new DB_User(argss);
            Args = argss;
            dB_Context = db_ContextFactory.CreateDbContext(argss);
            return dB_Context;
        }
    }
}
