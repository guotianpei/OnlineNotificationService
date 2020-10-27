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
using OPM.Infrastructure.Repositories.QueryRequests;

namespace OPM.Infrastructure.Repositories
{
    public class ProfileRepository : GenericRepository<EntityProfile>, IProfileRepository
    {

        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext context):base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public Task<EntityProfile> GetAsync(string EntityID)
        {
            return Task.FromResult(_context.EntityProfiles.Where(obj => obj.EntityID == EntityID)
                                                           .Include(obj => obj.ProfileResource)
                                                           .Include(obj => obj.ProfileComChannels)
                                                           .FirstOrDefault<EntityProfile>());
        }


        public Task<List<EntityProfile>> GetEntityProfiles(string EntityID)
        {
            //_context.EntityProfiles.
            var student = (from s in _context.EntityProfiles
                           where s.EntityID == EntityID
                           select s).FirstOrDefault<EntityProfile>();

            return Task.FromResult(_context.EntityProfiles.Where(obj => obj.EntityID == EntityID)
                                                           .Include(obj => obj.ProfileResource)
                                                           .Include(obj => obj.ProfileComChannels)
                                                           //.FirstOrDefault<EntityProfile>()
                                                           .ToList<EntityProfile>()
                                                           );
        }

        public Task<List<ProfileComChannel>> GetProfileComChannels(string entityID, string comchannel, bool isActive)
        {

            if (comchannel != null && comchannel.Trim().Length > 0)
            {
                return Task.FromResult(_context.ProfileComChannels.Where(obj => obj.EntityID == entityID
                                                            && obj.ComChannel == comchannel)
                                                            .ToList<ProfileComChannel>()
                                                           );
            }
            else
            {
                return Task.FromResult(_context.ProfileComChannels.Where(obj => obj.EntityID == entityID)
                                                           .ToList<ProfileComChannel>()
                                                          );
            }
        }

       
        public Task<List<ProfileComChannel>> GetProfileComChannelByIDs(ProfileComChannelRequest request)
        {

            //Will convert it to call Database
            //var joinResult = _context.ProfileComChannels.Join(// outer sequence 
            //          request.ListEntityIDs,  // inner sequence 
            //          ComChannel => ComChannel.EntityID,    // outerKeySelector
            //          reqestEntity => reqestEntity.EntityID,  // innerKeySelector
            //          (ComChannel, reqestEntity) => new // result selector
            //          {
            //              EntityID = ComChannel.EntityID,
            //              comChannel = ComChannel.ComChannel,
            //              Value = ComChannel.Value,
            //              Enabled = ComChannel.Enabled,
            //              Preference = ComChannel.Preference,
            //              TermDate = ComChannel.TermDate
            //          });



            List < ProfileComChannel> profileComChannels = new List<ProfileComChannel>();
            try
            {
                //Test
               // var books = _context.ProfileComChannels.FromSqlRaw("SELECT [Id],[EntityID],[ComChannel],[Value],[Enabled],[Preference],[TermDate],[Status]  FROM[OPM].[dbo].[ProfileComChannel]").ToList();

                var entityIDs = new SqlParameter("@EntityIDs", request.EntityIDs);
                var joinResult = _context.ProfileComChannels 
                .FromSqlRaw("EXEC GetProfileComChannelByIDs @EntityIDs", entityIDs)
                .ToList();

                profileComChannels = joinResult;
                //List<ProfileComChannel> profileComChannels = joinResult;
                //return Task.FromResult(joinResult);

                if (request.ComChannel != null && request.ComChannel.Trim().Length > 0)
                {
                    profileComChannels = joinResult.Where(obj => obj.ComChannel == request.ComChannel)
                                                                .ToList<ProfileComChannel>();
                }
                
                if (request.IsActive)
                {
                    profileComChannels = profileComChannels.Where(obj => obj.Enabled == request.IsActive)
                                                               .ToList<ProfileComChannel>();
                } 

            }  catch (Exception ex)
            {
                string err = ex.Message + " " + ex.StackTrace;

                throw ex;
            }


            return Task.FromResult(profileComChannels);

        }



    }
}
