﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Test;

    public class SourceSystemChecker : Checker<SourceSystem>
    {
        public SourceSystemChecker()
        {
            this.Compare(x => x.Identifiers);
            this.Compare(x => x.Details);
        }
    }
}
