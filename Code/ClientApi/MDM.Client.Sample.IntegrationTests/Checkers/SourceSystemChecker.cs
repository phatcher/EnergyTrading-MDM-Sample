﻿// <autogenerated>
//   This file was generated by T4 code generator CreateIntegrationTestsScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace MDM.Client.Sample.IntegrationTests.Checkers
{
    using EnergyTrading.Mdm.Contracts;

    public class SourceSystemChecker : NCheck.Checker<SourceSystem>
    {
        public SourceSystemChecker()
        {
            Compare(x => x.Identifiers);
            Compare(x => x.Details);
        }
    }
}