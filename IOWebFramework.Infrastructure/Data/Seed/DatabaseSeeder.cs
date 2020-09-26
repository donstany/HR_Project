using ExcelDataReader;
using IOWebFramework.Infrastructure.Data.Models;
using IOWebFramework.Infrastructure.Data.Models.Nomenclatures;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOWebFramework.Infrastructure.Data.Seed
{

    /// <summary>
    /// План за Seed-ване на базата.
    /// 1. Добавяне на номенклатура в DbContextExtensions.
    /// 2. Вкарване на данни в DatabaseSeeder.
    /// 3. Добавяне на ModelBuilder в ApplicationDbContext със StartValue по-голямо от броя записи.
    /// 4. drop-database
    /// 5. Delete migrations
    /// 6. Add migration
    /// 7. update-database
    /// </summary>
    public class DatabaseSeeder
    {
        private static DateTime defaultStartDate = new DateTime(1966, 1, 1);

        private readonly ApplicationDbContext context;

        public DatabaseSeeder(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task SeedClassificatorFromXlsx(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            List<Classifier> classifierSeedData = new List<Classifier>();

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var excelreader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = excelreader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                var classificator = dataSet.Tables[0];

                int counter = 0, currentId = 1;
                Dictionary<int, int> directionsPerParrentId = new Dictionary<int, int>();
                Dictionary<int, int> specialtiesPerParrentId = new Dictionary<int, int>();

                //вкарва областите и пълни речника с ID-та за вкарване на специалности
                foreach (DataRow row in classificator.Rows)
                {
                    //ако няма запис само в първата колона
                    if (!row[1].Equals(DBNull.Value) && row[0].Equals(DBNull.Value))
                    {
                        directionsPerParrentId[counter]++;
                    }
                    //ако има запис в първата
                    else if (!row[0].Equals(DBNull.Value))
                    {
                        directionsPerParrentId.Add(++counter, 1);
                        classifierSeedData.Add(new Classifier { Id = currentId, ParentId = currentId++, Name = row[0].ToString() });
                    }
                }

                //вкарва направленията и пълни речника с ID-та за вкарване на специалности
                foreach (DataRow row in classificator.Rows)
                {
                    var temp = directionsPerParrentId.First();

                    //ако няма запис в втората колона
                    if (row[1].Equals(DBNull.Value))
                    {
                        specialtiesPerParrentId[classifierSeedData.Last().Id]++;
                    }
                    //ако има запис в втората колона
                    else if (!row[1].Equals(DBNull.Value))
                    {
                        if (temp.Value == 0)
                        {
                            directionsPerParrentId.Remove(temp.Key);
                            temp = directionsPerParrentId.First();
                        }

                        classifierSeedData.Add(new Classifier { Id = currentId++, ParentId = temp.Key, Name = row[1].ToString() });
                        directionsPerParrentId[temp.Key]--;
                        specialtiesPerParrentId.Add(classifierSeedData.Last().Id, 1);
                    }
                }

                //вкарва специалностите
                foreach (DataRow row in classificator.Rows)
                {
                    var temp = specialtiesPerParrentId.First();

                    if (temp.Value == 0)
                    {
                        specialtiesPerParrentId.Remove(temp.Key);
                        temp = specialtiesPerParrentId.First();
                    }

                    classifierSeedData.Add(new Classifier { Id = currentId++, ParentId = temp.Key, Name = row[2].ToString() });
                    specialtiesPerParrentId[temp.Key]--;
                }
                excelreader.Close();
                excelreader.Dispose();
            }
            context.Classifiers.AddRange(classifierSeedData);
            await context.SaveChangesAsync();
        }

        public async Task SeedDepartmentsAndBranchesFromXlsx(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            List<Department> departmentSeedData = new List<Department>();
            List<Branch> branchSeedData = new List<Branch>();

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var excelreader = ExcelReaderFactory.CreateReader(stream))
            {
                var dataSet = excelreader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                var department = dataSet.Tables[0];
                var branch = dataSet.Tables[1];
                int currentId = 1;

                foreach (DataRow row in department.Rows)
                {
                    departmentSeedData.Add(new Department { Id = currentId, IsActive = true, OrderNumber = currentId, DateStart = defaultStartDate, BranchId = 1, Code = row[0].ToString(), Label = row[1].ToString() });
                    currentId++;
                }

                currentId = 1;

                foreach (DataRow row in branch.Rows)
                {
                    branchSeedData.Add(new Branch { Id = currentId, IsActive = true, OrderNumber = currentId, Code = row[0].ToString(), Label = row[1].ToString() });
                    currentId++;
                }
                excelreader.Close();
                excelreader.Dispose();
            }
            context.Departments.AddRange(departmentSeedData);
            context.Branches.AddRange(branchSeedData);
            await context.SaveChangesAsync();
        }

        //public async Task SeedBranchesInMemory()
        //{
        //    var b1 = new Branch { OrderNumber = 1, Id = 1, Code = "СФ", Label = "София", Description = "клон София", DateStart = defaultStartDate, IsActive = true };
        //    var b2 = new Branch { OrderNumber = 2, Id = 2, Code = "ВН", Label = "Варна", Description = "клон Варна", DateStart = defaultStartDate, IsActive = true };
        //    context.Branches.AddRange(new[] { b1, b2 });

        //    await context.SaveChangesAsync();
        //}

        //public async Task SeedDepartmentsInMemory()
        //{
        //    var d1 = new Department { OrderNumber = 1, Id = 1, BranchId = 1, Code = "MS", Label = "Microsoft Technologies", Description = "Microsoft Technologies", DateStart = defaultStartDate, IsActive = true };
        //    var d2 = new Department { OrderNumber = 2, Id = 2, BranchId = 2, Code = "MS", Label = "Microsoft Technologies", Description = "Microsoft Technologies", DateStart = defaultStartDate, IsActive = true };
        //    var d3 = new Department { OrderNumber = 3, Id = 3, BranchId = 1, Code = "JS", Label = "Javascript Technologies", Description = "Javascript Technologies", DateStart = defaultStartDate, IsActive = true };
        //    var d4 = new Department { OrderNumber = 4, Id = 4, BranchId = 2, Code = "JS", Label = "Javascript Technologies", Description = "Javascript Technologies", DateStart = defaultStartDate, IsActive = true };
        //    context.Departments.AddRange(new[] { d1, d2, d3, d4 });

        //    await context.SaveChangesAsync();
        //}

        public async Task SeedPersonsInMemory()
        {
            var p1 = new Person { Id = 1, FullName = "Станислав Цонев Станев", PID = "2904053520" };
            var p2 = new Person { Id = 2, FullName = "Димитър Д. Андреев", PID = "6511015357" };
            var p3 = new Person { Id = 3, FullName = "Мартин Горанов Николов", PID = "6807232728" };
            var p4 = new Person { Id = 4, FullName = "Иво Димитров Димитров", PID = "9501018821" };
            var p5 = new Person { Id = 5, FullName = "Иф Николаев Антонов", PID = "0901091456" };
            var p6 = new Person { Id = 6, FullName = "Димитър З. Баятов", PID = "3411181528" };
            context.Persons.AddRange(new[] { p1, p2, p3, p4, p5, p6 });

            await context.SaveChangesAsync();
        }
        public async Task SeedEmployeesInMemory()
        {
            var e1 = new Employee { Id = 1, PersonId = 1, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 103, PreviuosExperienceInIs = 2, PreviuosExperienceSummed = 105, Td = "12001", Email = "st.stanev@is-bg.net" };
            var e2 = new Employee { Id = 2, PersonId = 2, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 206, PreviuosExperienceInIs = 203, PreviuosExperienceSummed = 3, Td = "12002", Email = "d.andreev@is-bg.net" };
            var e3 = new Employee { Id = 3, PersonId = 3, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 25, PreviuosExperienceInIs = 22, PreviuosExperienceSummed = 3, Td = "12003", Email = "m.nikolov@is-bg.net" };
            var e4 = new Employee { Id = 4, PersonId = 4, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 255, PreviuosExperienceInIs = 252, PreviuosExperienceSummed = 3, Td = "12004", Email = "i.dimitrov@is-bg.net" };
            var e5 = new Employee { Id = 5, PersonId = 5, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 388, PreviuosExperienceInIs = 385, PreviuosExperienceSummed = 3, Td = "12005", Email= "i.antonov@is-bg.net" };
            var e6 = new Employee { Id = 6, PersonId = 6, IsActive = true, HireDate = new DateTime(2019, 05, 09), PreviuosExperience = 480, PreviuosExperienceInIs = 477, PreviuosExperienceSummed = 3, Td = "12006", Email = "d.bayatov@is-bg.net" };
            context.Employees.AddRange(new[] { e1, e2, e3, e4, e5, e6 });

            await context.SaveChangesAsync();
        }

        public async Task SeedProjectRolesInMemory()
        {
            var p1 = new ProjectRole { OrderNumber = 1, Id = 1, Code = "SQA", Label = "Senior QA", Description = "Senior quality assurance", DateStart = defaultStartDate, IsActive = true };
            var p2 = new ProjectRole { OrderNumber = 2, Id = 2, Code = "JDEV", Label = "Младши разработчик, софтуер", Description = "Младши разработчик, софтуер", DateStart = defaultStartDate, IsActive = true };
            var p3 = new ProjectRole { OrderNumber = 3, Id = 3, Code = "DEV", Label = "Разработчик, софтуер", Description = "Разработчик, софтуер", DateStart = defaultStartDate, IsActive = true };
            var p4 = new ProjectRole { OrderNumber = 4, Id = 4, Code = "SA", Label = "Системен администратор", Description = "Системен aдминистратор", DateStart = defaultStartDate, IsActive = true };
            var p5 = new ProjectRole { OrderNumber = 5, Id = 5, Code = "SDEV", Label = "Старши разработчик, софтуер", Description = "Старши разработчик, софтуер", DateStart = defaultStartDate, IsActive = true };
            context.ProjectRoles.AddRange(new[] { p1, p2, p3, p4, p5 });

            await context.SaveChangesAsync();
        }


        public async Task SeedDegreesInMemory()
        {
            //TEST no ids given for the squence
            var d1 = new Degree { Id = 1, OrderNumber = 1, IsActive = true, Code = "Masters", Label = "Магистър", Description = "Магистър" };
            var d2 = new Degree { Id = 2, OrderNumber = 2, IsActive = true, Code = "Bachelor", Label = "Бакалавър", Description = "Бакалавър" };
            var d3 = new Degree { Id = 3, OrderNumber = 3, IsActive = true, Code = "Middle", Label = "Средно", Description = "Средно" };
            context.Degrees.AddRange(new[] { d1, d2, d3 });

            await context.SaveChangesAsync();
        }

        public async Task SeedEduInstitutionsInMemory()
        {
            var ei1 = new EducationInstitution { Id = 1, OrderNumber = 1, IsActive = true, Code = "СУ", Label = "Софийски университет", Description = "Софийски университет \"Св.Климент Охридски\"", DateStart = defaultStartDate };
            var ei2 = new EducationInstitution { Id = 2, OrderNumber = 2, IsActive = true, Code = "ПУ", Label = "Пловдивски университет", Description = "Пловдивски университет \"Паисий Хилендарски\"", DateStart = defaultStartDate };
            var ei3 = new EducationInstitution { Id = 3, OrderNumber = 3, IsActive = true, Code = "ТУВ", Label = "ТУ Варна", Description = "Технически Университет Варна", DateStart = defaultStartDate };
            var ei4 = new EducationInstitution { Id = 4, OrderNumber = 4, IsActive = true, Code = "ТУС", Label = "ТУ София", Description = "Технически Университет София", DateStart = defaultStartDate };
            var ei5 = new EducationInstitution { Id = 5, OrderNumber = 5, IsActive = true, Code = "БИТ", Label = "УниБИТ", Description = "Университет по Библиотекознание и Информационни Технологии", DateStart = defaultStartDate };
            context.EducationInstitutions.AddRange(new[] { ei1, ei2, ei3, ei4, ei5 });

            await context.SaveChangesAsync();
        }

        public async Task SeedTrainingCentersInMemory()
        {
            var tc1 = new TrainingCenter { Id = 1, OrderNumber = 1, IsActive = true, Code = "ITCE", Label = "Innovative Training And Consulting Solutions", Description = "Innovative Training And Consulting Solutions", DateStart = defaultStartDate };
            var tc2 = new TrainingCenter { Id = 2, OrderNumber = 2, IsActive = true, Code = "SU", Label = "Softuni", Description = "Софтуерен университет", DateStart = defaultStartDate };
            var tc3 = new TrainingCenter { Id = 3, OrderNumber = 3, IsActive = true, Code = "TK", Label = "Telerik", Description = "Telerik Academy", DateStart = defaultStartDate };
            context.TrainingCenters.AddRange(new[] { tc1, tc2, tc3 });

            await context.SaveChangesAsync();
        }

        public async Task SeedTrainingNamesInMemory()
        {
            var tn1 = new TrainingName { Id = 1, OrderNumber = 1, IsActive = true, Code = "C#B", Label = "C# Fundamentals", Description = "Основи в C#", DateStart = defaultStartDate };
            var tn2 = new TrainingName { Id = 2, OrderNumber = 2, IsActive = true, Code = "JSF", Label = "JS Fundamentals", Description = "JavaScript Fundamentals", DateStart = defaultStartDate };
            var tn3 = new TrainingName { Id = 3, OrderNumber = 3, IsActive = true, Code = "EH", Label = "Ethical Hacking", Description = "Етично хакерство", DateStart = defaultStartDate };
            var tn4 = new TrainingName { Id = 4, OrderNumber = 4, IsActive = true, Code = "QA", Label = "QA Automation", Description = "Подсигуряване на качествен код", DateStart = defaultStartDate };
            context.TrainingNames.AddRange(new[] { tn1, tn2, tn3, tn4 });

            await context.SaveChangesAsync();
        }

        public async Task SeedCertificateNameIssuersInMemory()
        {
            var cni1 = new CertificateNameIssuer { Id = 1, IsActive = true, Name = "Microsoft", ParentId = 1 };
            var cni2 = new CertificateNameIssuer { Id = 2, IsActive = true, Name = "Telerik Academy", ParentId = 2 };
            var cni3 = new CertificateNameIssuer { Id = 3, IsActive = true, Name = "SoftUni", ParentId = 3 };
            var cni4 = new CertificateNameIssuer { Id = 4, IsActive = true, Name = "C# Fundamentals", ParentId = 1 };
            var cni5 = new CertificateNameIssuer { Id = 5, IsActive = true, Name = "C# Basics", ParentId = 1 };
            var cni6 = new CertificateNameIssuer { Id = 6, IsActive = true, Name = "JS Fundamentals", ParentId = 2 };
            var cni7 = new CertificateNameIssuer { Id = 7, IsActive = true, Name = "QA Automation", ParentId = 3 };
            context.CertificateNameIssuers.AddRange(new[] { cni1, cni2, cni3, cni4, cni5, cni6, cni7 });

            await context.SaveChangesAsync();
        }

        public async Task SeedCertificateTypesInMemory()
        {
            var ct1 = new CertificateType { Id = 1, OrderNumber = 1, IsActive = true, Code = "Dev", Label = "Developer", Description = "Софтуерен разработчик", DateStart = defaultStartDate };
            var ct2 = new CertificateType { Id = 2, OrderNumber = 2, IsActive = true, Code = "QA", Label = "QA Engineer", Description = "Подсигуряване на качествен код", DateStart = defaultStartDate };
            var ct3 = new CertificateType { Id = 3, OrderNumber = 3, IsActive = true, Code = "SA", Label = "SysAdmin", Description = "Системен Администратор", DateStart = defaultStartDate };
            var ct4 = new CertificateType { Id = 4, OrderNumber = 4, IsActive = false, Code = "МП", Label = "Машинопис", Description = "Писане на пишеща машина. ", DateStart = defaultStartDate, DateEnd = new DateTime(2010, 1, 1) };

            context.CertificateTypes.AddRange(new[] { ct1, ct2, ct3, ct4 });

            await context.SaveChangesAsync();
        }

        //public async Task SeedTrainingNameInMemory()
        //{
        //    var tn1 = new TrainingName { OrderNumber = 1, Id = 1, IsActive = true, Code = "СУ", Label = "Софийски университет", Description = "Софийски университет \"Св.Климент Охридски\"", DateStart = defaultStartDate };
        //    var tn2 = new TrainingName { OrderNumber = 2, Id = 2, IsActive = true, Code = "ПУ", Label = "Пловдивски университет", Description = "Пловдивски университет \"Паисий Хилендарски\"", DateStart = defaultStartDate };
        //    context.EducationInstitutions.AddRange(new[] { tn1, tn2 });

        //    await context.SaveChangesAsync();
        //}

    }
}