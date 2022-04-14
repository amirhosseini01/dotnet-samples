using GoogleIndexingAPIMVC.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Models;

namespace Web.Jobs
{
    public class Jobs : IJobs
    {
        #region CTOR
        public Jobs(MyDatabaseContext context)
        {
            _Context = context;

            //RecurringJob.AddOrUpdate("IndexProducts", () => IndexProducts(), "00 10 * * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate("IndexStatus", () => IndexStatus(), "*/20 * * * *", TimeZoneInfo.Local);
        }
        private readonly MyDatabaseContext _Context;
        #endregion


        public async Task IndexProducts()
        {
            var googleBatchIndexingService = new GoogleBatchIndexingService();
            try
            {
                var urls = new[]
          {
                "https://someurl.com",
            };
                var res = await googleBatchIndexingService.BatchAddOrUpdateGoogleIndex(urls);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                throw;
            }
        }
        public async Task IndexStatus()
        {
            var googleSingleIndexingService = new GoogleSingleIndexingService();

            int lastSkipCount = await GetLastSkip(ApiActionType.MetaData.ToString());
            var productLinks = await _Context.TblProducts
                .Select(x => new VmProductList()
                {
                    FakeLink = x.FakeLink,
                    Id = x.Id
                }).OrderBy(x => x.Id).Skip(lastSkipCount).Take(180).ToListAsync();
            foreach (var item in productLinks)
            {
                lastSkipCount = lastSkipCount + 1;
                try
                {
                    string url = item.FakeLink.Replace("~", "");
                    url = "https://someUrl" + url;
                    var status = await googleSingleIndexingService.GetGoogleIndexStatus(url);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + " " + ex.InnerException?.Message);
                    await UpdateLastSkip(lastSkipCount, ApiActionType.MetaData.ToString());
                    throw;
                }
            }

            await UpdateLastSkip(lastSkipCount, ApiActionType.MetaData.ToString());
        }

        private async Task<int> GetLastSkip(string type)
        {
            return await _Context.TransferLogs.Where(x => x.ActionType == type).Select(x => x.LastTransferSkip).FirstOrDefaultAsync();
        }
        private async Task<int> UpdateLastSkip(int skipCount, string type)
        {
            var entity = await _Context.TransferLogs.FirstOrDefaultAsync();
            if (entity is null)
            {
                entity = new()
                {
                    LastTransferSkip = skipCount,
                    ActionType = type
                };
                await _Context.TransferLogs.AddAsync(entity);
            }
            else
            {
                entity.LastTransferSkip = skipCount;
                _Context.TransferLogs.Update(entity);
            }


            return await _Context.SaveChangesAsync();
        }
    }
    public class VmProductList
    {
        public long Id { get; set; }
        public string FakeLink { get; set; }
    }
}
