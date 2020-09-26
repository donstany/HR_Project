���������� �� �������� � ���������� ENVIRONMENT:

	1. � Package Manager Console �� ������ ������������ �� �������.
		1.1. ������� ���� Default Projects e ������ �� IOWebFramework.Infrastructure
	
		PM> $env:ASPNETCORE_ENVIRONMENT='QA'
			��� $env:ASPNETCORE_ENVIRONMENT='Development'
			��� $env:ASPNETCORE_ENVIRONMENT='Production'

	2. ����������� ��� � �������� connection string ��� ������ � ��������� -whatif.
		
		PM>drop-Database -context ApplicationDbContext -whatif
	
		Build started...
		Build succeeded.
		What if: Performing the operation "Drop-Database" on target "database 'io_hr_project_DEV' on server 'tcp://10.240.145.54:5432'".

	3.  "��������" �� ���� � �������� connection string ��� ������ � "��������".
		drop-Database -context ApplicationDbContext

	4.  ��������� �� ���� ����.
		 Update-Database -context ApplicationDbContext
		 Update-Database -context CdnDbContext
	
	5. Seed-�� �� ���� � �����.
		5.1. ����� ��, �� ������� �� ���������� ��� VS e ������� ����� ����� �� ���������
		5.2. �������� �� IOWebFramework �������
	----------------------
	 
	 I. �������� �� �������� � ��������.
	  I.1. ����� ��, �� ����������� �� ������ � IOWebFramework
	  I.2. � �������� ���� �� Package Manager Console, Default Project � ������ IOWebFramework.Infrasturcture
	 Add-Migration AddColumnInTable -Context ApplicationDbContext
	 
	 II. ��������� �� �������� � ��������.
	 Remove-Migration -Context ApplicationDbContext

	 III. ��������� �� �������� � ��������.
	 Update-database -Context ApplicationDbContext


