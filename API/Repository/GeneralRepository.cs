using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key NIK)
        {
            var entity = entities.Find(NIK);

            if (entity != null)
            {
                entities.Remove(entity);
            }
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = 0;
            try
            {
                myContext.SaveChanges();
                result = 1;
            }
            catch (Exception)
            {
                result = 0;

            }
            return result;
        }


        public int Update(Entity entity,Key key)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = 0;
            try
            {
                myContext.SaveChanges();
                result = 1;
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
    }
}
