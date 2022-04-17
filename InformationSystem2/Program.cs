﻿using System;

namespace InformationSystem2
{

    internal class Program
    {

        //Статический метод вычисления номера пары неотрицательных
        //целых чисел (Канторовская нумерация пар)
        static int KantorPairs(int x, int y)
        {
            int n = (x + y) * (x + y + 1) / 2 + x;
            return n;
        }

        static void Main(string[] args)
        {
            #region Заполнение xml-файлов тестовыми данными
            //Создали экземпляр класса Organization
            Organization organization = new Organization();

            //Создали экземпляр класа Random
            Random rnd = new Random();
            int i, j = 0;

            //Создадим, для примера, 20 сотрудников и
            //4 отдела. И распределим по 5 сотрудников в
            //каждый отел.
            for (i = 0; i < 4; i++)
            {
                //Инициализация экземпляра класса Department
                Department department = new Department($"Отдел {KantorPairs(i, j)}",
                    new DateTime(2021, rnd.Next(1, 12), rnd.Next(1, 28)).Date);

                //Добавляем инициализированный отдел в список всех отделов
                organization.AddDepartmentToDepartments(department);

                for (j = 0; j < 5; j++)
                {
                    //Инициализация экземпляра класса Employee
                    Employee employee = new Employee($"Фамилия {rnd.Next(1, 10)}",
                        $"Имя {rnd.Next(1, 10)}", rnd.Next(18, 70), rnd.Next(1, 3));

                    //Добавляем сотрудника в список всех сотрудников
                    organization.AddEmployeeToEmployees(employee);

                    //Добавляем сотрудника в отдел
                    organization.AddEmployeeToDepartment(department, employee);
                }
            }
            #endregion


            #region Сохраним тестовые данные о сотрудниках и отделах в xml-файлах
            Console.Clear();
            //Добавляем коллекцию сотрудников в xml-файл
            organization.AddEmployeesToXml();

            //Добавляем коллекцию отделов в xml-файл
            organization.AddDepartmentsToXml();

            Console.ReadKey();

            #endregion
        }
    }
}
