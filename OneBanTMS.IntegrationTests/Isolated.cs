using System;
using System.Transactions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace OneBanTMS.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;
        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        public ActionTargets Targets { get; } = ActionTargets.Test;
    }
}