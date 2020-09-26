using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IOWebFramework.Shared.Common.MessageConstant;

namespace IOWebFramework.Core.Models.ProjectDetail
{
    public class TechnologyViewModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [DisplayName("Технология")]
        [Required(ErrorMessage = FieldIsMandatory)]
        public int TechnologyId { get; set; }
    }
}
