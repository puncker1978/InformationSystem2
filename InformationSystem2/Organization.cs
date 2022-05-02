using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace InformationSystem2
{
    internal class Organization
    {
        #region Приватные коллекции
        /// <summary>
        /// Список отделов
        /// </summary>
        private List<Department> departments = new List<Department>();

        /// <summary>
        /// Список сотрудников
        /// </summary>
        private List<Employee> employees = new List<Employee>();
        #endregion

        #region Свойства для коллекций
        /// <summary>
        /// Список отделов
        /// </summary>
        internal List<Department> Departments { get => departments; set => departments = value; }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        internal List<Employee> Employees { get => employees; set => employees = value; }
        #endregion

        #region Конструктры

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        internal Organization()
        { }

        #endregion

        #region Методы для заполнения xml-файлов тестовыми даннми
        /// <summary>
        /// Метод добавления списка сотрудников в xml-файл для заполнения файла тестовыми данными
        /// </summary>
        internal void AddEmployeesToXml()
        {
            XDocument xDoc = XDocument.Load("employees.xml");
            XElement root = xDoc.Element("Employees");
            foreach (Employee employee in Employees)
            {
                root.Add(new XElement("Employee",
                    new XElement("Id", employee.Id),
                    new XElement("Фамилия", employee.SecondName),
                    new XElement("Имя", employee.FirstName),
                    new XElement("Возраст", employee.Age),
                    new XElement("Зарплата", employee.Total),
                    new XElement("Проекты", employee.Projects),
                    new XElement("Отдел", employee.IdDepartment)));
            }
            xDoc.Save("employees.xml");
        }

        /// <summary>
        /// Метод добавления списка отделов в xml-файл для заполнения файла тестовыми данными
        /// </summary>
        internal void AddDepartmentsToXml()
        {
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement root = xDoc.Element("Departments");
            foreach (Department department in Departments)
            {
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id),
                            new XElement("Отдел", department.DepartmentName),
                            new XElement("Создан", department.CreationDate),
                            new XElement("Контингент", department.Contingent)));
            }
            xDoc.Save("departments.xml");
        }

        /// <summary>
        /// Метод добавления нового отдела к списку всех отделов
        /// </summary>
        /// <param name="department">Добавляемый отдел</param>
        internal void AddDepartmentToDepartments(Department department)
        {
            Departments.Add(department);
        }

        /// <summary>
        /// Метод добавления нового сотрудника к списку всех сотрудников
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        internal void AddEmployeeToEmployees(Employee employee)
        {
            employees.Add(employee);
        }

        /// <summary>
        /// Метод присваивает каждому сотруднику отдела
        /// уникальный идентификационный номер отдела
        /// </summary>
        /// <param name="department">Отдел к которому прикрепляется сотрудник</param>
        /// <param name="employee">Прикрепляемый сотрудник</param>
        internal void AddEmployeeToDepartment(Department department, Employee employee)
        {
            employee.IdDepartment = department.Id;
            department.Contingent++;
        }
        #endregion

        #region Рабочие методы

        /// <summary>
        /// Метод добавления одного сотрудника в xml-файл
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        internal void AddEmployeeToXml(Employee employee)
        {
            XDocument xdoc = XDocument.Load("employees.xml");
            XElement root = xdoc.Element("Employees");

            if (root != null)
            {
                // добавляем новый элемент
                root.Add(new XElement("Employee",
                            new XElement("Id", employee.Id),
                            new XElement("Фамилия", employee.SecondName),
                            new XElement("Имя", employee.FirstName),
                            new XElement("Возраст", employee.Age),
                            new XElement("Зарплата", employee.Total),
                            new XElement("Отдел", employee.IdDepartment)));

                xdoc.Save("employees.xml");
            }
        }

        /// <summary>
        /// Метод добавления одного отдела в xml-файл
        /// </summary>
        /// <param name="department"></param>
        internal void AddDepartmentToXml(Department department)
        {
            XDocument xdoc = XDocument.Load("departments.xml");
            XElement root = xdoc.Element("Departments");

            if (root != null)
            {
                // добавляем новый элемент
                root.Add(new XElement("Department",
                            new XElement("Id", department.Id),
                            new XElement("Фамилия", department.DepartmentName),
                            new XElement("Имя", department.CreationDate),
                            new XElement("Возраст", department.Contingent)));

                xdoc.Save("departments.xml");
            }
        }


        /// <summary>
        /// Метод поиска отдела в xml-файле осуществляется по названию отдела,
        /// поскольку название каждого отдела уникально.
        /// При попытке удаления отдела, мы должны убедиться,
        /// что отдел с таким названием существует в xml-файле.
        /// При попытке создания нового отдела, мы должны убедиться,
        /// что отдела с таким названием в xml-файле нет
        /// </summary>
        /// <param name="departmentName">Название отдела</param>
        /// <returns>Если отдел найден, возвращает true. Если отдел не найден, возвращает false</returns>
        internal bool FindDepartment(string departmentName)
        {
            bool result = false;
            
            XDocument xDoc = XDocument.Load("departments.xml");
            XElement Departments = xDoc.Element("Departments");
            foreach (XElement _department in Departments.Elements("Department"))
            {
                if (_department.Element("Отдел").Value == departmentName)
                {
                    result = true;
                }
            }
            xDoc.Save("departments.xml");
            return result;
        }
       
        internal List<Employee> FindEmployee(Employee employee)
        {
            List<Employee> result = new List<Employee>();
            XDocument xDoc = XDocument.Load("employees.xml");
            XElement Employees = xDoc.Element("Employees");
            foreach(XElement _employee in Employees.Elements("Employee"))
            {
                if(_employee.Element("Фамилия").Value == employee.SecondName && 
                    _employee.Element("Имя").Value == employee.FirstName)
                {
                    result.Add(employee);
                }
            }
            xDoc.Save("employees.xml");
            return result;
        }

        #endregion
    }
}
