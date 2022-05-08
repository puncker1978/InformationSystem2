using System;
using System.Collections.Generic;
using System.Text;

namespace InformationSystem2
{
    internal class Employee
    {
        #region Константы
        /// <summary>
        /// Ставка заработной платы труда на 1 проект
        /// </summary>
        private readonly int salary = 1000;
        #endregion

        #region Поля
        /// <summary>
        /// Уникальный идентификационный номер сотрудника
        /// </summary>
        private Guid id;

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        private string secondName;

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        private string firstName;

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        private int age;

        /// <summary>
        /// Номер отдела, к которому прикреплён сотрудник
        /// </summary>
        private Guid idDepartment;

        /// <summary>
        /// Количество проектов, закрепленных за сотрудником
        /// </summary>
        private int projects;

        /// <summary>
        /// Итоговая заработная плата. Расчитывается как произведение количества проектов на ставку заработной платы.
        /// </summary>
        private int total;
        #endregion

        #region Свойства
        /// <summary>
        /// Уникальный идентификационный номер сотрудника
        /// </summary>
        internal Guid Id { get => id; set => id = value; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        internal string SecondName { get => secondName; set => secondName = value; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        internal string FirstName { get => firstName; set => firstName = value; }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        internal int Age { get => age; set => age = value; }

        /// <summary>
        /// Номер отдела, к которому прикреплён сотрудник
        /// </summary>
        internal Guid IdDepartment { get => idDepartment; set => idDepartment = value; }

        /// <summary>
        /// Количество проектов, закрепленных за сотрудником
        /// </summary>
        internal int Projects { get => projects; set => projects = value; }

        /// <summary>
        /// Итоговая заработная плата. Расчитывается как произведение количества проектов на ставку заработной платы.
        /// </summary>
        internal int Total { get => total; set => total = value; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Пустой конструктор на всякий случай
        /// </summary>
        internal Employee()
        { }

        /// <summary>
        /// Конструктор для
        /// 1) создания сотрудника и инициализации тестовых данных. Конструктор инициализирует поля: secondName,
        /// firstName, age и projects. Поле total (итоговая заработная плата) расчитывается как произведение
        /// количества проектов, закреплённых за сотрудником (projects) на ставку (salary).
        /// 2) создания нового сотрудника
        /// </summary>
        /// <param name="secondName">Фамилия сотрудника</param>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="age">Возраст сотрудника</param>
        /// <param name="projects">Количество проектов, закрепленных за сотрудником</param>
        internal Employee(string secondName, string firstName, int age, int projects)
        {
            this.Id = Guid.NewGuid();
            this.SecondName = secondName;
            this.FirstName = firstName;
            this.Age = age;
            this.Projects = projects;
            this.Total = salary * this.Projects;
        }

        /// <summary>
        /// Конструктор класса для изменения заработной платы в зависимости от количества проектов
        /// </summary>
        /// <param name="projects">Количество проектов</param>
        internal Employee(int projects)
        {
            this.Total = salary * projects;
        }

        /// <summary>
        /// Конструктор для создания эземпляра класса Employee при чтении из xml-файла
        /// </summary>
        /// <param name="id">Уникальный идентификационный номер сотрудника</param>
        /// <param name="secondName">Фамилия сотрудника</param>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="age">Возраст сотрудника</param>
        /// <param name="idDepartment">Уникальный идентификационный номер отдела к которому прикреплён сотрудник</param>
        /// <param name="projects">Количество проектов, закреплённых за сотрудником</param>
        /// <param name="total">Заработная плата сотрудника</param>
        internal Employee(string id,
                          string secondName,
                          string firstName,
                          int age,
                          string idDepartment,
                          int projects,
                          int total)
        {
            this.Id = Guid.Parse(id);
            this.SecondName = secondName;
            this.FirstName = firstName;
            this.Age = age;
            this.IdDepartment = Guid.Parse(idDepartment);
            this.Projects = projects;
            this.Total = total;
        }

        /// <summary>
        /// Конструктор для создания экземпляра класса Employee по полям:
        /// Фамилия, имя, возраст, отдел, число проектов
        /// </summary>
        /// <param name="secondName"></param>
        /// <param name="firstName"></param>
        /// <param name="age"></param>
        /// <param name="id"></param>
        /// <param name="projects"></param>
        //public Employee(string secondName, string firstName, int age, Guid idDepartment, int projects)
        //{
        //    this.SecondName = secondName;
        //    this.FirstName = firstName;
        //    this.Age = age;
        //    this.IdDepartment = idDepartment;
        //    this.Projects = projects;
        //    this.Total = salary * projects;
        //}
        #endregion

        #region Методы
        /// <summary>
        /// Метод выводит сведения о сотруднике в консоль
        /// </summary>
        /// <returns>Сотрудник</returns>
        public override string ToString()
        {
            return $"№ Сотрудника\t{this.id}\n" +
                $"Фамилия\t{this.SecondName}\n" +
                $"Имя\t{this.FirstName}\n" +
                $"Возраст\t{this.Age}\n" +
                $"Количество проектов\t{this.Projects}\n" +
                $"Заработная плата\t{this.Total}\n" +
                $"№ Отдела\t{this.IdDepartment}";
        }
        #endregion
    }
}
