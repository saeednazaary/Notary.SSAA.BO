using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSAA.Notary.Domain.Abstractions.Base;
using SSAA.Notary.Domain.Entities;

namespace SSAR.Notary.Coordinator.NotaryDocument.Core.Transactions.Core
{
    class TransactionInfoManager
    {
        private readonly IRepository<TransactionInfo> _transactionInfoRepository;

        public TransactionInfoManager(IRepository<TransactionInfo> transactionInfoRepository)
        {

            _transactionInfoRepository = transactionInfoRepository;
        }

        public CreateTransactionSteps()
    }
}
