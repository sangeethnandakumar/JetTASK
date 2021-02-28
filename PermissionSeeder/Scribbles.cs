using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSeeder
{
    internal class Scribbles
    {
        public struct student
        {
            public int id;
            public String fName, lName;
            public float gpa;
        };

        public static void printList(List<student> stds)
        {
            for (int i = 0; i < stds.Count; i++)
            {
                Console.WriteLine("Student #" + (i + 1) + " :");
                Console.WriteLine("First Name: " + stds[i].fName);
                Console.WriteLine("Last Name: " + stds[i].lName);
                Console.WriteLine("ID: " + stds[i].id);
                Console.WriteLine("GPA: " + stds[i].gpa);
                Console.WriteLine();
            }
        }

        public static void gpa(List<student> stds)
        {
            //Instead of length, Use count in a collection
            for (int i = 0; i < stds.Count; i++)
            {
                var item = stds[i];
                Console.WriteLine("Enter GPA for ID: " + item.id);
                item.gpa = Int32.Parse(Console.ReadLine());
            }
        }

        public static void TestRun()
        {
            //Instead of an array, Declare studets as a list (collection)
            List<student> stds = new List<student>();

            for (int n = 0; n < 2; n++)
            {
                //Create a student everytime you loop
                var std = new student();
                Console.WriteLine("Enter the first name of student " + (n + 1));
                std.fName = Console.ReadLine();
                Console.WriteLine("Enter the last name of student " + (n + 1));
                std.lName = Console.ReadLine();
                Console.WriteLine();
                std.id = n + 1;
                std.gpa = 0;

                //And add it to our list
                stds.Add(std);
            }
            gpa(stds);
            printList(stds);
        }
    }
}