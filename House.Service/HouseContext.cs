using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace House.Service
{
    public class HouseContext:DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof(HouseContext));//声明Log4NET对象，建议一个类就声明一个ILog对象
        public HouseContext() : base("name=conn")
        {
            //将EF生成的sql语句记录在日志里面
            this.Database.Log = (sql) =>
            {
                log.DebugFormat("EF开始执行sql语句{0}", sql);
            };
            //Database.SetInitializer<HouseContext>(null);//只要数据库建造好后，就加上这句话，禁止Ef再去帮你创建数据库的一些操作
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommunityEntity> Communities { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointments { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<HousePicEntity> HousePics { get; set; }
        public DbSet<AdminLogEntity> AdminUserLogs { get; set; }
    }
}
