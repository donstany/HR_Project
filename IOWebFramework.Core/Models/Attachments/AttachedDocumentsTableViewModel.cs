namespace IOWebFramework.Core.Models.Attachments
{
    public class AttachedDocumentsTableViewModel
    {
        public long DocumentId { get; set; }

        public int DocumentType { get; set; }

        public bool HideAddNewDocumentButton { get; set; }

        public bool HideAllButtons { get; set; }

        public bool HideDeleteButton { get; set; }

        //public DateTime? DateToCompare { get; set; }

        //public int MonthsToSubtract { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Попълва се, когато ще има повече от една таблица с прикачени документи на един екран, 
        /// за да се избегне повторение на ID на таблицата
        /// </summary>
        public string Index { get; set; }
    }
}
