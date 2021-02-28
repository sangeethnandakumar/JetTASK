using ExpressMicroPermissions;
using ExpressMicroPermissions.Models;
using System;

namespace PermissionSeeder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MicroPermissions.AddPermission("Add.Project", "Allows an account to add a project", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("Update.Project", "Allows an account to update a project", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("View.Projects", "Allows an account to view projects", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("Delete.Project", "Allows an account to view projects", PermissionLevel.STANDARD);

            MicroPermissions.AddPermission("Add.Client", "Allows an account to add a client", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("Update.Client", "Allows an account to update a client", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("View.Clients", "Allows an account to view clients", PermissionLevel.STANDARD);
            MicroPermissions.AddPermission("Delete.Client", "Allows an account to delete a client", PermissionLevel.STANDARD);

            MicroPermissions.BindPermissionToUser(1, "Add.Project");
            MicroPermissions.BindPermissionToUser(1, "Update.Project");
            MicroPermissions.BindPermissionToUser(1, "View.Projects");
            MicroPermissions.BindPermissionToUser(1, "Delete.Projects");

            MicroPermissions.BindPermissionToUser(1, "Add.Client");
            MicroPermissions.BindPermissionToUser(1, "Update.Client");
            MicroPermissions.BindPermissionToUser(1, "View.Clients");
            MicroPermissions.BindPermissionToUser(1, "Delete.Client");
        }
    }
}