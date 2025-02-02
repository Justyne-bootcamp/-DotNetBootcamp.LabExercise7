﻿using System;
using System.Collections.Generic;
using System.IO;
using CSharp.LabExercise7.Service;
using CSharp.LabExercise7.Model;
using CSharp.LabExercise7.Renderer;

namespace CSharp.LabExercise7
{
    class StudentDBManagementSystem
    {
        public void AddStudent(DBService dBService)
        {
            Student student = new Student()
            {
                StudentID = "2022-" + ((dBService.GetStudentCount()+1).ToString("D" + 4))
            };
            GeneralRenderer generalRenderer = new GeneralRenderer();
            generalRenderer.RenderAddStudent(student);
            dBService.AddStudentToDb(student);
        }

        public void ListStudents(DBService dBService)
        {
            if(dBService.GetStudentCount() == 0)
            {
                Console.WriteLine("No Student is on the list");
            }
            foreach (var student in dBService.GetStudents())
            {
                Console.WriteLine(student.ToString());
            }
        }
        public void ModifyStudent(DBService dBService)
        {
            ListStudents(dBService);
            Console.WriteLine("==========================");
            Console.Write("Enter Student ID to Modify:");
            string idToModify = Console.ReadLine();

            SearchService searchService = new SearchService();
            int indexToModify = searchService.GetIndexByStudentID(dBService.GetStudents(), idToModify);
            if(indexToModify != -1)
            {
                GeneralRenderer generalRenderer = new GeneralRenderer();
                generalRenderer.RenderAddStudent(dBService.GetStudents()[indexToModify]);
            }
            else
            {
                Console.WriteLine($"Student with Student ID: {idToModify} is not Found.");
            }
        }
        public void DeleteStudent(DBService dBService)
        {
            ListStudents(dBService);
            Console.WriteLine("==========================");
            Console.Write("Enter Student ID to delete:");
            string idToDelete = Console.ReadLine();

            SearchService searchService = new SearchService();
            int indexToDelete = searchService.GetIndexByStudentID(dBService.GetStudents(), idToDelete);
            
            if(indexToDelete != -1)
            {
                dBService.RemoveStudentByIndex(indexToDelete);
            }
            else
            {
                Console.WriteLine($"Student with Student ID: {idToDelete} is not Found. Cannot be deleted");
            }

        }
        public void WriteToFile(DBService dBService)
        {
            if (dBService.GetStudentCount() > 0)
            {
                DateGetterService dateGetterService = new DateGetterService();

                string fileName = $"studentDB.{dateGetterService.GetCurrentDate(DateTmePattern.FullDate).Replace("-", "")}.txt";
                string filePath = @$"C:\Users\justyne.nam-ay\Documents\backend\CSharp.LabExercise7\fileOutputs\{fileName}";
                Console.WriteLine(filePath); ;
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Student ID \t\t First Name \t\t Last Name \t\t Grade Level \t\t Section");
                    foreach (Student student in dBService.GetStudents())
                    {
                        writer.WriteLine(student.ToString());
                    }
                }
            }
            System.Environment.Exit(0);
        }
        public void LogFile()
        {
            DateGetterService dateGetterService = new DateGetterService();

            string fileName = $"application.{dateGetterService.GetCurrentDate(DateTmePattern.CurrentDate).Replace("-", "")}.log";
            string filePath = @$"C:\Users\justyne.nam-ay\Documents\backend\CSharp.LabExercise7\fileOutputs\{fileName}";
            Console.WriteLine(fileName);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(dateGetterService.GetCurrentDate(DateTmePattern.FullDate));

            }
        }
    }
}
