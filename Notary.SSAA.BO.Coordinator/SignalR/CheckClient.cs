using Microsoft.AspNetCore.SignalR.Client;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.Coordinator.SignalR
{
    public class CheckClient
    {
        public delegate Task<ApiResult> OnActionCompleted(CheckClientResult result);
        private List<string> ConnectionIdList;
        private HubConnection HubConnection { get; set; }
        private string HubUrl { get; set; }
        private CheckClientRequest? Request { get; set; }
        private bool ResponseReceived { get; set; }
        private CheckClientResult? Response { get; set; }
        public CheckClient(string hubUrl)
        {
            this.HubUrl = hubUrl;
            this.ResponseReceived = false;
            this.Request = null;
            this.Response = null;
            this.ConnectionIdList = new List<string>();
            this.HubConnection = new HubConnectionBuilder()
                 .WithUrl(HubUrl)
                 .WithAutomaticReconnect(new[]
                 {
                    TimeSpan.FromSeconds(0),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(3)
                 })
                 .Build();
            this.HubConnection.Reconnected += (ConnectionId) => { if (ConnectionId != null) this.ConnectionIdList.Add(ConnectionId); return Task.CompletedTask; };
            this.HubConnection.On<CheckClientResult>("CheckTokenResult", (result) =>
            {
                if (this.Request == null) return;
                if (result.RequestId == this.Request.RequestId && (result.ReceiverConnectionId == HubConnection.ConnectionId || this.ConnectionIdList.Contains(result.ReceiverConnectionId)))
                {
                    this.ResponseReceived = true;
                    this.Response = result;
                }
            });
        }
        public event OnActionCompleted? OnCompleted;
        public async Task<ApiResult> Send(CheckClientAction action, string clientConnetionId, CancellationToken cancellationToken)
        {                     
            if (this.OnCompleted == null)
            {
                throw new Exception("تابع OnCompleted مقداردهی نشده است");                
            }
            if (string.IsNullOrWhiteSpace(clientConnetionId))
            {
                throw new Exception( "شناسه سرویس گیرنده خالی می باشد");               
            }
            try
            {
               
                this.Request = new CheckClientRequest() { RequestId = Guid.NewGuid().ToString() };
                this.Request.ReceiverConnectionId = clientConnetionId;                
                
                
                await HubConnection.StartAsync(cancellationToken);
                if (HubConnection.ConnectionId != null)
                {
                    this.ConnectionIdList.Add(this.HubConnection.ConnectionId);
                    this.Request.SenderConnectionId = this.HubConnection.ConnectionId;
                }
                string methodName = "";
                if (action == CheckClientAction.CheckToken)
                    methodName = "CheckToken";
                await HubConnection.SendAsync(methodName, this.Request, cancellationToken);
                var startTime = DateTime.Now;
                var endTime = startTime.AddSeconds(10);
                while (startTime <= endTime)
                {
                    if (this.ResponseReceived)
                    {
                        await HubConnection.StopAsync(cancellationToken);
                        if (this.Response != null)
                        {
                            return await this.OnCompleted(this.Response);                            
                        }
                        else
                        {
                            this.ResponseReceived = false;                            
                        }
                    }
                    startTime = DateTime.Now;
                }
                
                await HubConnection.StopAsync(cancellationToken);
                return await this.OnCompleted(new CheckClientResult() { ErrorMessage = ".پاسخی از سرویس گیرنده در زمان مقرر (به علت قطعی ارتباط سرویس گیرنده با سرویس دهنده  یا بستن سامانه توسط سرویس گیرنده یا وجود اختلال در شبکه) دریافت نشد", Result = false, RequestId = this.Request.RequestId });
                
            }
            catch
            {
                try
                {
                    await HubConnection.StopAsync(cancellationToken);
                }
                catch
                {

                }
                return await OnCompleted(new CheckClientResult() { ErrorMessage = "خطا در برقراری ارتباط با سرویس گیرنده رخ داد", Result = false, RequestId = (this.Request != null) ? this.Request.RequestId : "" });
             
            }
            
        }
    }
    public enum CheckClientAction
    {
        CheckToken = 1
    }
    public class CheckClientRequest
    {
        public string ReceiverConnectionId { get; set; }
        public string SenderConnectionId { get; set; }
        public string RequestId { get; set; }
        public CheckClientRequest()
        {
            this.ReceiverConnectionId = string.Empty;
            this.SenderConnectionId = string.Empty;
            this.RequestId = string.Empty;
        }
    }
    public class CheckClientResult
    {
        public string ErrorMessage { get; set; }
        public bool Result { get; set; }
        public string RequestId { get; set; }
        public string ReceiverConnectionId { get; set; }
        public CheckClientResult()
        {
            this.ErrorMessage = string.Empty;
            this.RequestId = string.Empty;
            this.ReceiverConnectionId = string.Empty;
        }
    }
   
}
