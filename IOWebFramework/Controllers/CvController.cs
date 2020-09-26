using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using IOWebFramework.Components;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Models.Cv;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace IOWebFramework.Controllers
{
    public class CvController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public CvController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [MenuItem("cv")]
        public IActionResult Index(int employeeId)
        {
            CvViewModel cvviewmodel = _employeeService.GetCvViewModelById(employeeId);

            return new ViewAsPdf("Index", cvviewmodel)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                FileName = "CV-" + cvviewmodel.FullName + "_" + DateTime.Now.ToString("dd_MM_yyyy_HHч.mmм.ssс") + @".pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }

        [HttpPost]
        public IActionResult DownloadCheckedCvs(string checkedEmployeesIds)
        {
            var checkedEmployeesIdsSplited = checkedEmployeesIds.Split(',').Select(Int32.Parse).ToList();
            string zipFileName = "EmployeeCvs" + DateTime.Now.ToString("dd_MM_yyyy_HHч.mmм.ssс") + ".zip";

            List<ViewAsPdf> groupedPdfFiles;
            List<string> pdfFilesNames;
            GroupPdfFiles(checkedEmployeesIdsSplited, out groupedPdfFiles, out pdfFilesNames);
            
            byte[] compressedBytes = ArchivingGroupedPdfFiles(groupedPdfFiles, pdfFilesNames);

            return File(compressedBytes, "application/zip", zipFileName);
        }

        private byte[] ArchivingGroupedPdfFiles(List<ViewAsPdf> groupedPdfFiles, List<string> pdfFilesNames)
        {
            List<byte[]> inMemoryByteArray = groupedPdfFiles.Select(x => x.BuildFile(this.ControllerContext).GetAwaiter().GetResult()).ToList();

            byte[] compressedBytes;
            using (var outStream = new MemoryStream())
            {
                int i = 0;

                using (var archive = new ZipArchive(outStream, ZipArchiveMode.Create, true))
                {
                    foreach (var item in inMemoryByteArray)
                    {
                        var fileInArchive = archive.CreateEntry(pdfFilesNames[i], CompressionLevel.Optimal);
                        using (var entryStream = fileInArchive.Open())
                        using (var fileToCompressStream = new MemoryStream(item))
                        {
                            fileToCompressStream.CopyTo(entryStream);
                        }
                        i++;
                    }
                }

                compressedBytes = outStream.ToArray();
            }

            return compressedBytes;
        }

        private void GroupPdfFiles(List<int> checkedEmployeesIdsSplited, out List<ViewAsPdf> pdfFiles, out List<string> pdfFilesNames)
        {
            pdfFiles = new List<ViewAsPdf>();
            pdfFilesNames = new List<string>();
            foreach (int checkedEmployeeId in checkedEmployeesIdsSplited)
            {
                CvViewModel cvviewmodel = _employeeService.GetCvViewModelById(checkedEmployeeId);
                var pdfFile = new ViewAsPdf("Index", cvviewmodel)
                {
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,                    
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                };
                pdfFilesNames.Add("CV-" + cvviewmodel.FullName + @".pdf");
                pdfFiles.Add(pdfFile);
            }
        }
    }
}
