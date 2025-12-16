using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Notary.SSAA.BO.Domain.Entities;
using System.Security.Policy;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Azure.Core;
using System.Net.Http;
using System.Windows.Input;
using System.Threading;


namespace Notary.SSAA.BO.Infrastructure.Persistence.Interceptors
{
    public class ChangesInterceptor : SaveChangesInterceptor
    {
        IConfiguration _configuration;
        List<EntityAttribute> entityAttributes;
        string eventId = null;
        DateTime startDateTime;
       public ChangesInterceptor(IConfiguration configuration )
        {

            _configuration = configuration;
        
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbContext = eventData.Context;
          entityAttributes = new List<EntityAttribute>();


            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified ||e.State==EntityState.Deleted))
            {

                var entityType = entry.Entity.GetType().ToString().Replace("Notary.SSAA.BO.Domain.Entities.","");
                if (isEntityNameInWhiteList(entityType))
                {
   
                    EntityAttribute entityAttribute = new EntityAttribute();
                    if (entry.State == EntityState.Added)
                    {
                        var properties = entry.Properties
                          .Select(prop => new
                          {
                              Property = prop.Metadata,
                              Value = prop.CurrentValue,
                          }).Select(p => new Attribute { Name = p.Property.Name, Value = p.Value }).ToList();

                        entityAttributes.Add(new EntityAttribute { Attributes = properties, EntityName = entityType, OpertionType = OperationTypeEnum.Insert });


                    }
                    else if (entry.State == EntityState.Deleted)
                    {

                        var properties = entry.Properties.Where(p=>p.Metadata.Name=="Id")
                         .Select(prop => new
                         {
                             Property = prop.Metadata,
                             Value = prop.CurrentValue,
                         }).Select(p => new Attribute { Name = p.Property.Name, Value = p.Value }).ToList();

                        entityAttributes.Add(new EntityAttribute { Attributes = properties, EntityName = entityType, OpertionType = OperationTypeEnum.Delete });


                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        var properties = entry.Properties
                             .Where(prop => prop.IsModified || prop.Metadata.Name == "Id")
                             .Select(prop => new
                             {
                                 Property = prop.Metadata,
                                 Value = prop.CurrentValue,
                             }).Select(p => new Attribute { Name = p.Property.Name, Value = getValue( p.Value )}).ToList();

                        entityAttributes.Add(new EntityAttribute { Attributes = properties, EntityName = entityType, OpertionType = OperationTypeEnum.Update });


                    }
                }
            }

            if (entityAttributes != null && entityAttributes.Count > 0)
            {
                string id = null;
                try
                {
                     id = await sendLog(entityAttributes);

                }
                catch (Exception ex)
                {

                }
                if (id != null)
                {
                    eventId = id;
                    startDateTime = DateTime.UtcNow;
                    var saveChangeResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
                    return saveChangeResult;

                }
                else
                {
                    throw new DbUpdateException("خطا در ذخیره سازی اطلاعات در سرور");
                }
            }
            else {

                var saveChangeResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
                return saveChangeResult;

            }
         



            //if (((int)saveChangeResult.Result)>0 )
                //{
                //if (entityAttributes.Count > 0)
                //{ 
                
                //   await sendLog(entityAttributes);


                //}
            //}
           
        }

        public object getValue(object value)
        {
            if (value != null)
            {

                if (value.GetType() == typeof(byte[]))
                {
                    return Convert.ToBase64String(value as byte[]);


                }
            }
            return value;
        
        }
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default(CancellationToken))
        {
            var finalResult =await  base.SavedChangesAsync(eventData, result,cancellationToken);
            if (((uint)finalResult) > 0 && entityAttributes!=null && entityAttributes.Count>0)
            {
                await changeLogStatus((int)EventStatusEnum.Draft);



            }


            return finalResult;
        }

        public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesFailedAsync(eventData, cancellationToken);

            if (entityAttributes != null && entityAttributes.Count > 0)
            { 
               await changeLogStatus((int)EventStatusEnum.SaveError);

            }


        }

        public Boolean isEntityNameInWhiteList(string name)
        {

            List<string> whiteList = new List<string>() { "SignRequestPerson","SignRequestPersonRelated","SignRequestSemaphore","SignRequest","SignRequestFile","SignElectronicBook", "PersonFingerprint" };
            if (whiteList.Contains(name))
            {
                return true;
            }
            else {
                return false;
            }

        }

        public async Task<string> sendLog(List<EntityAttribute> entityAttributes)
        {
            try
            {

                var httpclient = new HttpClient();
                string address = _configuration.GetValue(typeof(string), "EventLogAddress").ToString() + "/Events/api/v1/Event";
                httpclient.Timeout = TimeSpan.FromSeconds(5);
                // var address =   "http://192.168.20.39:8081/Events/api/v1/Event";
                CreateEventRequest createEventRequest = new CreateEventRequest(JsonConvert.SerializeObject(entityAttributes));
                var eventResponse = await httpclient.PostAsJsonAsync(address, createEventRequest);

                if (eventResponse.StatusCode == HttpStatusCode.OK)
                {
                    string res = await eventResponse.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<dynamic>(res);
                    if (content != null && content.isSuccess == true)
                    {

                        return content.value.eventId;

                    }
                    else

                        return null;


                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task changeLogStatus(int status)
        {
            var httpclient = new HttpClient();
            string address = _configuration.GetValue(typeof(string), "EventLogAddress").ToString() + "/Events/api/v1/Event/ChangeStatus";
            // var address =   "http://192.168.20.39:8081/Events/api/v1/Event";
            UpdateEventCommand updateEventCommand = new UpdateEventCommand(eventId, (int)status, startDateTime, DateTime.UtcNow,null);
            var eventResponse = await httpclient.PostAsJsonAsync(address, updateEventCommand).ConfigureAwait(false);
        }
        /// <summary>
        ///   var httpclient = new HttpClient();
        /// var address = _configuration.GetSection("EventLogAddress").Value.ToString() + "/Events/api/v1/Event";

        ///var eventResponse = await httpclient.PostAsJsonAsync(address, request).ConfigureAwait(false);

        /// </summary>

        public class EntityAttribute
        {
           public List<Attribute> Attributes = new List<Attribute>();

           public string EntityName { set; get; }
           public OperationTypeEnum OpertionType { set; get; }

           

           

        }

        public class Attribute
        { 
            public string Name { get; set; } 
            public Object Value { get; set; } 
        
        }

        public enum OperationTypeEnum
        {
            Insert = 1,
            Update = 2,
            Delete = 3,          

        }
        public enum EventStatusEnum
        {
            SaveError = -1,
            BeforeSave = 0,
            Draft = 1,
            Published = 2,
            Completed = 3,
            Canceled = 4
        }
        public sealed record CreateEventRequest( string content);
        public sealed record UpdateEventCommand(string eventId, int status, DateTime startDateTime, DateTime endDateTime, string Response);


    }

}
