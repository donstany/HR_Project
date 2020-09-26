namespace IOWebFramework.Shared.Common
{
	
	public static class MessageConstant
	{ 	
		public const string FieldIsMandatory = @"Полето '{0}' e задължително.";

		public const string GradeCertificateMsg = @"Оценката не може да е повече от 50 символа";
		public const int GradeCertificateMaxLength = 50;

		public const int GradeDiplomaMaxLength = 100;

		public const string GradeMsg = @"Въведете валидна оценка от 3.00 до 6.00";
		public const string GradeRegexPattern = @"(^([3-5][\.]{0,1}[0-9]{0,2})$)|(6.00)|(^([3-5][\,]{0,1}[0-9]{0,2})$)|(^[6]$)";

		public const string ExpMsg = @"Въведете валиден стаж във формат гг:мм:дд";
		public const string ExpRegexPattern = @"(^\d\d:(0[0-9]|1[012]):(0[0-9]|[12][0-9]|3[01])$)";
	}
}
