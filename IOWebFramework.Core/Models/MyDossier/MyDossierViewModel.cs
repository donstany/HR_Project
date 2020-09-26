using IOWebFramework.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.MyDossier
{
    public class MyDossierViewModel
    {
        public int Id { get; set; }

        [DisplayName("Снимка")]
        public byte[] Photo { get; set; }

        [DisplayName("Име")]
        public string PersonName { get; set; }

        [DisplayName("ЕГН")]
        public string PID { get; set; }

        [DisplayName("ТД номер")]
        public string Td { get; set; }

        [DisplayName("Дата на назначаване")]
        public DateTime HireDate { get; set; }

        [DisplayName("Клон")]
        public string Branch { get; set; }

        [DisplayName("Отдел")]
        public string Department { get; set; }

        [DisplayName("Позиция")]
        public string Position { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        [DisplayName("Телефон")]
        public string Telephone { get; set; }

        [DisplayName("Еmail")]
        public string Email { get; set; }
    }
}
