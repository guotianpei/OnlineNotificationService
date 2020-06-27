using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;
using OPM.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace OPM.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {

        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //public ProfileRepository()
        //{
        //    //_context = context ?? throw new ArgumentNullException(nameof(context));
        //}

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public EntityProfile Add(EntityProfile profile)
        {
            return _context.EntityProfiles.Add(profile).Entity;
        }
        public void Update(EntityProfile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
        }

        public Task<EntityProfile> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EntityProfile> GetAsync(string EntityID)
        {
            return   Task.FromResult(_context.EntityProfiles.Where(obj => obj.EntityID == EntityID).FirstOrDefault<EntityProfile>()); 

            //return GetEntityDetail(EntityID);

        }

        public IEnumerable<Task<EntityProfile>> GetMultiple(List<string> EntityIDs)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task<EntityProfile>> GetWhere(Expression<Func<Task, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        List<Task<EntityProfile>> IProfileRepository.GetMultiple(List<string> EntityIDs)
        {
            throw new NotImplementedException();
        }

        private Task<EntityProfile> GetEntityDetail(string entityid)
        {
            using (SqlConnection connection = new SqlConnection(
                "Server=localhost,5433;Database=OPM;User Id=SA;Password=Pass@word;"))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter sqlDA = new SqlDataAdapter("USP_GET_EntityProfileByEntityID", connection);
                    sqlDA.SelectCommand.Parameters.AddWithValue("@EntityID", entityid);
                    sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDA.Fill(ds);

                    EntityProfile entprof;
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        entprof = new EntityProfile("", "", "", "", "", "", 1);

                        return Task.FromResult(entprof);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }   
            }
            return null;

        }


    }
}
