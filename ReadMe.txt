Стартиране на миграция в съответния ENVIRONMENT:

	1. В Package Manager Console се сетват променливите за средите.
		1.1. Провери дали Default Projects e сетнат на IOWebFramework.Infrastructure
	
		PM> $env:ASPNETCORE_ENVIRONMENT='QA'
			или $env:ASPNETCORE_ENVIRONMENT='Development'
			или $env:ASPNETCORE_ENVIRONMENT='Production'

	2. Проверяваме кой е активния connection string към базата с параметър -whatif.
		
		PM>drop-Database -context ApplicationDbContext -whatif
	
		Build started...
		Build succeeded.
		What if: Performing the operation "Drop-Database" on target "database 'io_hr_project_DEV' on server 'tcp://10.240.145.54:5432'".

	3.  "Дропване" на база с активния connection string към базата и "контекст".
		drop-Database -context ApplicationDbContext

	4.  Създаване на нова база.
		 Update-Database -context ApplicationDbContext
		 Update-Database -context CdnDbContext
	
	5. Seed-не на база с данни.
		5.1. Увери се, че профила за стартиране във VS e средата която искаш да стартираш
		5.2. Стартира се IOWebFramework проекта
	----------------------
	 
	 I. Добаване на миграция с контекст.
	  I.1. Увери се, че стартиращия ти проект е IOWebFramework
	  I.2. В падащото менщ на Package Manager Console, Default Project е избран IOWebFramework.Infrasturcture
	 Add-Migration AddColumnInTable -Context ApplicationDbContext
	 
	 II. Изтриване на миграция с контекст.
	 Remove-Migration -Context ApplicationDbContext

	 III. Прилагане на миграция с контекст.
	 Update-database -Context ApplicationDbContext


