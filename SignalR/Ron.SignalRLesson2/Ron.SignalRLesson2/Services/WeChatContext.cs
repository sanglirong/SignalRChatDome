using Microsoft.EntityFrameworkCore;
using Ron.SignalRLesson2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ron.SignalRLesson2.Services
{
    public class WeChatContext : DbContext
    {
        public WeChatContext(DbContextOptions<WeChatContext> options) : base(options)
        {

        }

        /// <summary>
        /// 在线列表
        /// </summary>
        public DbSet<AbpOnlineClient> OnlineClientList { get; set; }
    }


    public class DAL_OnlieClient : IDAL_OnlineClient
    {
        private readonly WeChatContext _context;

        public DAL_OnlieClient(WeChatContext context)
        {
            _context = context;
        }

        public List<AbpOnlineClient> GetListByName(string uname)
        {
            return _context.OnlineClientList.Where(a => a.UserName == uname).ToList();
        }


        /// <summary>
        /// 保存或删除
        /// </summary>
        /// <param name="connid"></param>
        /// <param name="uname"></param>
        public async void SaveAsync(string connid, string uname)
        {
            try
            {
                var item = new AbpOnlineClient(connid, uname);
                await _context.OnlineClientList.AddAsync(item);
                _context.SaveChanges();
            }
            catch { }
        }

        /// <summary>
        /// 删除连接对象
        /// </summary>
        /// <param name="connid"></param>
        public async void DeleteAsync(string connid)
        {
            try
            {
                var item = await _context.OnlineClientList.FirstOrDefaultAsync(a => a.ConnectionId == connid);
                if (item != null) _context.OnlineClientList.Remove(item);
                _context.SaveChanges();
            }
            catch { }
        }

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="connids"></param>
        public void DeleteList(List<string> connids)
        {
            try
            {
                var clients = _context.OnlineClientList.Where(a => connids.Contains(a.ConnectionId));
                _context.OnlineClientList.RemoveRange(clients);

            }
            catch { }
        }

    }

    public interface IDAL_OnlineClient
    {
        List<AbpOnlineClient> GetListByName(string uname);

        void SaveAsync(string connid, string uname);

        void DeleteAsync(string connid);


        void DeleteList(List<string> connids);
    }

}
