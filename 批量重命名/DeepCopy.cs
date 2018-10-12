using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 批量重命名
{
    public static class DeepCopy
    {
        /// <summary>
        /// IList<T>的扩展方法，实现集合元素（不适用于集合中集合的情况）的深拷贝
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="listToClone">要操作的IList<T>对象</param>
        /// <returns>深拷贝后的IList<T>对象</returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T: ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        
        }
    }
}
