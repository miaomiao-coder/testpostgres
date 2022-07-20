
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Repository.Base
{

    internal static class EntityPropertiesCache
    {
        public static string GovCode = string.Empty;

        public static Dictionary<string, EntityProperties> PropertiesCache { get; set; } = new Dictionary<string, EntityProperties>();

        /// <summary>
        /// 包含设置了字段类型属性的字段
        /// </summary>
        public static Dictionary<string, List<PropertyInfo>> PropertyWithColumnAttributes { get; set; } = new Dictionary<string, List<PropertyInfo>>();


        public static Dictionary<Type, Dictionary<string, string>> _EntityPropertyTypeCache { get; set; } = new Dictionary<Type, Dictionary<string, string>>();

        public static Dictionary<Type, Dictionary<string, string>> GetEntityPropertyTypeCache()
        {
            if (_EntityPropertyTypeCache != null) return _EntityPropertyTypeCache;

            string path = AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory;
            Assembly[] assemblies = Directory.GetFiles(path, $"*.Domain.dll").Select(m => Assembly.LoadFrom(m)).ToArray();

            List<Type> listExists = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(n => n.IsClass);

                foreach (var type in types)
                {
                    var tableType = types.Where(n => n.GetCustomAttribute<TableAttribute>() != null).FirstOrDefault();
                    if (tableType == null) continue;

                    Dictionary<string, string> dicProperties = new Dictionary<string, string>();
                    foreach (var property in tableType.GetProperties())
                    {
                        dicProperties.Add(property.Name, property.PropertyType.Name);
                    }
                    if (!listExists.Contains(tableType))
                    {
                        _EntityPropertyTypeCache.Add(tableType, dicProperties);
                    }
                    listExists.Add(tableType);
                }
            }
            return new Dictionary<Type, Dictionary<string, string>>();
        }


        public static List<PropertyInfo> GetPropertyWithColumnAttributes<TEntity>()
        {
            string Name = typeof(TEntity).Name;
            if (PropertyWithColumnAttributes != null && PropertyWithColumnAttributes.Any() && PropertyWithColumnAttributes.ContainsKey(Name))
            {
                return PropertyWithColumnAttributes[Name];
            }
            return null;
        }

        /// <summary>
        /// 获取实体类中包含IDeleteEntity的实体
        /// </summary>
        public static IEnumerable<IMutableEntityType> WithDeleteEntites { get; set; }


        public static IEnumerable<IMutableEntityType> GetEntitiesWithIDeleteEntity(ModelBuilder modelBuilder)
        {
            if (WithDeleteEntites == null || !WithDeleteEntites.Any())
            {
                //WithDeleteEntites = modelBuilder.Model.GetEntityTypes().Where(e => typeof(IDeleteEntity).IsAssignableFrom(e.ClrType));
            }
            return WithDeleteEntites;
        }


        /// <summary>
        /// 获取实体类中包含IGovEntity的实体
        /// </summary>
        public static IEnumerable<IMutableEntityType> WithGovEntites { get; set; }


        public static IEnumerable<IMutableEntityType> GetEntitiesWithIGovEntity(ModelBuilder modelBuilder)
        {
            if (WithGovEntites == null || !WithGovEntites.Any())
            {
                //WithGovEntites = modelBuilder.Model.GetEntityTypes().Where(e => typeof(IGovEntity).IsAssignableFrom(e.ClrType));
            }
            return WithGovEntites;
        }


        /// <summary>
        /// 获取实体类中包含IGovernmentEntity的实体
        /// </summary>
        public static IEnumerable<IMutableEntityType> WithGovermentEntites { get; set; }


        public static IEnumerable<IMutableEntityType> GetEntitiesWithIGovermentEntites(ModelBuilder modelBuilder)
        {
            if (WithGovEntites == null || !WithGovEntites.Any())
            {
                //WithGovEntites = modelBuilder.Model.GetEntityTypes().Where(e => typeof(IGovernmentEntity).IsAssignableFrom(e.ClrType));
            }
            return WithGovEntites;
        }
    }

    internal class EntityProperties
    {
        public PropertyInfo PIId { get; set; }

        public PropertyInfo PiCreateBy { get; set; }

        public PropertyInfo PiCreateTime { get; set; }

        public PropertyInfo PiUpdateBy { get; set; }

        public PropertyInfo PiUpdateTime { get; set; }

        public PropertyInfo PiDeleteTime { get; set; }

        public PropertyInfo PiDeleteBy { get; set; }

        public PropertyInfo PiIsDeleted { get; set; }

        public PropertyInfo PiGovFirstId { get; set; }

        public PropertyInfo PiGovSecId { get; set; }

        public PropertyInfo PiGovThirdId { get; set; }

        /// <summary>
        ///  获取系统字段
        /// </summary>
        /// <returns></returns>
        public EntityProperties GetEntityProperty<TEntity>()
        {
            string Name = typeof(TEntity).Name;
            if (EntityPropertiesCache.PropertiesCache != null && EntityPropertiesCache.PropertiesCache.ContainsKey(Name))
            {
                return EntityPropertiesCache.PropertiesCache[Name];
            }

            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            EntityProperties entityProperties = new EntityProperties();
            foreach (PropertyInfo pi in typeof(TEntity).GetProperties())
            {
                if (string.Equals("CreateTime", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiCreateTime = pi;
                    continue;
                }
                if (string.Equals("CreateBy", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiCreateBy = pi;
                    continue;
                }
                if (string.Equals("UpdateTime", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiUpdateTime = pi;
                    continue;
                }
                if (string.Equals("UpdateBy", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiUpdateBy = pi;
                    continue;
                }
                if (string.Equals("DeleteTime", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiDeleteTime = pi;
                    continue;
                }
                if (string.Equals("DeleteBy", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiDeleteBy = pi;
                    continue;
                }
                if (string.Equals("IsDeleted", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiIsDeleted = pi;
                    continue;
                }

                if (string.Equals("GovFirstId", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiGovFirstId = pi;
                    continue;
                }
                if (string.Equals("GovSecId", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiGovSecId = pi;
                    continue;
                }
                if (string.Equals("GovThirdId", pi.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    entityProperties.PiGovThirdId = pi;
                    continue;
                }

                //if (pi.GetCustomAttribute<DbColumnAttribute>() != null && !EntityPropertiesCache.PropertyWithColumnAttributes.ContainsKey(Name))
                //{
                //    propertyInfos.Add(pi);
                //}
            }

            if (propertyInfos != null && propertyInfos.Any())
            {
                EntityPropertiesCache.PropertyWithColumnAttributes.Add(Name, propertyInfos);
            }

            if (EntityPropertiesCache.PropertiesCache.ContainsKey(Name))
            {
                EntityPropertiesCache.PropertiesCache.Add(Name, entityProperties);
            }

            return entityProperties;
        }

        public static object GetColumnValue(PropertyInfo pi)
        {
            //var columnAttr = pi.GetCustomAttribute<DbColumnAttribute>();
            //if (columnAttr == null || columnAttr.ColumnType == ColumnType.Default)
            //    return null;

            //if (columnAttr.ColumnType == ColumnType.SnowFlake)
            //{
            //    return SnowFlake.GetId();
            //}

            //if (columnAttr.ColumnType == ColumnType.Redis)
            //{
            //    throw new Exception("该方法还未实现");
            //}
            //if (columnAttr.ColumnType == ColumnType.Guid)
            //{
            //    return Guid.NewGuid().ToString();
            //}
            return null;
        }
    }
}
