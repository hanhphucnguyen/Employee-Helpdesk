/* HelpDeskRepository Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T: HelpdeskEntity
    {
        private HelpdeskContext ctx = null;

        public HelpdeskRepository(HelpdeskContext context = null)
        {
            ctx = context != null ? context : new HelpdeskContext();
        }

        public List<T> GetAll()
        {
            return ctx.Set<T>().ToList();
        }

        public List<T> GetByExpression(Expression<Func<T, bool>> lambdaExp)
        {
            return ctx.Set<T>().Where(lambdaExp).ToList();
        }

        public T Add(T entity)
        {
            ctx.Set<T>().Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus upStatus = UpdateStatus.Failed;

            try
            {
                HelpdeskEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                ctx.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                ctx.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);

                if (ctx.SaveChanges() == 1) // should throw exception if stale
                {
                    upStatus = UpdateStatus.Ok;
                }
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                upStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return upStatus;
        }

        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            ctx.Set<T>().Remove(currentEntity);
            return ctx.SaveChanges();
        }
    }
}
