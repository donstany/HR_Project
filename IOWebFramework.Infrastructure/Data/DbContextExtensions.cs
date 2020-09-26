using IOWebFramework.Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace IOWebFramework.Infrastructure.Data
{
    public static class DbContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context, bool isDevelopmentEnvironment)
        {
            //https://stackoverflow.com/questions/42355481/auto-create-database-in-entity-framework-core
            // alternative way to execute all migration on empty db
            //context.Database.Migrate();

            context.Database.EnsureCreated();

            // Because each of the following seed method needs to do a save
            // (the data they're importing is relational), we need to call
            // SaveAsync within each method.
            // So let's keep tabs on the counts as they come 
            var dbSeeder = new DatabaseSeeder(context);

            if (!context.Classifiers.Any())
            {
                string currentDir = Directory.GetCurrentDirectory();
                var parent = new DirectoryInfo(currentDir).Parent.FullName;
                var pathToData = Path.Combine(parent, "IOWebFramework.Infrastructure\\Data\\Seed", "Classifier.xlsx");
                dbSeeder.SeedClassificatorFromXlsx(pathToData).Wait();
            }

            if (!context.Departments.Any())
            {
                string currentDir = Directory.GetCurrentDirectory();
                var parent = new DirectoryInfo(currentDir).Parent.FullName;
                var pathToData = Path.Combine(parent, "IOWebFramework.Infrastructure\\Data\\Seed", "DepartmentsAndBranches.xlsx");
                dbSeeder.SeedDepartmentsAndBranchesFromXlsx(pathToData).Wait();
            }

            if (!context.Branches.Any())
            {
                string currentDir = Directory.GetCurrentDirectory();
                var parent = new DirectoryInfo(currentDir).Parent.FullName;
                var pathToData = Path.Combine(parent, "IOWebFramework.Infrastructure\\Data\\Seed", "DepartmentsAndBranches.xlsx");
                dbSeeder.SeedDepartmentsAndBranchesFromXlsx(pathToData).Wait();
            }

            if (!isDevelopmentEnvironment)
            {
                return;
            }

            if (!context.Persons.Any())
            {
                dbSeeder.SeedPersonsInMemory().Wait();
            }

            if (!context.ProjectRoles.Any())
            {
                dbSeeder.SeedProjectRolesInMemory().Wait();
            }

            if (!context.Employees.Any())
            {
                dbSeeder.SeedEmployeesInMemory().Wait();
            }

            if (!context.Degrees.Any())
            {
                dbSeeder.SeedDegreesInMemory().Wait();
            }

            if (!context.EducationInstitutions.Any())
            {
                dbSeeder.SeedEduInstitutionsInMemory().Wait();
            }

            if (!context.TrainingCenters.Any())
            {
                dbSeeder.SeedTrainingCentersInMemory().Wait();
            }

            if (!context.TrainingNames.Any())
            {
                dbSeeder.SeedTrainingNamesInMemory().Wait();
            }
            if (!context.CertificateNameIssuers.Any())
            {
                dbSeeder.SeedCertificateNameIssuersInMemory().Wait();
            }
            if (!context.CertificateTypes.Any())
            {
                dbSeeder.SeedCertificateTypesInMemory().Wait();
            }
        }
    }
}