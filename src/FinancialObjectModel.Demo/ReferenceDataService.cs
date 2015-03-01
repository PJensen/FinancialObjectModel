using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using FinancialObjectModel.Interfaces;

namespace FinancialObjectModel.Demo
{
    /// <summary>
    /// ReferenceDataService
    /// </summary>
    internal class ReferenceDataService : IReferenceDataService
    {
        /// <summary>
        /// the baked in url for downloading reference data.
        /// </summary>
        private readonly Uri _nasdaqListedUri = new Uri("ftp://ftp.nasdaqtrader.com/SymbolDirectory/nasdaqlisted.txt");

        /// <summary>
        /// the web client from which this instance will download the reference data
        /// </summary>
        private readonly WebClient _webClient = new WebClient();

        /// <summary>
        /// 
        /// </summary>
        public List<Security> Securities { get { return _securities; } }

        /// <summary>
        /// GetSecurity
        /// </summary>
        /// <param name="ticker">the ticke</param>
        /// <returns></returns>
        public Security GetSecurity(string ticker)
        {
            return _securities.SingleOrDefault(s => s.Ticker == ticker);
        }

        /// <summary>
        /// securities
        /// </summary>
        readonly List<Security> _securities = new List<Security>();

        /// <summary>
        /// Initialized
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Initializes the reference data service
        /// </summary>
        public bool Initialize()
        {
            const string referenceData = "reference.dat";
            const int maxTries = 2;

            var tryCount = 0;

            #region download NASDAQ data; reduce repeated visits by storing to disk

            while (!File.Exists(referenceData) && tryCount < maxTries)
            {
                try
                {
                    _webClient.DownloadFile(_nasdaqListedUri, referenceData);
                }
                catch (Exception)
                {
                    // TODO: add exception hdlr to the interface
                }
                finally
                {
                    tryCount++;
                }
            }

            byte[] raw;

            try
            {
                raw = File.ReadAllBytes(referenceData);
            }
            catch (Exception)
            {
                return false;
            }

            #endregion

            using (var m = new MemoryStream(raw))
            using (var s = new StreamReader(m))
            {
                var line = s.ReadLine();

                // if there is no header; return empty handed.
                if (string.IsNullOrEmpty(line)) return false;

                // Symbol|Security Name|Market Category|Test Issue|Financial Status|Round Lot Size
                // 0     | 1           | 2             | 3        |3               | 4
                // File Creation Time: 0227201521:33|||||
                var columnMap = new Dictionary<string, int>();

                #region create columnar mapping

                var columns = line.Split('|');

                for (var i = 0; i < columns.Length; i++)
                {
                    columnMap.Add(columns[i], i);
                }

                #endregion

                while (!string.IsNullOrEmpty((line = s.ReadLine())))
                {
                    columns = line.Split('|');

                    const string symbolColumnName = "Symbol";
                    const string securityNameColumnNamne = "Security Name";

                    var symbol = columns[columnMap[symbolColumnName]];
                    var securityName = columns[columnMap[securityNameColumnNamne]];

                    var tmp = new Equity(securityName, symbol);

                    const string marketCategoryColumnName = "Market Category";
                    const string testIssueColumnName = "Test Issue";
                    const string statusColumnName = "Financial Status";
                    const string lotSizeColName = "Round Lot Size";

                    // File Creation Time: 0227201521:33
                    if (TryGetEndOfFile(columns))
                    {
                        break;
                    }

                    var securityStatus = columns[columnMap[statusColumnName]];
                    var securityTestIssue = columns[columnMap[testIssueColumnName]];
                    var securityMarketCategory = columns[columnMap[marketCategoryColumnName]];
                    var lotSizeColumn = columns[columnMap[lotSizeColName]];

                    tmp.Attributes[lotSizeColName] = int.Parse(lotSizeColumn);
                    tmp.Attributes[statusColumnName] = securityStatus;
                    tmp.Attributes[testIssueColumnName] = securityTestIssue == "Y";
                    tmp.Attributes[marketCategoryColumnName] = securityMarketCategory;

                    _securities.Add(tmp);
                }
            }

            return Initialized = _securities.Count > 0;
        }

        /// <summary>
        /// <value>File Creation Time: (\\d*):\\d*</value>
        /// </summary>
        static readonly Regex RgxCreationTime = new Regex("File Creation Time: (\\d*):\\d*");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        private static bool TryGetEndOfFile(string[] columns)
        {
            var fileCreationTime = columns[0];

            if ((RgxCreationTime.IsMatch(fileCreationTime)))
            {
                var tmpMatch = RgxCreationTime.Match(fileCreationTime);
                long tmpTicks;
                if ((long.TryParse(tmpMatch.Groups[1].Value, out tmpTicks)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}