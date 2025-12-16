namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Messaging
{
    using MediatR;
    using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="SMSGeneratorEngine" />
    /// </summary>
    public class SMSGeneratorEngine
    {
        /// <summary>
        /// Defines the mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SMSGeneratorEngine"/> class.
        /// </summary>
        /// <param name="_mediator">The _mediator<see cref="IMediator"/></param>
        public SMSGeneratorEngine(IMediator _mediator)
        {
            mediator = _mediator;
        }

        /// <summary>
        /// The GetStandardCellphoneNumber
        /// </summary>
        /// <param name="inputNumber">The inputNumber<see cref="string?"/></param>
        /// <returns>The <see cref="string?"/></returns>
        private string? GetStandardCellphoneNumber(string? inputNumber)
        {
            if (string.IsNullOrWhiteSpace(inputNumber))
                return null;

            if (inputNumber.Length < 10)
                return null;

            if (!inputNumber.StartsWith("0"))
            {
                if (inputNumber.StartsWith("+98"))
                    inputNumber = inputNumber.Replace("+98", "0");
                else
                    inputNumber = "0" + inputNumber;
            }

            return inputNumber;
        }

        /// <summary>
        /// The GenerateCoreSMS
        /// </summary>
        /// <param name="messageInputPacket">The messageInputPacket<see cref="MessageInputPacket?"/></param>
        /// <param name="smsCostPolicy">The smsCostPolicy<see cref="SMSCostPolicy"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        internal async Task<bool> GenerateCoreSMS(MessageInputPacket? messageInputPacket, SMSCostPolicy smsCostPolicy)
        {
            return true;
            try
            {
                string? phoneNumber = this.GetStandardCellphoneNumber(messageInputPacket?.RecipientPhoneNo);

                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    SendSmsFromKanoonServiceInput sendSMSServiceInput = new();
                    sendSMSServiceInput.Message = messageInputPacket?.MessageContext;
                    sendSMSServiceInput.MobileNo = messageInputPacket?.RecipientPhoneNo;
                    //sendSMSServiceInput.ClientId = "SSAR";

                    CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken cancellationToken = source.Token;
                    var smsResult = await mediator.Send(sendSMSServiceInput, cancellationToken);


                    //SMSEngineCoreService.ServiceSoapClient smsEngineCoreServiceController = new SMSEngineCoreService.ServiceSoapClient();
                    //long[] smsResult = null;
                    //switch (smsCostPolicy)
                    //{
                    //    case SMSCostPolicy.Non_Free:
                    //        smsResult = smsEngineCoreServiceController.SMS_Insert(145, "dexter@1347", 0, messageInputPacket.RecipientPhoneNo, messageInputPacket.MessageContext);
                    //        break;
                    //    case SMSCostPolicy.Free:
                    //        smsResult = smsEngineCoreServiceController.SMS_Insert(147, "jh976oG809$ii", 0, messageInputPacket.RecipientPhoneNo, messageInputPacket.MessageContext);
                    //        break;
                    //    default:
                    //        break;
                    //}

                    return smsResult.IsSuccess;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
