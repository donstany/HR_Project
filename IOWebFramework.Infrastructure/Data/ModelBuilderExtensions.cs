using ExcelDataReader;
using IOWebFramework.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace IOWebFramework.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedClassificator(this ModelBuilder builder, bool seed)
        {
            if (seed)
            {
                var filePath = @"c:\Users\ksavov\Desktop\Classificator\Oblasti i napravlenia original.xlsx";

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                List<Classifier> listOfEntities = new List<Classifier>();

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                  
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
                                listOfEntities.Add(new Classifier { Id = currentId, ParentId = currentId++, Name = row[0].ToString() });
                            }
                        }

                        //вкарва направленията и пълни речника с ID-та за вкарване на специалности
                        foreach (DataRow row in classificator.Rows) 
                        {
                            var temp = directionsPerParrentId.First();

                            //ако няма запис в втората колона
                            if (row[1].Equals(DBNull.Value))                           
                            {
                                specialtiesPerParrentId[listOfEntities.Last().Id]++;
                            }
                            //ако има запис в втората колона
                            else if (!row[1].Equals(DBNull.Value))                    
                            {
                                if (temp.Value == 0)
                                {
                                    directionsPerParrentId.Remove(temp.Key);
                                    temp = directionsPerParrentId.First();
                                }

                                listOfEntities.Add(new Classifier { Id = currentId++, ParentId = temp.Key, Name = row[1].ToString() });
                                directionsPerParrentId[temp.Key]--;
                                specialtiesPerParrentId.Add(listOfEntities.Last().Id, 1);
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

                            listOfEntities.Add(new Classifier { Id = currentId++, ParentId = temp.Key, Name = row[2].ToString() });
                            specialtiesPerParrentId[temp.Key]--;
                        }
                        excelreader.Close();
                        excelreader.Dispose();

                    }
                    builder.Entity<Classifier>().HasData(listOfEntities);
                }
            }
        }
       
    }                                                                    
}                                                                        