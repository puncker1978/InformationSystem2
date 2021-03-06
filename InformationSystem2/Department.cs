using System;
using System.Collections.Generic;
using System.Text;

namespace InformationSystem2
{
    internal class Department
    {
        #region Поля
        /// <summary>
        /// Уникальный идентификационный номер отдела
        /// </summary>
        private Guid id;

        /// <summary>
        /// Название отдела
        /// </summary>
        private string departmentName;

        /// <summary>
        /// Дата создания отдела
        /// </summary>
        private DateTime creationDate;

        /// <summary>
        /// Количество сотрудников отдела
        /// </summary>
        private int contingent;
        #endregion

        #region Свойства
        /// <summary>
        /// Уникальный идентификационный номер отдела
        /// </summary>
        public Guid Id { get => id; set => id = value; }

        /// <summary>
        /// Название отдела
        /// </summary>
        public string DepartmentName { get => departmentName; set => departmentName = value; }

        /// <summary>
        /// Дата создания отдела
        /// </summary>
        public DateTime CreationDate { get => creationDate.Date; set => creationDate = value; }

        /// <summary>
        /// Количество сотрудников отдела
        /// </summary>
        public int Contingent { get => contingent; set => contingent = value; }
        #endregion

        #region Конструкторы

        /// <summary>
        /// Пустой конструктор на всякий случай
        /// </summary>
        internal Department()
        { }

        /// <summary>
        /// Конструктор для создания отдела.
        /// Остальные поля (id и contingent заполняются вручную по мере надобности)
        /// </summary>
        /// <param name="departmentName">Название отдела</param>
        /// <param name="creationDate">Дата создания отдела</param>
        internal Department(string departmentName, DateTime creationDate)
        {
            this.Id = Guid.NewGuid();
            this.DepartmentName = departmentName;
            this.CreationDate = creationDate.Date;
        }

        /// <summary>
        /// Конструктор для получения данных из xml-файла с целью инициализации коллекции отделов;
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentName"></param>
        /// <param name="creationDate"></param>
        /// <param name="contingent"></param>
        internal Department(string id, string departmentName, string creationDate, string contingent)
        {
            this.Id = Guid.Parse(id);
            this.DepartmentName = departmentName;
            this.CreationDate = DateTime.Parse(creationDate);
            this.Contingent = int.Parse(contingent);
        }


        internal Department(Guid id, string departmentName, DateTime creationDate, int contingent)
        {
            this.Id = id;
            this.DepartmentName = departmentName;
            this.CreationDate = creationDate;
            this.Contingent = contingent;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод выводит сведения об отделе в консоль
        /// </summary>
        /// <returns>Отдел</returns>
        public override string ToString()
        {
            return $"№ Отдела:\t{this.Id}\n" +
                $"Наименование отдела:\t{this.DepartmentName}\n" +
                $"Дата основания отдела:\t{this.CreationDate.Date}\n" +
                $"Количество сотрудников:\t{this.Contingent}\n";
        }

        #endregion
    }
}
