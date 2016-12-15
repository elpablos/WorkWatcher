using Lorenzo.WorkWatcher.Core.Common;
using Lorenzo.WorkWatcher.Core.DbModels;
using Lorenzo.WorkWatcher.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lorenzo.WorkWatcher.Core.Managers
{
    public class DataManager : IDataManager
    {
        #region Fields

        private readonly IRawDataService _RawDataService;

        #endregion

        #region Properties

        /// <summary>
        /// Blacklist
        /// </summary>
        public IList<string> BlackList { get; set; }

        #endregion

        public DataManager(IRawDataService rawDataService)
        {
            _RawDataService = rawDataService;
            // todo 
            BlackList = new List<string>
            {
                "Explorer.EXE", "SearchUI.exe", "LockAppHost.exe", "ShellExperienceHost.exe", "ApplicationFrameHost.exe", "WindowsIoTCoreWatcher.exe"
            };
        }

        /// <summary>
        /// Ulozeni dat do databaze vcetne upravy dat
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public int SaveData(IEnumerable<RawData> original, bool isForce = false)
        {
            var data = CorrectData(original);
            var last = data.LastOrDefault();
            if (last != null)
            {
                last.DateFinished = DateTime.Now;
            }
            return _RawDataService.InserBulk(data);
        }

        /// <summary>
        /// Vratim vsechny zaznamy z DB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RawData> GetAll()
        {
            return _RawDataService.GetAll();
        }

        /// <summary>
        /// Ocisti vycet o duplicitni zaznamy a procesy z blacklistu
        /// </summary>
        /// <param name="original">puvodni vycet</param>
        /// <returns>novy vycet</returns>
        public IEnumerable<RawData> CorrectData(IEnumerable<RawData> original)
        {
            var ret = new List<RawData>();
            RawData last = null;
            foreach (var data in original)
            {
                if (last != null)
                {
                    // pokud je v blackListu, preskoc
                    if (BlackList.Contains(data.ProcessName, StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // pokud jsou stejny, preskoc
                    if (CompareRawData(data, last))
                    {
                        continue;
                    }

                    last.DateFinished = data.DateCreated;
                }

                // pridam na seznam
                ret.Add(data);
                // aktualizace posledniho
                last = data;
            }

            // vratim novy seznam dat
            return ret;
        }

        public IEnumerable<RawDataGrouped> GroupedTitleData(DateTime dateFilter)
        {
            return GetAll()
            .Where(x => x.DateCreated.ToString("yyyy.MM.dd.hh") == dateFilter.ToString("yyyy.MM.dd.hh"))
            .Select(x => new
            {
                //Id = x.Id,
                //DateCreated = x.DateCreated,
                //DateFinished = x.DateFinished,
                //ProcessId = x.ProcessId,
                ProcessName = x.ProcessName,
                WindowTitle = x.WindowTitle,
                Diff = (x.DateFinished - x.DateCreated).Ticks,
                Date = x.DateCreated.Trim(TimeSpan.TicksPerHour)
            })
            .GroupBy(x => new
            {
                x.Date,
                x.ProcessName,
                x.WindowTitle
            })
            .Select(x => new RawDataGrouped
            {
                Date = x.Key.Date,
                ProcessName = x.Key.ProcessName,
                WindowTitle = x.Key.WindowTitle,
                Amount = TimeSpan.FromTicks(x.Sum(y => y.Diff)),
                Count = x.Count()
            })
            .OrderBy(x => x.Date)
            .ThenByDescending(x => x.Amount);
        }

        public IEnumerable<RawDataGrouped> GroupedProcessData(DateTime dateFilter)
        {
            return GetAll()
            .Where(x => x.DateCreated.ToShortDateString() == dateFilter.ToShortDateString())
            .Select(x => new
            {
                //Id = x.Id,
                //DateCreated = x.DateCreated,
                //DateFinished = x.DateFinished,
                //ProcessId = x.ProcessId,
                ProcessName = x.ProcessName,
                //WindowTitle = x.WindowTitle,
                Diff = (x.DateFinished - x.DateCreated).Ticks,
                Date = x.DateCreated.Trim(TimeSpan.TicksPerHour)
            })
            .GroupBy(x => new
            {
                x.Date,
                x.ProcessName,
            })
            .Select(x => new RawDataGrouped
            {
                Date = x.Key.Date,
                ProcessName = x.Key.ProcessName,
                Amount = TimeSpan.FromTicks(x.Sum(y => y.Diff)),
                Count = x.Count()
            })
            .OrderBy(x => x.Date)
            .ThenByDescending(x => x.Amount);
        }

        /// <summary>
        /// Porovnani dvou polozek dat
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        protected virtual bool CompareRawData(RawData first, RawData second)
        {
            if (first == null || second == null) return false;
            return (first.ProcessId == second.ProcessId
                && first.ProcessName == second.ProcessName
                && first.WindowTitle == second.WindowTitle);
        }
    }
}
