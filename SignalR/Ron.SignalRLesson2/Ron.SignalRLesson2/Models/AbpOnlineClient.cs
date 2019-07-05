using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ron.SignalRLesson2.Models
{
    [Table("AbpOnlineClient")]
    public class AbpOnlineClient
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        public AbpOnlineClient() { }

        public AbpOnlineClient(string connid, string uname)
        {

            ConnectionId = connid;
            UserName = uname;
        }
    }
}
