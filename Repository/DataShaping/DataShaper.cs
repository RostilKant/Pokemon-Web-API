using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Contracts;
using Entities.Models;

namespace Repository.DataShaping
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldsString) =>
            FetchData(entities, GetRequiredProperties(fieldsString));


        public ShapedEntity ShapeData(T entity, string fieldsString) =>
            FetchDataForEntity(entity, GetRequiredProperties(fieldsString));

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();
            if (string.IsNullOrWhiteSpace(fieldsString)) 
                requiredProperties = Properties.ToList();
            else
            {
                var fields = fieldsString.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);

                requiredProperties.AddRange(fields.Select(field =>
                        Properties.FirstOrDefault(p =>
                            p.Name.Equals(field, StringComparison.InvariantCultureIgnoreCase)))
                    .Where(property => property != null));
            }

            return requiredProperties;
        }

        private static IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities,
            IEnumerable<PropertyInfo> requiredProperties) =>
            entities.Select(entity => 
                FetchDataForEntity(entity, requiredProperties)).ToList();
        

        private static ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> 
            requiredProperties)
        {
            var shapedObject = new ShapedEntity();
            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectId = entity.GetType().GetProperty("Id");
            
            if (objectId != null) shapedObject.Id = (int) objectId.GetValue(entity);
            
            return shapedObject;
        }
    }
}