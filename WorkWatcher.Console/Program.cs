using Lorenzo.WorkWatcher.Core.DbModels;
using Lorenzo.WorkWatcher.Core.Managers;
using Lorenzo.WorkWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lorenzo.WorkWatcher.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IRawDataService rawDataService = new RawDataService();
            IDataManager dataManager = new DataManager(rawDataService);
            dataManager.BlackList = new List<string>
            {
                "Explorer.EXE", "SearchUI.exe", "LockAppHost.exe", "ShellExperienceHost.exe", "ApplicationFrameHost.exe", "WindowsIoTCoreWatcher.exe"
            };

            bool correctData = true;
            if (correctData)
            {
                IEnumerable<RawData> collectionCheck = null;
                var collection = dataManager.GetAll().Select(d => new RawData
                {
                    DateCreated = d.DateCreated,
                    DateFinished = d.DateFinished,
                    Description = d.Description,
                    ProcessId = d.ProcessId,
                    ProcessName = d.ProcessName,
                    WindowTitle = d.WindowTitle
                }).ToList();

                Console.WriteLine(collection.Count());

                if (rawDataService.Truncate())
                {
                    collectionCheck = dataManager.GetAll();
                    Console.WriteLine(collectionCheck.Count());
                }

                Console.WriteLine(collection.Count());
                dataManager.SaveData(collection);

                collectionCheck = dataManager.GetAll().ToList();
                Console.WriteLine(collectionCheck.Count());
            }


            var groupedList = dataManager.GroupedProcessData(DateTime.Now);
            foreach (var item in groupedList)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} ({4})",
                    item.Date, item.ProcessName, item.WindowTitle, item.Amount, item.Count);
            }
        }
    }
}
