using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace House.Service
{
    public class BaseService<T> where T : BaseEntity //泛型约束，表示只能传递BaseEntity或者BaseEntity的子类
    {
        private HouseContext context;

        public BaseService(HouseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// 查询所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return context.Set<T>().Where(p => p.IsDeleted == false);//此处是延迟加载，只有遍历结果时才执行SQL语句，例如toList toArray
        }

        /// <summary>
        /// 获取数据的总数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPagedData(int startIndex, int count)
        {
            return GetAll().OrderBy(e => e.CreateDateTime)
                .Skip(startIndex).Take(count);

            //skip就是跳过n调数据，Take就是取n条数据
        }

        /// <summary>
        /// 查找id=id的数据，如果找不到返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            //return GetAll().FirstOrDefault(T => T.Id == id);
            return GetAll().Where(m => m.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            int r = context.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
